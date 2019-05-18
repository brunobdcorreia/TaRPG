using RPGproject.Campanhas;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Diagnostics;


// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RPGproject
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CriarCampanha : Page
    {
        private ObservableCollection<int> OpcoesNumeroJogadores = new ObservableCollection<int>();
        private static int NumeroJogadoresParaCriar;
        public static int NumeroJogadores => NumeroJogadoresParaCriar;

        private static string NomeCampanhaParaCriar;
        public static string NomeCampanha => NomeCampanhaParaCriar;

        public CriarCampanha()
        {
            this.InitializeComponent();

            for(int i = 1; i <= 16; i++)
            OpcoesNumeroJogadores.Add(i);           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CriaCampanha.IsEnabled = false;
            NomeDaCampanha.IsEnabled = false;
            NomeCampanhaParaCriar = NomeDaCampanha.Text;
            this.Frame.Navigate(typeof(ResumoCampanha));
        }

        private void NumJogadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            NumeroJogadoresParaCriar = OpcoesNumeroJogadores.IndexOf(box.SelectedIndex) + 2;
        }

    }
}
