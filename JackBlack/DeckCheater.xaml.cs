using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JackBlack
{
    /// <summary>
    /// Interaction logic for DeckCheater.xaml
    /// </summary>
    public partial class DeckCheater : Window
    {
        private readonly GameState game;

        public DeckCheater(GameState game)
        {
            InitializeComponent();
            this.game = game;
            DataContext = game;
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e) => MoveSelected(-1);
        private void MoveDown_Click(object sender, RoutedEventArgs e) => MoveSelected(1);

        private void MoveSelected(int offset)
        {
            int index = deckListBox.SelectedIndex;
            int newIndex = index + offset;

            // No selection, or already at the top/bottom: do nothing
            if (index < 0 || newIndex < 0 || newIndex >= game.Deck.Count)
                return;

            game.Deck.Move(index, newIndex);
            deckListBox.SelectedIndex = newIndex;   // keep the card selected
        }
    }
}
