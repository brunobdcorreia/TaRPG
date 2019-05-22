using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.CharacterCreation
{
    class Class
    {
        private String Name { get; set; }
        private String Description { get; set; }
        private String HitDie { get; set; }
        private String PrimaryAbility { get; set; }
        private List<String> SavingThrowProficiencies { get; set; }
        private String ArmorAndWeaponProficiencies { get; set; }
        private String SubClass { get; set; }

        public Class(String Name, String Description, String HitDie, String PrimaryAbility, List<String> SavingThrowProficiencies, String ArmorAndWeaponProficiencies, String SubClass)
        {
            this.Name = Name;
            this.Description = Description;
            this.HitDie = HitDie;
            this.PrimaryAbility = PrimaryAbility;
            this.SavingThrowProficiencies = SavingThrowProficiencies;
            this.ArmorAndWeaponProficiencies = ArmorAndWeaponProficiencies;
            this.SubClass = SubClass;
        }
    }
}
