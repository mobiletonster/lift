using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Lift.Models
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        private int _hours;
        private int _days;
        private int _food;
        private int _shelter;
        private int _happiness;
        private int _stones;
        private int _airStones;
        private int _population;
        private string _ritual;

        public int Hours { get { return _hours; } set { _hours = value; OnPropertyChanged("Hours"); }}
        public int Days { get { return _days; } set { _days = value; OnPropertyChanged("Days");  }}
        public int Food { get { return _food; } set { _food = value;  OnPropertyChanged("Food"); }}
        public int Shelter { get { return _shelter;  } set { _shelter = value;  OnPropertyChanged("Shelter"); }}
        public int Happiness { get { return _happiness; } set { _happiness = value; OnPropertyChanged("Happiness"); }}
        public int Stones { get { return _stones; } set { _stones = value; OnPropertyChanged("Stones"); }}
        public int AirStones { get { return _airStones; } set { _airStones = value;  OnPropertyChanged("AirStones"); }}
        public int Population { get { return _population; } set { _population = value;  OnPropertyChanged("Population"); }}
        public string Ritual { get { return _ritual; } set { _ritual = value; OnPropertyChanged("Ritual"); } }
        #endregion
        private int maxFood;
        private int maxHappiness;
        private int maxShelter;

        private DispatcherTimer GameTimer;
       
        public GameViewModel()
        {
            
        }

        public void StartGame()
        {
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            GameTimer = new DispatcherTimer();
            GameTimer.Tick += GameTimer_Tick;
            GameTimer.Interval = new TimeSpan(0, 0, 0, 2, 500); // right now, the timer fires every second.
            GameTimer.Start();
            Initialize();
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            string runeValue;
            switch (args.VirtualKey)
            {
                case VirtualKey.Q:
                    runeValue = "1";
                    break;
                case VirtualKey.W:
                    runeValue = "2";
                    break;
                case VirtualKey.E:
                    runeValue = "3";
                    break;
                case VirtualKey.A:
                    runeValue = "4";
                    break;
                case VirtualKey.D:
                    runeValue = "5";
                    break;
                case VirtualKey.Z:
                    runeValue = "6";
                    break;
                case VirtualKey.X:
                    runeValue = "7";
                    break;
                case VirtualKey.C:
                    runeValue = "8";
                    break;
                default:
                    runeValue = "";
                    break;
            }
            if (!string.IsNullOrEmpty(runeValue))
            {
                CaptureRuneClick(runeValue);
            }
        }


        private void GameTimer_Tick(object sender, object e)
        {
            Hours++;
            HourlyEvents();
            if (Hours == 24)
            {
                Days++;
                Hours = 0;
        }
        }

        public ICommand RuneClicked
        {
            get
            {
                return new DelegateCommand((rune) =>
                {
                    CaptureRuneClick((string)rune);
                });
            }
        }

        private void CaptureRuneClick(string rune)
        {
            Ritual += rune;
        }
        private void HourlyEvents()
        {
            Food -= Population/12;
            Happiness -= Population/12;
            Shelter -= Population/12;
            maxFood = Population * 10;
            maxShelter = Population * 10;
            maxHappiness = Population * 10;
        }
        private void Initialize()
        {
            Hours = 0;
            Days = 0;
            Population = 100;
            Food = 1000;
            Shelter = 1000;
            Happiness = 1000;
        }
        #region PropertyChangeStuff
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
