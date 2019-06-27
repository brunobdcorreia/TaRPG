using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDROID_TARPG
{
    public class Feat
    {
        private String Name { get; }
        private String Description { get; }
        
        public Feat(String Name, String Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
}
