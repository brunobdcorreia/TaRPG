using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.CharacterCreation
{
    public class Race
    {
        private String Name { get; set; }
        private String SubRace { get; set; }

        public Race(String Name, String SubClass)
        {
            this.Name = Name;
            this.SubRace = SubClass;
        }

        public Race(String Name)
        {
            this.Name = Name;
            SubRace = null;
        }

        public void CalculateModifiers()
        {

        }
    }
}
