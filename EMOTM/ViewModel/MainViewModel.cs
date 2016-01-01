using System.Diagnostics;
using EMOTM.Infrastructure;
using EMOTM.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace EMOTM.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;



        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                    }
                });

            TimerDisplayForeground = BlackBrush;
            StartTimerCmd = new RelayCommand(ExecuteStartTimer, CanStartTimer);

            StopTimerCmd = new RelayCommand(StopTimer, CanStopTimer);

            PauseTimerCmd = new RelayCommand(PauseTimer, CanPauseTimer);

            TimerText = string.Format("{0}:00", TotalTime);

            PropertyChanged += OnPropertyChanged;
        }

        
        // 
        // We want the Start/Stop/Pause commands' enable state to respond to TimerState.
        // Rather than add junk to the TimerState setter, put things that are dependant on 
        // TimerState here.
        //
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            // Debug.WriteLine(String.Format("OnPropertyChanged: {0}", propertyChangedEventArgs.PropertyName));

            switch (propertyChangedEventArgs.PropertyName)
            {
                case TimerStatePropertyName:
                    StartTimerCmd.RaiseCanExecuteChanged();
                    StopTimerCmd.RaiseCanExecuteChanged();
                    PauseTimerCmd.RaiseCanExecuteChanged();
                    break;
                case LengthOfMinutePropertyName:
                    SetMainWinTitle();
                    break;
                case TimerTypePropertyName:
                    SetMainWinTitle();
                    break;
                default:
                    break;
            }
        }

        private void SetMainWinTitle()
        {
            string title = "TROUBLE";

            switch (TimerType)
            {
                case TimerType.EMOTM:
                    title = string.Format("E{0}MO{1}M", (LengthOfMinute > 1 ? LengthOfMinute.ToString() : string.Empty), (LengthOfMinute > 1 ? LengthOfMinute.ToString() : "T"));
                    break;
                case TimerType.AMRAP:
                    title = "AMRAP";
                    break;
            }
            MainWinTitle = title;
        }


        private bool CanStopTimer()
        {
            return (TimerState == TimerState.Started || TimerState == TimerState.Paused);
        }

        private void StopTimer()
        {
            TheTimer.Stop();
            TimerState = TimerState.Stopped;
            TimerDisplayForeground = GrayBrush;
        }

        private bool CanStartTimer()
        {
            //Debug.WriteLine("CanStartTimer: {0}", (TimerState == TimerState.Stopped || TimerState == TimerState.Paused);
            return (TimerState == TimerState.Stopped || TimerState == TimerState.Paused);
        }

        private bool CanPauseTimer()
        {
            return TimerState == TimerState.Started;
        }

        private void PauseTimer()
        {
            //throw new NotImplementedException();
            TheTimer.Stop();
            TimerState = TimerState.Paused;
            TimerDisplayForeground = BlueBrush;
        }

        private TimeSpan timeSpan;

        private void ExecuteStartTimer()
        {
            if (TimerState == TimerState.Stopped)
            {
                WhichMinute = ThisThatMin.ThisMinute;
                timeSpan = new TimeSpan(0, TotalTime, 0);
                TheMinute = TotalTime;
            }
            runTimer(timeSpan);
        }


        private readonly string timerFormatString = @"m\:ss";

        private void runTimer(TimeSpan ts)
        {
            TheTimer.Stop();

            TimerDisplayForeground = BlackBrush;
            TimerText = ts.ToString(timerFormatString);

            TimerState = TimerState.Started;
            TheTimer.Start();
        }


        /// <summary>
        /// The <see cref="TimerState" /> property's name.
        /// </summary>
        public const string TimerStatePropertyName = "TimerState";

        private TimerState _timerState = TimerState.Stopped;

        /// <summary>
        /// Sets and gets the TimerState property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TimerState TimerState
        {
            get { return _timerState; }

            set
            {
                if (_timerState == value)
                {
                    return;
                }

                //RaisePropertyChanging(TimerStatePropertyName);
                _timerState = value;
                RaisePropertyChanged(TimerStatePropertyName);

                //StartTimerCmd.RaiseCanExecuteChanged();
                //StopTimerCmd.RaiseCanExecuteChanged();
                //PauseTimerCmd.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// The <see cref="TimerType" /> property's name.
        /// </summary>
        public const string TimerTypePropertyName = "TimerType";

        private TimerType _timerType = TimerType.EMOTM;

        /// <summary>
        /// Sets and gets the TimerType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TimerType TimerType
        {
            get
            {
                return _timerType;
            }

            set
            {
                if (_timerType == value)
                {
                    return;
                }

                // RaisePropertyChanging(TimerTypePropertyName);
                _timerType = value;
                RaisePropertyChanged(TimerTypePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TheMinute" /> property's name.
        /// </summary>
        public const string TheMinutePropertyName = "TheMinute";

        private int _theMinute = 10;

        /// <summary>
        /// Sets and gets the TheMinute property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TheMinute
        {
            get
            {
                return _theMinute;
            }

            set
            {
                if (_theMinute == value)
                {
                    return;
                }

                //RaisePropertyChanging(TheMinutePropertyName);
                _theMinute = value;
                RaisePropertyChanged(TheMinutePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WhichMinute" /> property's name.
        /// </summary>
        public const string WhichMinutePropertyName = "WhichMinute";

        private ThisThatMin _whichMinute = ThisThatMin.ThisMinute;

        /// <summary>
        /// Sets and gets the WhichMinute property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ThisThatMin WhichMinute
        {
            get { return _whichMinute; }

            set
            {
                if (_whichMinute == value)
                {
                    return;
                }

                // RaisePropertyChanging(WhichMinutePropertyName);
                _whichMinute = value;
                RaisePropertyChanged(WhichMinutePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="StartBtnCaption" /> property's name.
        /// </summary>
        public const string StartBtnCaptionPropertyName = "StartBtnCaption";

        private string _startBtnCaption = "Start";


        /// <summary>
        /// Sets and gets the StartBtnCaption property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string StartBtnCaption
        {
            get
            {
                return _startBtnCaption;
            }

            set
            {
                if (_startBtnCaption == value)
                {
                    return;
                }

                _startBtnCaption = value;
                RaisePropertyChanged(StartBtnCaptionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ListCnt" /> property's name.
        /// </summary>
        public const string ListCntPropertyName = "ListCnt";

        private ListCntEnums _listCnt = ListCntEnums.Two;

        /// <summary>
        /// Sets and gets the ListCnt property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ListCntEnums ListCnt
        {
            get { return _listCnt; }

            set
            {
                if (_listCnt == value)
                {
                    return;
                }

                _listCnt = value;
                RaisePropertyChanged(ListCntPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalTime" /> property's name.
        /// </summary>
        public const string TotalTimePropertyName = "TotalTime";

        private int _totalTime = 10;

        /// <summary>
        /// Sets and gets the TotalTime property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int TotalTime
        {
            get { return _totalTime; }

            set
            {
                if (_totalTime == value)
                {
                    return;
                }

                _totalTime = value;
                if (!TheTimer.Enabled)
                {
                    TimerText = string.Format("{0}:00", TotalTime);
                    TheMinute = TotalTime;
                }
                RaisePropertyChanged(TotalTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LengthOfMinute" /> property's name.
        /// </summary>
        public const string LengthOfMinutePropertyName = "LengthOfMinute";

        private int _lengthOfMinute = 1;

        /// <summary>
        /// Sets and gets the LengthOfMinute property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int LengthOfMinute
        {
            get
            {
                return _lengthOfMinute;
            }

            set
            {
                if (_lengthOfMinute == value)
                {
                    return;
                }

                _lengthOfMinute = value;
                RaisePropertyChanged(LengthOfMinutePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WindowState" /> property's name.
        /// </summary>
        public const string WindowStatePropertyName = "WindowState";

        private WindowState _windowState = WindowState.Normal;

        /// <summary>
        /// Sets and gets the WindowState property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public WindowState WindowState
        {
            get { return _windowState; }

            set
            {
                if (_windowState == value)
                {
                    return;
                }

                _windowState = value;
                RaisePropertyChanged(WindowStatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MainWinTitle" /> property's name.
        /// </summary>
        public const string MainWinTitlePropertyName = "MainWinTitle";

        private string _mainWinTitle = "EMOTM";

        /// <summary>
        /// Sets and gets the MainWinTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string MainWinTitle
        {
            get
            {
                return _mainWinTitle;
            }

            set
            {
                if (_mainWinTitle == value)
                {
                    return;
                }

                _mainWinTitle = value;
                RaisePropertyChanged(MainWinTitlePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DoTenSecondCountdown" /> property's name.
        /// </summary>
        public const string DoTenSecondCountdownPropertyName = "DoTenSecondCountdown";

        private bool _doTenSecCountdown;

        /// <summary>
        /// Sets and gets the DoTenSecondCountdown property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool DoTenSecondCountdown
        {
            get { return _doTenSecCountdown; }

            set
            {
                if (_doTenSecCountdown == value)
                {
                    return;
                }

                _doTenSecCountdown = value;
                RaisePropertyChanged(DoTenSecondCountdownPropertyName);
            }
        }

        private readonly SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);
        private readonly SolidColorBrush OrangeBrush = new SolidColorBrush(Colors.DarkOrange);
        private readonly SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
        private readonly SolidColorBrush BlueBrush = new SolidColorBrush(Colors.Blue);
        private readonly SolidColorBrush GrayBrush = new SolidColorBrush(Colors.LightGray);

        /// <summary>
        /// The <see cref="TimerDisplayForeground" /> property's name.
        /// </summary>
        public const string TimerDisplayForegroundPropertyName = "TimerDisplayForeground";

        private Brush _timerDisplayBrush = new SolidColorBrush(Colors.Black);


        /// <summary>
        /// Sets and gets the TimerDisplayForeground property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Brush TimerDisplayForeground
        {
            get { return _timerDisplayBrush; }

            set
            {
                if (_timerDisplayBrush.Equals(value))
                {
                    return;
                }

                _timerDisplayBrush = value;
                RaisePropertyChanged(TimerDisplayForegroundPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TimerText" /> property's name.
        /// </summary>
        public const string TimerTextPropertyName = "TimerText";

        private string _timerText = "0:00";

        /// <summary>
        /// Sets and gets the TimerText property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string TimerText
        {
            get { return _timerText; }

            set
            {
                if (_timerText == value)
                {
                    return;
                }

                _timerText = value;
                RaisePropertyChanged(TimerTextPropertyName);
            }
        }

        private Timer _theTimer;

        protected Timer TheTimer
        {
            get
            {
                if (_theTimer == null)
                {
                    _theTimer = new Timer() { Interval = 1000 };
                    _theTimer.Elapsed += TheTimerOnElapsed;
                }
                return _theTimer;
            }
        }

        private readonly TimeSpan oneSecond = new TimeSpan(0, 0, 1);

        private void TheTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            // Use this to avoid comparing the time on every tick when AutoMinimize is true.

            timeSpan = timeSpan.Subtract(oneSecond);
            TimeSpan now = timeSpan;
            
            TimerText = now.ToString(timerFormatString);
            // System.Diagnostics.Debug.WriteLine(string.Format("\t\t{0}", now.ToString()));

            if (now.Seconds == 0)
            {
                if (now.Minutes == 0)
                {
                    // timerExpired();
                    TheTimer.Stop();
                }
                else
                {
                    Debug.Write(string.Format("now.Minutes : {0}\n", now.Minutes));
                    bool isEndOfMinute = ((now.Minutes) % LengthOfMinute == 0);
                    TheMinute = now.Minutes; // + 1;
                    TimerDisplayForeground = BlackBrush;
                    if (isEndOfMinute)
                    {
                        switch (ListCnt)
                        {
                            case ListCntEnums.One:
                                break;
                            case ListCntEnums.Two:
                                WhichMinute = _whichMinute == ThisThatMin.ThisMinute
                                    ? ThisThatMin.ThatMinute
                                    : ThisThatMin.ThisMinute;
                                break;
                            case ListCntEnums.Three:
                                if (WhichMinute == ThisThatMin.ThisMinute)
                                {
                                    WhichMinute = ThisThatMin.ThatMinute;
                                }
                                else if (WhichMinute == ThisThatMin.ThatMinute)
                                {
                                    WhichMinute = ThisThatMin.TheOtherMinute;
                                }
                                else if (WhichMinute == ThisThatMin.TheOtherMinute)
                                {
                                    WhichMinute = ThisThatMin.ThisMinute;
                                }
                                break;
                        }
                    }
                }
            }
            else if (now.Seconds <= 5)
            {
                TimerDisplayForeground = RedBrush;
            }
            else if (now.Seconds <= 10)
            {
                TimerDisplayForeground = OrangeBrush;
            }
        }

        public RelayCommand StartTimerCmd { get; private set; }

        public RelayCommand StopTimerCmd { get; private set; }

        public RelayCommand PauseTimerCmd { get; private set; }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}
