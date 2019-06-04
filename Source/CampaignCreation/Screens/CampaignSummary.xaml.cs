using RPGproject.Source.CampaignCreation;
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

namespace RPGproject
{
    public sealed partial class ResumoCampanha : Page
    {
        private List<Character> addedCharacters;
        public ResumoCampanha()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CampaignModel model = (CampaignModel)e.Parameter;
            CampaignName.Text = model.GetCampaignModel.CampaignName;
            addedCharacters = model.GetCampaignModel.Characters;
        }
    }
}
