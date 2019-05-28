using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CharacterCreation
{
    public class Skill
    {
        private String Name { get; }
        private int Value { get; set; }

        public Skill(String Name, int Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public Skill(String Name)
        {
            this.Name = Name;
            Value = 0;
        }
    }
}
