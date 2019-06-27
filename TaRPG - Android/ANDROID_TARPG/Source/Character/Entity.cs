using System;
using System.Collections.Generic;

namespace ANDROID_TARPG
{
    public abstract class Entity
    {
        private String name;
        public String Name { get { return name; } set { name = value; } }
        private int CurrentHitPoints { get; set; }
        private int HitPointMax { get; set; }
        private int ArmorClass { get; set; }
        private SavingThrow SavingThrows;
        private List<Skill> Skills;
        private List<CharAttribute> attributes;
        public List<CharAttribute> Attributes { get { return attributes; } set { attributes = value; } }
        private int ProficiencyBonus { get; set; }
        private int PassiveWisdom { get; set; }
        private List<String> LanguagesSpoken { get; }
        private List<Condition> ActiveConditions { get; }
        private String Traits;
        private String Notes;

        public Entity(String Name, int HitPointMax, int ArmorClass, SavingThrow SavingThrows, List<Skill> Skills, List<CharAttribute> attributes, int ProficiencyBonus, int PassiveWisdom,
            List<String> LanguagesSpoken, List<Condition> ActiveConditions, String Traits, String Notes)
        {
            this.Name = Name;
            this.CurrentHitPoints = this.HitPointMax = HitPointMax;
            this.ArmorClass = ArmorClass;
            this.SavingThrows = SavingThrows;
            this.ProficiencyBonus = ProficiencyBonus;
            this.PassiveWisdom = PassiveWisdom;
            this.LanguagesSpoken = LanguagesSpoken;
            this.Skills = Skills;
            this.attributes = attributes;
            this.ActiveConditions = null;
            this.Traits = Traits;
            this.Notes = Notes;
        }

        protected Entity()
        {

        }
    }
}