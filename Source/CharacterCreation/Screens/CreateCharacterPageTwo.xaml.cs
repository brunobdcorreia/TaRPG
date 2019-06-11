using RPGproject.Source.UserData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPGproject.Source.CharacterCreation
{
    public sealed partial class CreateCharacterPageTwo : Page
    {
        private bool rolled = false;
        public CreateCharacterPageTwo()
        {
            this.InitializeComponent();            
        }

        private void NextButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (Strength.Text == "" || Dexterity.Text == "" || Constitution.Text == "" || Intelligence.Text == "" || Wisdom.Text == "" || Charisma.Text == "")
            {
                DisplayBlankValueWarning();               
            }

            else
            {
                RequestUserConfirmation();
            }
        }      

        private async void RequestUserConfirmation()
        {
            ContentDialog requestConfirmation = new ContentDialog()
            {
                Title = "Confirm character creation",
                Content = "Are you sure you want to create this character?",
                PrimaryButtonText = "Yes!",
                SecondaryButtonText = "No, wait!"
            };

            ContentDialogResult result = await requestConfirmation.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                List<CharAttribute> charAttributes = CreateAttributeList();

                if (charAttributes != null)
                    CharacterModel.GetCharacterModel.Attributes = charAttributes;
                else
                {
                    DisplayInvalidValueWarning();
                    return;
                }

                //CreatedCharacters.AddCharacter(CharacterModel.GetCharacterModel);                         

                CharacterDB.InsertCharacter(CharacterModel.GetCharacterModel);
                this.Frame.Navigate(typeof(MainPage));              
            }
        }

        private void BackButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateCharacterPageOne), CharacterModel.GetCharacterModel);
        }

        private void Strength_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);
        }

        private void Dexterity_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);
        }

        private void Constitution_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);
        }


        private void Intelligence_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);
        }

        private void Wisdom_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);
        }

        private void Charisma_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);
        }
        private static void PreventNonNumericInput(TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private async void DisplayInvalidValueWarning()
        {
            ContentDialog invalidValueDialog = new ContentDialog
            {
                Title = "Invalid value",
                Content = "One of the attribute values is invalid. Make sure the values are numerical and have no spaces or other characters surrounding them.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await invalidValueDialog.ShowAsync();
        }

        private async void DisplayBlankValueWarning()
        {
            ContentDialog blankValueDialog = new ContentDialog
            {
                Title = "Field is blank",
                Content = "All fields must have a numeric value.",
                CloseButtonText = "Ok."
            };

            ContentDialogResult result = await blankValueDialog.ShowAsync();
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

            if(result == ContentDialogResult.Primary)
            {             
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private async void DisplayRollOnceWarning()
        {
            ContentDialog rollOnceWarning = new ContentDialog
            {
                Title = "Already rolled attribute values",
                Content = "You can only roll attribute values once.",
                CloseButtonText = "Ok."
            };

            ContentDialogResult result = await rollOnceWarning.ShowAsync();
        }

        private void RollAttributes(object sender, RoutedEventArgs e)
        {
            if(rolled)
            {
                DisplayRollOnceWarning();
                return;
            }

            Random rand = new Random();
            List<int> rolls = new List<int>();
            List<int> results = new List<int>();
            int sum = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                    rolls.Add(rand.Next(1, 7));

                rolls.Sort();

                for (int k = 3; k >= 1; k--)
                    sum += rolls.ElementAt(k);

                results.Add(sum);
                rolls.Clear();
                sum = 0;
            }

            Strength.Text = results.ElementAt(0).ToString();
            Dexterity.Text = results.ElementAt(1).ToString();
            Constitution.Text = results.ElementAt(2).ToString();
            Intelligence.Text = results.ElementAt(3).ToString();
            Wisdom.Text = results.ElementAt(4).ToString();
            Charisma.Text = results.ElementAt(5).ToString();

            rolled = true;
            RollAttributesButton.Background = RollAttributesButton.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Gray);
        }

        private List<CharAttribute> CreateAttributeList()
        {
            try
            {
                CharAttribute strength = new CharAttribute("Strength", int.Parse(Strength.Text));
                CharAttribute dexterity = new CharAttribute("Dexterity", int.Parse(Dexterity.Text));
                CharAttribute constitution = new CharAttribute("Constitution", int.Parse(Constitution.Text));
                CharAttribute intelligence = new CharAttribute("Intelligence", int.Parse(Intelligence.Text));
                CharAttribute wisdom = new CharAttribute("Wisdom", int.Parse(Wisdom.Text));
                CharAttribute charisma = new CharAttribute("Charisma", int.Parse(Charisma.Text));

                List<CharAttribute> charAttributes = new List<CharAttribute>();

                charAttributes.Add(strength);
                charAttributes.Add(dexterity);
                charAttributes.Add(constitution);
                charAttributes.Add(intelligence);
                charAttributes.Add(wisdom);
                charAttributes.Add(charisma);

                return charAttributes;
            }

            catch
            {
                return null;
            }
        }
    }
}
