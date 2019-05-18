using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPGproject
{
    public sealed partial class CreateCharacter : Page
    {
        public List<String> Races = new List<String>();
        public List<String> Classes = new List<String>();

        public CreateCharacter()
        {
            this.InitializeComponent();       
            Races.Add("Dragonborn");
            Races.Add("Tiefling");
            Races.Add("Argonian");
            Classes.Add("Bard");
            Classes.Add("Barbarian");
            Classes.Add("Ranger");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Strength: " + Strength.Text);
            Debug.WriteLine("Dexterity: " + Dexterity.Text);
            Debug.WriteLine("Constitution: " + Constitution.Text);
            Debug.WriteLine("Intelligence: " + Intelligence.Text);
            Debug.WriteLine("Wisdom: " + Wisdom.Text);
            Debug.WriteLine("Charisma: " + Charisma.Text);
        }
    }
}
