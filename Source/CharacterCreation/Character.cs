using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CharacterCreation
{
    /*struct ce
    {
        int Amount;
        string DiceType;
    }*/
    public class Character : Entity
    {
        [SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement]
        public int characterID { get; set; }
        private Class characterClass;
        //FirstDescription and SecondDescription are used when describing the characters on the CreatedCharactersPage.
        public string FirstDescription { get { return this.Name + ", " + this.Age; } }
        public string SecondDescription { get { return this.CharacterRace.Name + " " + this.characterClass.Name; } }
        public Class CharacterClass { get { return characterClass; } set { characterClass = value; } }
        [SQLite.Net.Attributes.Ignore]
        private Race characterRace { get; set; }
        public Race CharacterRace { get { return characterRace; } set { characterRace = value; } }
        private string playerName;
        public string PlayerName { get { return playerName; } set { playerName = value; } }
        private string alignment;
        public string Alignment { get { return alignment; } set { alignment = value; } }
        private int experiencePoints;
        public int ExperiencePoints { get { return experiencePoints; } set { experiencePoints = value; } }
        private bool Inspiration { get; set; }
        private int level;
        public int Level { get { return level; } set { level = value; } }
        [SQLite.Net.Attributes.Ignore]
     // private List<HitDice> HitDieAvailable { get; set; } = new List<HitDice>();
     // [SQLite.Net.Attributes.Ignore]
     // private List<HitDice> HitDieTotal { get; set; } = new List<HitDice>();
        private byte DeathSavesFailures { get; set; } = 0;
        private byte DeathSavesSuccesses { get; set; } = 0;
        private List<string> OtherProficiencies { get; } = new List<string>();
        private string PersonalityTraits { get; set; }
        private string Ideals { get; set; }
        private string Bonds { get; set; }
        private string Flaws { get; set; }
        private int age;
        public int Age { get { return age; } set { age = value; } }
        private string height;
        public string Height { get { return height; } set { height = value; } }
        private double weight;
        public double Weight { get { return weight; } set { weight = value; } }
        public string GetFormattedWeight { get { return weight + " lbs"; } }
        public string Appearance { get; set; }
        public string CharacterBackstory { get; set; }


        /********/
        public string getCharacterBackstory()
        {
            return CharacterBackstory;
        }
        public string getApp()
        {
            return Appearance;
        }
     
        /***/
        //private List<Spellcaster> SpellcastingClasses { get; set; } = new List<Spellcaster>();
        [SQLite.Net.Attributes.Ignore]
        private List<Feat> feats { get; set; } = new List<Feat>();
        public List<Feat> Feats { get { return feats; } set { feats = value; } }

        public Character(string Name, int HitPointMax, int ArmorClass, SavingThrow SavingThrows, List<Skill> Skills, List<CharAttribute> Attributes, int ProficiencyBonus,
            int PassiveWisdom, List<string> LanguagesSpoken, List<Condition> ActiveConditions, string Traits, string Notes, Class characterClass, Race characterRace, string playerName, string Alignment,
            string PersonalityTraits, string Ideals, string Bonds, string Flaws, int age, string height, float weight, string Appearance,
            string CharacterBackstory, List<Feat> Feats) : base(Name, HitPointMax, ArmorClass, SavingThrows, Skills, Attributes, ProficiencyBonus, PassiveWisdom, LanguagesSpoken, ActiveConditions,
            Traits, Notes)
        {
            this.characterClass = characterClass;
            this.characterRace = characterRace;
            this.playerName = playerName;
            this.PersonalityTraits = PersonalityTraits;
            this.level = 1;
            this.experiencePoints = 0;
            this.Ideals = Ideals;
            this.Bonds = Bonds;
            this.Flaws = Flaws;
            this.age = age;
            this.height = height;
            this.Weight = weight;
            this.Appearance = Appearance;
            this.Alignment = Alignment;
            this.Inspiration = false;
            this.CharacterBackstory = CharacterBackstory;
            this.Feats = Feats;
        }

        /***********/
        public Character(int ID, string Name, string Alignment, int Age, string Height, double Weight, string CharacterBackstory, int Strength, int Dexterity, int Constitution,
            int Intelligence, int Wisdom, int Charisma, string selectedClass, string selectedRace,int Experience, int Level)
        {
            this.characterID = ID;
            this.Name = Name;
            this.Alignment = Alignment;
            this.Age = Age;
            this.Height = Height;
            this.Weight = Weight;
            this.CharacterBackstory = CharacterBackstory;
            this.Attributes= new List<CharAttribute>();
            this.Attributes.Add(new CharAttribute("Strength", Strength));
            this.Attributes.Add(new CharAttribute("Dexterity", Dexterity));
            this.Attributes.Add(new CharAttribute("Constitution", Constitution));
            this.Attributes.Add(new CharAttribute("Intelligence", Intelligence));
            this.Attributes.Add(new CharAttribute("Wisdom", Wisdom));
            this.Attributes.Add(new CharAttribute("Charisma", Charisma));
            this.ExperiencePoints = Experience;
            this.Level = Level;

            StandardLoader loader = new StandardLoader();
            loader.LoadStandardValues();

            foreach (Class c in StandardLoader.Classes)
            {
                if (selectedClass.Equals(c.Name))
                {
                    this.CharacterClass = c;
                    break;
                }
            }

            foreach (Race r in StandardLoader.Races)
            {
                if (selectedRace.Equals(r.Name))
                {
                    this.CharacterRace = r;
                    break;
                }
            }

        }
        /******/

        public Character()
        {
            this.level = 1;
            this.ExperiencePoints = 0;
        }
    }
}
