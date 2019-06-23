using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ANDROID_TARPG
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void CreateCharacter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateCharacterPageOne());
        }
        private async void ViewCharacter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());

        }
        private async void Connect(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}
