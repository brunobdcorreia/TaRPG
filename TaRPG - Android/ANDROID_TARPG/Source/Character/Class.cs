using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANDROID_TARPG
{
    public class Class
    {
        private string iconPath;
        public string ClassIcon { get { return iconPath; } }
        private String name;
        public String Name { get { return name; } set { name = value; } }
        private String Description { get; set; }
        private String HitDie { get; set; }
        private String PrimaryAbility { get; set; }
        private List<String> SavingThrowProficiencies { get; set; }
        private String ArmorAndWeaponProficiencies { get; set; }
        private String SubClass { get; set; }

        public Class(String name, String Description, String HitDie, String PrimaryAbility, List<String> SavingThrowProficiencies, String ArmorAndWeaponProficiencies, String SubClass)
        {
            this.name = name;
            this.Description = Description;
            this.HitDie = HitDie;
            this.PrimaryAbility = PrimaryAbility;
            this.SavingThrowProficiencies = SavingThrowProficiencies;
            this.ArmorAndWeaponProficiencies = ArmorAndWeaponProficiencies;
            this.SubClass = SubClass;
        }

        public Class(String name, string path)
        {
            this.name = name;
            iconPath = path;
        }
    }
}
