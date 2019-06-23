using Windows.UI.Xaml.Controls;
using RPGproject.Source.CharacterCreation;
using System.Collections.Generic;
using System;
using RPGproject.Source.UserData;
using Windows.UI.Xaml;
using RPGproject.Source.CampaignCreation.Screens;
using RPGproject.Source.UserData.Screens;
using System.Diagnostics;
using Windows.UI.Xaml.Navigation;
using RPGproject.Source.CampaignCreation;

namespace RPGproject
{
    public sealed partial class CreateCampaign : Page
    {
        private List<Character> addedCharacters;
        private CampaignModel campaignModel;

        public CreateCampaign()
        {
            this.InitializeComponent();
            addedCharacters = new List<Character>();
            campaignModel = new CampaignModel();
        }

        private void AddPlayers_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SelectCharactersPage), addedCharacters);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is List<Character>)
            {
                List<Character> characters = (List<Character>)e.Parameter;

                if (characters.Count > 0)
                {
                    campaignModel.GetCampaignModel.Characters = characters;
                    addedCharacters = campaignModel.GetCampaignModel.Characters;
                }

                else return;
            }
        }

        private void CharacterGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Character selectedCharacter = (Character) e.ClickedItem;
            this.Frame.Navigate(typeof(CharacterSummary), selectedCharacter);
        }

        private void CreateCampaign_Click(object sender, RoutedEventArgs e)
        {
            campaignModel.GetCampaignModel.CampaignName = CampaignName.Text;
            Debug.WriteLine(campaignModel.GetCampaignModel.CampaignName);
            this.Frame.Navigate(typeof(ResumoCampanha), campaignModel);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            CancelCampaignCreation();
        }
        
        private async void CancelCampaignCreation()
        {
            ContentDialog cancelCampaignCreation = new ContentDialog
            {
                Title = "Cancel campaign creation",
                Content = "Are you sure you want to cancel this campaign\'s creation?",
                PrimaryButtonText = "Yes!",
                CloseButtonText = "No, wait!"
            };

            ContentDialogResult result = await cancelCampaignCreation.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
