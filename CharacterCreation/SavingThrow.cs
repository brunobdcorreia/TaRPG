using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.CharacterCreation
{
    public class SavingThrow
    {
        private String AttributeName { get; set; }

        public SavingThrow(String AttributeName)
        {
            this.AttributeName = AttributeName;
        }
    }
}
