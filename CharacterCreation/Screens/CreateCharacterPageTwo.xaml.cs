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

namespace RPGproject
{
    public sealed partial class CreateCharacterPageTwo : Page
    {
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
                Debug.WriteLine("Strength: " + Strength.Text);
                Debug.WriteLine("Dexterity: " + Dexterity.Text);
                Debug.WriteLine("Constitution: " + Constitution.Text);
                Debug.WriteLine("Intelligence: " + Intelligence.Text);
                Debug.WriteLine("Wisdom: " + Wisdom.Text);
                Debug.WriteLine("Charisma: " + Charisma.Text);
            }
        }      
        private void BackButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateCharacterPageOne));
        }

        private void Strength_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void Dexterity_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void Constitution_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void Intelligence_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void Wisdom_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void Charisma_BeforeChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
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
    }
}
