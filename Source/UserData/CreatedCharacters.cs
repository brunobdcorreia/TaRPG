using RPGproject.Source.CharacterCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.UserData
{
    class CreatedCharacters
    {
        private static List<Character> userCharacters = new List<Character>();
        public static List<Character> UserCharacters { get { return userCharacters; } }

        public static void AddCharacter(Character character)
        {
            foreach(Character c in userCharacters)
            {
                if (character.Name.Equals(c.Name))
                    return;
            }

            userCharacters.Add(character);
        }

        public static void DeleteCharacter(Character character)
        {
            userCharacters.Remove(character);
        }
    }
}
