using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.CharacterCreation
{
    class CharacterModel
    {
        private static Character characterModel = null;
        public static Character GetCharacterModel { get { return characterModel; } set { characterModel = value; } }
        private static bool rolledAttributes = false;
        public static bool RolledAttributes { get { return rolledAttributes; } set { rolledAttributes = value; } }

        public CharacterModel()
        {
            characterModel = new Character();
        }
    }
}
