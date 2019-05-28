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


namespace RPGproject.Source.CharacterCreation
{    
    public sealed partial class CreateCharacterPageOne : Page
    {
        //private List<String> characterRaces = new List<String>();
        //private List<String> characterClasses = new List<String>();
        CharacterModel charModel = new CharacterModel();
        public CreateCharacterPageOne()
        {
            this.InitializeComponent();
            StandardLoader loader = new StandardLoader();
            loader.LoadStandardValues();
            this.RaceSelector.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = loader.GetCharRaces() });
            this.ClassSelector.SetBinding(ComboBox.ItemsSourceProperty, new Binding() { Source = loader.GetCharClasses() });
        }

        private void CharacterAge_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);
        }

        private void CharacterHeightFeet_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);
        }
        private void CharacterHeightInches_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            PreventNonNumericInput(args);

            try
            {
                int newValue = int.Parse(args.NewText);

                if (newValue > 11)
                {
                    newValue = 11;
                    CharacterHeightInches.Text = newValue.ToString();
                }

                else if (newValue < 0)
                {
                    newValue = 0;
                    CharacterHeightInches.Text = newValue.ToString();
                }
            }

            catch (System.FormatException ex)
            {
                args.Cancel = true;
            }
        }

        private void CharacterWeight_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            foreach(char c in args.NewText)
            {
                if(!char.IsDigit(c) && c != '.')
                    args.Cancel = true;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

            if (CharacterName.Text == "" || CharacterName.Text[0] == ' ' || CharacterAge.Text == "" || CharacterWeight.Text == "" || (CharacterHeightFeet.Text == "0"
            && CharacterHeightInches.Text == "0"))
            {
                DisplayBlankValueWarning();
                return;
            }

            if (!VerifyWeightValue())
                return;

            SetCharacterPhysicalAttributes();
            this.Frame.Navigate(typeof(CreateCharacterPageTwo));
        }

        private void SetCharacterPhysicalAttributes()
        {
            CharacterModel.GetCharacterModel.Name = CharacterName.Text;

            String selectedClass = ClassSelector.SelectedItem as String;
            String selectedRace = RaceSelector.SelectedItem as String;

            foreach (Class c in StandardLoader.Classes)
            {
                if (selectedClass.Equals(c.Name))
                {
                    CharacterModel.GetCharacterModel.CharacterClass = c;
                    break;
                }
            }

            foreach (Race r in StandardLoader.Races)
            {
                if (selectedRace.Equals(r.Name))
                {
                    CharacterModel.GetCharacterModel.CharacterRace = r;
                    break;
                }
            }

            try
            {
                CharacterModel.GetCharacterModel.Age = int.Parse(CharacterAge.Text);
            }

            catch (FormatException ex)
            {
                DisplayBlankValueWarning();
            }

            CharacterModel.GetCharacterModel.Height = CharacterHeightFeet.Text + "feet, " + CharacterHeightInches.Text + "inches";

            try
            {
                double weightValue = double.Parse(CharacterWeight.Text);
                CharacterModel.GetCharacterModel.Weight = weightValue;
            }

            catch (System.FormatException ex)
            {
                Debug.WriteLine(ex);
                return;
            }
            }

        private bool VerifyWeightValue()
        {
            int punctuated = 0;

            foreach (char c in CharacterWeight.Text)
            {
                if (c == '.')
                    punctuated++;

                if (!char.IsDigit(c) && c != '.' || punctuated >= 2 || CharacterWeight.Text[0] == '.')
                {
                    DisplayInvalidWeightValueWarning();
                    return false;
                }
            }

            return true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayCancelCharacterCreationConfirmation();         
        }

        private async void DisplayBlankValueWarning()
        {

            ContentDialog blankValue = new ContentDialog
            {
                Title = "One or more fields have invalid values or no values.",
                Content = "All fields must have a valid value.",
                CloseButtonText = "Ok."
            };

            ContentDialogResult result = await blankValue.ShowAsync(); 
        }

        private async void DisplayInvalidWeightValueWarning()
        {
            ContentDialog invalidWeightValue = new ContentDialog
            {
                Title = "Invalid \"Weight\" value",
                Content = "Weight must be written as a numerical value, using a single comma or a dot",
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

        private void PlusFeet_Click(object sender, RoutedEventArgs e)
        {
            IncrementFeet();
        }
        private void PlusFeetCallback(object sender, PointerRoutedEventArgs e)
        {
            PlusFeet_Click(sender, new RoutedEventArgs());
        }

        private void MinusFeet_Click(object sender, RoutedEventArgs e)
        {
            DecrementFeet();
        }

        private void MinusFeetCallback(object sender, PointerRoutedEventArgs e)
        {
            MinusFeet_Click(sender, new RoutedEventArgs());
        }

        private void PlusInches_Click(object sender, RoutedEventArgs e)
        {
            IncrementInch();
        }

        private void PlusInchCallback(object sender, PointerRoutedEventArgs e)
        {
            PlusInches_Click(sender, new RoutedEventArgs());
        }

        private void MinusInches_Click(object sender, RoutedEventArgs e)
        {
            DecrementInch();
        }
        private void MinusInchCallback(object sender, PointerRoutedEventArgs e)
        {
            MinusInches_Click(sender, new RoutedEventArgs());
        }

        private void PreventNonNumericInput(TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void IncrementFeet()
        {
            int newValue = int.Parse(CharacterHeightFeet.Text) + 1;

            if (newValue >= 0)
                CharacterHeightFeet.Text = newValue.ToString();
        }

        private void DecrementFeet()
        {
            int newValue = int.Parse(CharacterHeightFeet.Text) - 1;

            if (newValue >= 0)
                CharacterHeightFeet.Text = newValue.ToString();
        }

        private void IncrementInch()
        {
            int newValue = int.Parse(CharacterHeightInches.Text) + 1;

            if (newValue > 11)
            { 
                newValue = 0;
                CharacterHeightInches.Text = newValue.ToString();
                IncrementFeet();
            }

            else if (newValue >= 0 && newValue <= 11)
                CharacterHeightInches.Text = newValue.ToString();
        }

        private void DecrementInch()
        {
            int newValue = int.Parse(CharacterHeightInches.Text) - 1;

            if (newValue >= 0)
                CharacterHeightInches.Text = newValue.ToString();
        }
    }
}
