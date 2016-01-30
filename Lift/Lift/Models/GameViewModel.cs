﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lift.Models
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _hours;
        private int _days;
        private int _food;
        private int _shelter;
        private int _happiness;
        private int _stones;
        private int _airStones;
        private int _population;

        public int Hours { get { return _hours; } set { _hours = value; OnPropertyChanged("Hours"); }}
        public int Days { get { return _days; } set { _days = value; OnPropertyChanged("Days");  }}
        public int Food { get { return _food; } set { _food = value;  OnPropertyChanged("Food"); }}
        public int Shelter { get { return _shelter;  } set { _shelter = value;  OnPropertyChanged("Shelter"); }}
        public int Happiness { get { return _happiness; } set { _happiness = value; OnPropertyChanged("Happiness"); }}
        public int Stones { get { return _stones; } set { _stones = value; OnPropertyChanged("Stones"); }}
        public int AirStones { get { return _airStones; } set { _airStones = value;  OnPropertyChanged("AirStones"); }}
        public int Population { get { return _population; } set { _population = value;  OnPropertyChanged("Population"); }}
        public GameViewModel()
        {
            
        }

        public void StartGame()
        {
            Hours = 50;
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