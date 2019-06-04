using RPGproject.Source.CharacterCreation;
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
using RPGproject.Source.UserData;
using System.Diagnostics;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPGproject.Source.CampaignCreation.Screens
{
    public sealed partial class SelectCharactersPage : Page
    {
        private List<Character> availableCharacters = new List<Character>();
        private static List<Character> selectedCharacters = new List<Character>();
        public static List<Character> SelectedCharacters { get { return selectedCharacters; } }
        private Character selectedCharacter;

        public SelectCharactersPage()
        {
            this.InitializeComponent();
            availableCharacters = CreatedCharacters.UserCharacters;
        }

        private void CharacterGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            selectedCharacters.Add((Character)e.ClickedItem);
            selectedCharacter = (Character)e.ClickedItem;
            this.Frame.Navigate(typeof(CreateCampaign), selectedCharacters);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateCampaign));
        }
    }
}
