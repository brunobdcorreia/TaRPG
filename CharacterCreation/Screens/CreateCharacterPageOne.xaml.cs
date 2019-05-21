using System;
using System.Collections.Generic;
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

namespace RPGproject
{    
    public sealed partial class CreateCharacterPageOne : Page
    {
        public List<String> Races = new List<String>();
        public List<String> Classes = new List<String>();
        public CreateCharacterPageOne()
        {
            this.InitializeComponent();
            LoadRaces();
            LoadClasses();
        }

        private void LoadClasses()
        {
            Classes.Add("Barbarian");
            Classes.Add("Bard");
            Classes.Add("Cleric");
            Classes.Add("Druid");
            Classes.Add("Fighter");
            Classes.Add("Monk");
            Classes.Add("Paladin");
            Classes.Add("Ranger");
            Classes.Add("Rogue");
            Classes.Add("Sorcerer");
            Classes.Add("Warlock");
            Classes.Add("Wizard");
        }

        private void LoadRaces()
        {
            Races.Add("Dragonborn");
            Races.Add("Dwarf");
            Races.Add("Elf");
            Races.Add("Gnome");
            Races.Add("Half Elf");
            Races.Add("Half Orc");
            Races.Add("Halfling");
            Races.Add("Human");
            Races.Add("Tiefling");
        }

        private void CharacterAge_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void CharacterHeight_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(char c in CharacterWeight.Text)
            {
                if(!char.IsDigit(c) && c != '.' && c != ',')
                {
                    DisplayInvalidWeightValueWarning();
                    return;
                }
            }

            this.Frame.Navigate(typeof(CreateCharacterPageTwo));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayCancelCharacterCreationConfirmation();         
        }

        private async void DisplayInvalidWeightValueWarning()
        {
            ContentDialog invalidWeightValue = new ContentDialog
            {
                Title = "Invalid \"Weight\" value",
                Content = "Weight must be written as a numerical value, using a comma or a dot",
                CloseButtonText = "Ok."
            };

            ContentDialogResult result = await invalidWeightValue.ShowAsync();
        }

        private async void DisplayCancelCharacterCreationConfirmation()
        {
            ContentDialog cancelCharacterCreation = new ContentDialog
            {
                Title = "Cancel character creation?",
                Content = "Are you sure you want to cancel this character's creation?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            ContentDialogResult result = await cancelCharacterCreation.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
