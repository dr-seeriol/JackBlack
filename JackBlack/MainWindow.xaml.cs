using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JackBlack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameState game = new();
        private DeckCheater? deckCheater;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = game;
            game.StartNewGame(); // This just starts a new game on startup and you can get rid of this
        }

        private void NewGame_Click(object sender, RoutedEventArgs e) => game.StartNewGame();

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            
            if (!game.DealPlayer())
            {
                statusLabel.Content = "Deck Empty";
                return;
            }

            statusLabel.Content = game.PlayerBusted ? "Bust" : "";
        }

        private void Deck_Click(object sender, RoutedEventArgs e)
        {
            if (deckCheater == null)
            {
                deckCheater = new DeckCheater(game);
                deckCheater.Closed += (closedSender, args) => deckCheater = null;
                deckCheater.Show();
            }
            else
            {
                deckCheater.Activate();
            }
        }
    }
}