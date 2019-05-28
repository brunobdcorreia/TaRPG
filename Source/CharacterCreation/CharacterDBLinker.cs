using RPGproject.Source.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CharacterCreation
{
    public static class CharacterDBLinker
    {
        public static void InitializeDatabase()
        {
            CharacterDBAccess dbAccess = new CharacterDBAccess();
            dbAccess.CreateDatabase(CharacterDBAccess.DB_PATH);
        }
        public static void AddCharacter(Character character)
        {
            CharacterDBAccess dbAccess = new CharacterDBAccess();
            dbAccess.Insert(character);
        }
    }
}
