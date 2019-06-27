using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDROID_TARPG
{
    public class CharAttribute
    {
        private String attributeName { get; }
        public String Name { get { return attributeName; } }
        private int attributeValue { get; set; }
        public int Value { get { return attributeValue; } }

        public CharAttribute(String Name, int Value)
        {
            this.attributeName = Name;
            this.attributeValue = Value;
        }
    }
}
