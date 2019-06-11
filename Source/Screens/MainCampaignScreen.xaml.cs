using RPGproject.Source.CampaignCreation;
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

namespace RPGproject.Source.Screens
{
    public sealed partial class MainCampaignScreen : Page
    {
        private List<Character> ParticipatingCharacters;
        private Campaign campaign;

        public MainCampaignScreen()
        {
            this.InitializeComponent();
            ParticipatingCharacters = new List<Character>();
            campaign = new Campaign();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is Campaign)
            {
                campaign = (Campaign)e.Parameter;
                ParticipatingCharacters = campaign.Characters;
            }
        }
    }
}
