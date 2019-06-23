using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDROID_TARPG
{
    public class Attributes
    {
        private string Name { get; }
        private int Value { get; set; }

        public Attributes(string Name, int Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
