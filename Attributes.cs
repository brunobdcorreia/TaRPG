using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class Attributes
    {
        private String Name { get; }
        private int Value { get; set; }

        public Attributes(String Name, int Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
