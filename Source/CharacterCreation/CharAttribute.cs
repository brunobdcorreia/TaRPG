using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CharacterCreation
{
    public class CharAttribute
    {
        private String Name { get; }
        private int Value { get; set; }

        public CharAttribute(String Name, int Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
