using Windows.UI.Xaml.Controls;
using RPGproject.Source.CharacterCreation;
using System.Collections.Generic;
using RPGproject.Source.UserData;
using Windows.UI.Xaml;
using RPGproject.Source.CampaignCreation.Screens;
using RPGproject.Source.UserData.Screens;
using System.Diagnostics;
using Windows.UI.Xaml.Navigation;
using RPGproject.Source.CampaignCreation;
using RPGproject.Source.Data;

namespace RPGproject
{
    public sealed partial class CreateCampaign : Page
    {
        private List<Character> addedCharacters;
        private CampaignModel campaignModel;

        public CreateCampaign()
        {
            this.InitializeComponent();
            campaignModel = new CampaignModel();
        }

        private void AddPlayers_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SelectCharactersPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is List<Character>)
            {
                campaignModel.GetCampaignModel.Characters = (List<Character>)e.Parameter;
                addedCharacters = campaignModel.GetCampaignModel.Characters;
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
            CampaignDB.Insert(campaignModel.GetCampaignModel);
        }
    }
}
