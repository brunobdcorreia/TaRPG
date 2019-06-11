using RPGproject.Source.CharacterCreation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPGproject.Source.UserData.Screens
{
    public sealed partial class CreatedCharactersPage : Page
    {
        private List<Character> Characters;
        //private static Character selectedCharacter;

        //public static Character GetSelectedCharacter()
        //{ return selectedCharacter; }
        public CreatedCharactersPage()
        {
            this.InitializeComponent();
            CharacterDB.RecoverCharacters();
            Characters = CreatedCharacters.UserCharacters;

            if (Characters.Count == 0)
            {
                NoCharacters.Text = "You haven\'t created any characters yet!";
            }

            else NoCharacters.Text = "";
        }

        private void CharacterGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Character selectedCharacter = (Character) e.ClickedItem;
            this.Frame.Navigate(typeof(CharacterSummary), selectedCharacter);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
