using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ANDROID_TARPG
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewedCharacterPage : ContentPage
    {
        private static Character character;
        public ViewedCharacterPage(Character a)
        {
            character = a;
            InitializeComponent();
            InitializeInfo();
        }

        private void InitializeInfo()
        {
            List<int> attMods = character.AttributeModifiers;

            CharacterName.Text ="Name: " + character.Name;
            CharacterAge.Text = "Age: " + character.Age.ToString();
            CharacterRace.Text = "Race: " +  character.CharacterRace.Name;
            CharacterClass.Text = "Class: " + character.CharacterClass.Name;
            CharacterHeight.Text = "Height: " + character.Height;
            CharacterWeight.Text = "Weight: " + character.GetFormattedWeight;
            StrengthAtt.Text = "Strength: " + character.Attributes.Find(x => x.Name == "Strength").Value.ToString() + "(" + attMods.ElementAt(0).ToString() + ")";
            DexterityAtt.Text = "Dexterity: " + character.Attributes.Find(x => x.Name == "Dexterity").Value.ToString() + "(" + attMods.ElementAt(1).ToString() + ")";
            ConstitutionAtt.Text = "Constitution: " + character.Attributes.Find(x => x.Name == "Constitution").Value.ToString() + "(" + attMods.ElementAt(2).ToString() + ")";
            IntelligenceAtt.Text = "Intelligence: " + character.Attributes.Find(x => x.Name == "Intelligence").Value.ToString() + "(" + attMods.ElementAt(3).ToString() + ")";
            WisdomAtt.Text = "Wisdom: " + character.Attributes.Find(x => x.Name == "Wisdom").Value.ToString() + "(" + attMods.ElementAt(4).ToString() + ")";
            CharismaAtt.Text = "Charisma: " + character.Attributes.Find(x => x.Name == "Charisma").Value.ToString() + "(" + attMods.ElementAt(5).ToString() + ")";

        }

        private async void DeleteCharacter(object sender, EventArgs e)
        {
            var result = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to delete this character? This cannot be undone.", "Delete character", "Yes!", "No");

            if (result)
            {
                CharacterDB.DeleteCharacterbyID(character);
                await Navigation.PopToRootAsync();
            }
        }

        private async void EditCharacter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage(character));
        }

        private async void SendCharacter(object sender, EventArgs e)
        {
            var result = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to send this character?", "Send character", "Yes!", "No");

            if (result)
            {
                CharacterDB.SendCharacter(character);
                await UserDialogs.Instance.AlertAsync("Sended");
            }
        }



    }
}