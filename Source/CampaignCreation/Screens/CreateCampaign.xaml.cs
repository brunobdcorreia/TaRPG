using RPGproject.Campanhas;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Diagnostics;

namespace RPGproject
{
    public sealed partial class CriarCampanha : Page
    {
        private ObservableCollection<int> playerNumberOptions = new ObservableCollection<int>();
        private static int playerNumberChosen;
        public static int playerNumber => playerNumberChosen;

        private static string campaignNameChosen;
        public static string campaignName => campaignNameChosen;

        public CriarCampanha()
        {
            this.InitializeComponent();

            for(int i = 1; i <= 16; i++)
            playerNumberOptions.Add(i);           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CriaCampanha.IsEnabled = false;
            NomeDaCampanha.IsEnabled = false;
            campaignNameChosen = NomeDaCampanha.Text;
            this.Frame.Navigate(typeof(ResumoCampanha));
        }

        private void NumJogadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            playerNumberChosen = playerNumberOptions.IndexOf(box.SelectedIndex) + 2;
        }

    }
}
