using System.Windows;
using EMOTM.Infrastructure;
using EMOTM.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Timers;

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
                        return;
                    }
                });

            StartTimerCmd = new RelayCommand(() => ExecuteStartTimer());

            StopTimerCmd = new RelayCommand(() =>
            {
                TheTimer.Stop();
                TimerState = TimerState.Stopped;
            });

            TimerText = string.Format("{0}:00", TotalTime);
        }

        private TimeSpan timeSpan;

        private void ExecuteStartTimer()
        {
            WhichMinute = ThisThatMin.ThisMinute;
            timeSpan = new TimeSpan(0, TotalTime, 0);
            runTimer(timeSpan);
        }


        private readonly string timerFormatString = @"m\:ss";

        private void runTimer(TimeSpan ts)
        {
            TheTimer.Stop();

            TimerText = ts.ToString(timerFormatString);

            TimerState = TimerState.Started;
            TheTimer.Start();
        }

        /// <summary>
        /// The <see cref="TimerState" /> property's name.
        /// </summary>
        public const string TimerStatePropertyName = "TimerState";

        private TimerState _timerState = Infrastructure.TimerState.Stopped;

        /// <summary>
        /// Sets and gets the TimerState property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TimerState TimerState
        {
            get
            {
                return _timerState;
            }

            set
            {
                if (_timerState == value)
                {
                    return;
                }

                RaisePropertyChanging(TimerStatePropertyName);
                _timerState = value;
                RaisePropertyChanged(TimerStatePropertyName);
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
            get
            {
                return _whichMinute;
            }

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
            get
            {
                return _listCnt;
            }

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
            get
            {
                return _totalTime;
            }

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
                }
                RaisePropertyChanged(TotalTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WindowState" /> property's name.
        /// </summary>
        public const string WindowStatePropertyName = "WindowState";

        private WindowState _windowState = System.Windows.WindowState.Normal;

        /// <summary>
        /// Sets and gets the WindowState property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public WindowState WindowState
        {
            get
            {
                return _windowState;
            }

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
            get
            {
                return _timerText;
            }

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
            TimerText = timeSpan.ToString(timerFormatString);

            if (timeSpan.Seconds == 0)
            {
                if (timeSpan.Minutes == 0)
                {
                    // timerExpired();
                    TheTimer.Stop();
                }
                else
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

        public RelayCommand StartTimerCmd { get; private set; }

        public RelayCommand StopTimerCmd { get; private set; }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}