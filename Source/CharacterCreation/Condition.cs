using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.CharacterCreation
{
    public class Condition
    {
        private String Name { get; }
        private String Duration { get; set; }
        private String Description { get; set; }

        public Condition(String Name, String Duration, String Description)
        {
            this.Name = Name;
            this.Duration = Duration;
            this.Description = Description;
        }
    }
}
