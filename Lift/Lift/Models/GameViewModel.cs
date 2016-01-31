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
    public class GameViewModel : BaseModel
    {

        #region Properties
        private int _hours;
        private int _days;
        private int _food;
        private Village _gameVillage;
        private string _ritual;
        private List<string> _textLogList;
        private string _textLog;

        public int Hours { get { return _hours; } set { _hours = value; OnPropertyChanged(); }}
        public int Days { get { return _days; } set { _days = value; OnPropertyChanged();  }}
        public Village GameVillage { get { return _gameVillage; } set { _gameVillage = value; OnPropertyChanged(); } }
        public string Ritual { get { return _ritual; } set { _ritual = value; OnPropertyChanged(); } }
        public List<string> TextLogList { get { return _textLogList; } set { _textLogList = value;OnPropertyChanged(); } }
        public string TextLog { get { return _textLog; } set { _textLog = value;OnPropertyChanged(); } }
        #endregion

        private DispatcherTimer GameTimer;
        Random rnd = new Random();
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
        private void Initialize()
        {
            
            Hours = 0;
            Days = 0;
            GameVillage = new Village();
            GameVillage.Population = 100;
            GameVillage.Food = 100;
            GameVillage.Shelter = 100;
            GameVillage.Happiness = 50;
            TextLogList = new List<string>();
        }

        #region events
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
                case VirtualKey.S:
                    runeValue = "CAST";
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
            if (rune == "CAST")
            {
                CheckRitual(Ritual);
                Ritual = string.Empty;
            }
            else
            {
                Ritual += rune;
            }
        }
        private void HourlyEvents()
        {

            GameVillage.Population += (int)(GameVillage.Happiness/10.0);
            
            
            if (GameVillage.Food < GameVillage.Population)
            {
                GameVillage.Population -= (GameVillage.Population-GameVillage.Food);
                Log("Lost population not enough food");
            }
            GameVillage.Food -= GameVillage.Population / 24;
            GameVillage.Happiness -= (int)(1.0 * GameVillage.DiffMultiplier);
            GameVillage.Shelter -= (int)(10.0 * GameVillage.DiffMultiplier);
            GameVillage.Stones += GameVillage.Population / 100;
            int calamityChance = rnd.Next(1, (int)(24.0/GameVillage.DiffMultiplier));
            if (calamityChance ==1)
            {
                var calamity = new Calamities(GameVillage);
                Log(calamity.doCalamity());
                
            }
        }
        #endregion

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

        private void CheckRitual(string Ritual)
        {
            switch (Ritual) {
                case "123":
                    Log(GameVillage.ChangeFood(10));
                    break;
                case "475":
                    Log(GameVillage.ChangeHappiness(1));
                    break;
                case "425":
                    Log(GameVillage.ChangeShelter(10));
                    break;
                case "1345":
                    Log(GameVillage.ConvertStone());
                    break;
                default:
                    Log(Ritual + " is an Invalid Ritual");
                    break;
                }
        }

        private void Log(string message)
        {

            if (TextLogList.Count > 10)
            {
                TextLogList.Insert(0, message);
                TextLogList.Remove(TextLogList.Last());
            }else if(TextLogList.Count < 1)
            {
                TextLogList.Add(message);
            }
            else
            {
                TextLogList.Insert(0, message);
            }
            string textlog = string.Empty;
            foreach(var m in TextLogList)
            {
                textlog += m + "\n";
            }

            TextLog = textlog;
        }

    }
}
