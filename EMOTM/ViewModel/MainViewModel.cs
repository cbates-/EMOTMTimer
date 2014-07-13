using EMOTM.Infrastructure;
using GalaSoft.MvvmLight;
using EMOTM.Model;

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

                RaisePropertyChanging(TotalTimePropertyName);
                _totalTime = value;
                RaisePropertyChanged(TotalTimePropertyName);
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}