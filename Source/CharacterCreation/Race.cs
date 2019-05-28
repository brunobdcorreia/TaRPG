using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CharacterCreation
{
    public class Race
    {
        private String name { get; set; }
        public String Name { get { return name; } set { name = value; } }
        private String SubRace { get; set; }

        public Race(String name, String SubRace)
        {
            this.name = name;
            this.SubRace = SubRace;
        }

        public Race(String name)
        {
            this.name = name;
            SubRace = null;
        }

        public void CalculateModifiers()
        {

        }
    }
}
