using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift.Models
{
    public class Calamities
    {
        Random rnd = new Random();
        private Village village;
        public Calamities(Village gameVillage)
        {
            village = gameVillage;
        }
        public string doCalamity()
        {
            string calamityText;
            int calamityType = rnd.Next(1,3);
            switch (calamityType)
            {
                case 1:
                    calamityText = Earthquake();
                    break;
                case 2:
                    calamityText = Hurricane();
                    break;
                default:
                    calamityText = "Default Calamity";
                    break;
            }
            return calamityText;
        }

        private string Earthquake()
        {
            int popDmg = (int)(10.0 * village.DiffMultiplier);
            int sheltDmg = (int)(500.0 * village.DiffMultiplier);
            village.Population -= (popDmg);
            village.Shelter -= (sheltDmg);
            return "Earthquake has hit. Population Loss: " + popDmg + " Shelter Damage: " + sheltDmg;
        }
        private string Hurricane()
        {
            int popDmg = (int)(20.0 * village.DiffMultiplier);
            int sheltDmg = (int)(300.0 * village.DiffMultiplier);
            int foodDmg = (int)(200.0 * village.DiffMultiplier);
            village.Population -= popDmg;
            village.Shelter -= sheltDmg;
            village.Food -= foodDmg;
            return "Hurricane has hit. Population Loss: " + popDmg + " Shelter Damage: " + sheltDmg + "Food Damage: " + foodDmg;
        }
    }
}
