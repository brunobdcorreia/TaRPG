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
        public ResumoCampanha()
        {
            this.InitializeComponent();
            NumeroText.Text = CriarCampanha.playerNumber.ToString();
            NomeText.Text = CriarCampanha.campaignName;
            Debug.WriteLine(NomeText.Text);
        }
    }
}
