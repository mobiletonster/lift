using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift.Models
{
    public class Village: BaseModel
    {
        private int _food;
        private int _shelter;
        private int _happiness;
        private int _stones;
        private int _airStones;
        private int _population;

        public int Food { get { return _food; } set { _food = value; OnPropertyChanged("Food"); } }
        public int Shelter { get { return _shelter; } set { _shelter = value; OnPropertyChanged("Shelter"); } }
        public int Happiness { get { return _happiness; } set { _happiness = value; OnPropertyChanged("Happiness"); } }
        public int Stones { get { return _stones; } set { _stones = value; OnPropertyChanged("Stones"); } }
        public int AirStones { get { return _airStones; } set { _airStones = value; OnPropertyChanged("AirStones"); } }
        public int Population { get { return _population; } set { _population = value; OnPropertyChanged("Population"); } }
        public double DiffMultiplier { get { return _airStones / 25.0 + 1.0; } }
        public string ChangeFood(double change)
        {
            double adjusted = change * DiffMultiplier;
            Food += (int)adjusted;
            return "Added " + adjusted + " units of food.";
        }
        public string ChangeShelter(double change)
        {
            double adjusted = change * DiffMultiplier;

                Shelter += (int)adjusted;
                return "Added " + adjusted + " units of shelter.";

        }
        public string ChangeHappiness(double change)
        {
            double adjusted = change * DiffMultiplier;
            if (Happiness + adjusted <= 100)
            {
                Happiness += (int)adjusted;
                return "Added " + adjusted + " units of happiness.";
            }
            else
            {
                Happiness = 100;
                return "Villagers are sufficiently happy.";
            }
        }
        public string ConvertStone()
        {
            if (Stones >= 10)
            {
                Stones -= 10;
                AirStones += 1;
                return "10 Stones converted to 1 Air Stone.";
            }
            else
            {
                return "Not enough stones. To convert to Air Stone, you need at least 10 stones.";
            }
        }

    }
}
