using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CharacterCreation
{
    //This class is responsible for loading all the standard values for the player
    //to choose from when creating a character.
    class StandardLoader
    {
        //private List<Class> characterClasses;
        //private List<Race> characterRaces;
        private List<String> characterClasses;
        private List<String> characterRaces;
        private static List<Race> races;
        public static List<Race> Races { get { return races; } }
        private static List<Class> classes;
        public static List<Class> Classes { get { return classes; } }

        public StandardLoader()
        {
            characterClasses = new List<String>();
            characterRaces = new List<String>();
            races = new List<Race>();
            classes = new List<Class>();
        }

        private void LoadClasses()
        {
            classes.Add(new Class("Barbarian"));
            classes.Add(new Class("Bard"));
            classes.Add(new Class("Cleric"));
            classes.Add(new Class("Druid"));
            classes.Add(new Class("Fighter"));
            classes.Add(new Class("Monk"));
            classes.Add(new Class("Paladin"));
            classes.Add(new Class("Ranger"));
            classes.Add(new Class("Rogue"));
            classes.Add(new Class("Sorcerer"));
            classes.Add(new Class("Warlock"));
            classes.Add(new Class("Wizard")); 

            characterClasses.Add("Barbarian");
            characterClasses.Add("Bard");
            characterClasses.Add("Cleric");
            characterClasses.Add("Druid");
            characterClasses.Add("Fighter");
            characterClasses.Add("Monk");
            characterClasses.Add("Paladin");
            characterClasses.Add("Ranger");
            characterClasses.Add("Rogue");
            characterClasses.Add("Sorcerer");
            characterClasses.Add("Warlock");
            characterClasses.Add("Wizard");
        }

        private void LoadRaces()
        {
            races.Add(new Race("Dragonborn"));
            races.Add(new Race("Dwarf"));
            races.Add(new Race("Elf"));
            races.Add(new Race("Gnome"));
            races.Add(new Race("Half Elf"));
            races.Add(new Race("Half Orc"));
            races.Add(new Race("Halfling"));
            races.Add(new Race("Human"));
            races.Add(new Race("Tiefling")); 

            characterRaces.Add("Dragonborn");
            characterRaces.Add("Dwarf");
            characterRaces.Add("Elf");
            characterRaces.Add("Gnome");
            characterRaces.Add("Half Elf");
            characterRaces.Add("Half Orc");
            characterRaces.Add("Halfling");
            characterRaces.Add("Human");
            characterRaces.Add("Tiefling");
        }

        public void LoadStandardValues()
        {
            LoadClasses();
            LoadRaces();
        }

        public List<String> GetCharRaces()
        {
            return characterRaces;
        }

        public List<String> GetCharClasses()
        {
            return characterClasses;
        }

        public List<Race> GetRaces()
        {
            return races;
        }

        public List<Class> GetClasses()
        {
            return classes;
        }
    }
}
