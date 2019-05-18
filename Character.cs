using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    struct HitDice
    {
        int Amount;
        String DiceType;
    }
    class Character : Entity
    {
        private Class CharacterClass { get; set; }
        private Race CharacterRace { get; set; }
        private String PlayerName { get; set; }
        private String Alignment { get; set; }
        private int ExperiencePoints { get; set; } = 0;
        private bool Inspiration { get; set; }
        private int Level { get; set; } = 1;
        private List<HitDice> HitDieAvailable = new List<HitDice>();
        private List<HitDice> HitDieTotal = new List<HitDice>();
        private byte DeathSavesFailures { get; set; } = 0;
        private byte DeathSavesSuccesses { get; set; } = 0;
        private List<String> OtherProficiencies { get; } = new List<String>();
        private String PersonalityTraits { get; set; }
        private String Ideals { get; set; }
        private String Bonds { get; set; }
        private String Flaws { get; set; }
        private int Age { get; set; }
        private float Height { get; set; }
        private float Weight { get; set; }
        private String Appearance { get; set; }
        private String CharacterBackstory { get; set; }
        //private List<Spellcaster> SpellcastingClasses { get; set; } = new List<Spellcaster>();
        private List<Feat> Feats { get; set; } = new List<Feat>();

        public Character(string Name, int HitPointMax, int ArmorClass, SavingThrow SavingThrows, List<Skill> Skills, List<Attribute> Attributes, int ProficiencyBonus,
            int PassiveWisdom, List<string> LanguagesSpoken, List<Condition> ActiveConditions, string Traits, string Notes, Class CharacterClass, Race CharacterRace, String PlayerName, String Alignment,
            String PersonalityTraits, String Ideals, String Bonds, String Flaws, int Age, float Height, float Weight, String Appearance,
            String CharacterBackstory, List<Feat> Feats) : base(Name, HitPointMax, ArmorClass, SavingThrows, Skills, Attributes, ProficiencyBonus, PassiveWisdom, LanguagesSpoken, ActiveConditions,
            Traits, Notes)
        {
            this.CharacterClass = CharacterClass;
            this.CharacterRace = CharacterRace;
            this.PlayerName = PlayerName;
            this.PersonalityTraits = PersonalityTraits;
            this.Ideals = Ideals;
            this.Bonds = Bonds;
            this.Flaws = Flaws;
            this.Age = Age;
            this.Height = Height;
            this.Weight = Weight;
            this.Appearance = Appearance;
            this.Alignment = Alignment;
            this.Inspiration = false;
            this.CharacterBackstory = CharacterBackstory;
            this.Feats = Feats;
        }
    }
}
