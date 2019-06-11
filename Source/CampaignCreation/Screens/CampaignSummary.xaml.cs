using RPGproject.Source.CampaignCreation;
using RPGproject.Source.CharacterCreation;
using RPGproject.Source.UserData;
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

namespace RPGproject
{
    public sealed partial class ResumoCampanha : Page
    {
        CampaignModel model;

        private List<Character> addedCharacters;
        public ResumoCampanha()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            model = (CampaignModel)e.Parameter;
            string aux = CampaignName.Text;
            CampaignName.Text = aux + " " + model.GetCampaignModel.CampaignName;
            //CampaignName.Text = CampaignName.Text + " " + model.GetCampaignModel.CampaignName;
            addedCharacters = model.GetCampaignModel.Characters;
        }

        private async void DisplayCreationConfirmation()
        {
            ContentDialog confirm = new ContentDialog
            {
                Title = "Confirm campaign creation.",
                Content = "Are you sure you want to create a campaign with these settings?",
                PrimaryButtonText = "Yes!",
                CloseButtonText = "No!"
            };

            ContentDialogResult result = await confirm.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                CreatedCampaigns.AddCampaign(model.GetCampaignModel);
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            DisplayCreationConfirmation();
        }
    }
}
