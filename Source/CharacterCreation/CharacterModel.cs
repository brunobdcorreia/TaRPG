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
        public static Character GetCharacterModel { get { return characterModel; } }

        public CharacterModel()
        {
            characterModel = new Character();
        }
    }
}
