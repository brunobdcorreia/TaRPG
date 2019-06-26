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
    public partial class CreatedCharactersPage : ContentPage
    {
        private List<Character> Characters;
        public CreatedCharactersPage()
        {
            InitializeComponent();
            CharacterDB.RecoverCharacters();
            Characters = CreatedCharacters.UserCharacters;
            CreatedCharactersView.ItemsSource = Characters;
            if (Characters.Count == 0)
            {
                NoCharacters.Text = "You haven\'t created any characters yet!";
            }

        }
        private async void CharacterSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Character selectedCharacter = e.SelectedItem as Character;
            await Navigation.PushAsync(new ViewedCharacterPage(selectedCharacter));

        }
    }
}