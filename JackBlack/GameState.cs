using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace JackBlack
{
    public class GameState : INotifyPropertyChanged
    {
        private static readonly Random random = new Random();
        private int playerScore;
        private int dealerScore;

        public ObservableCollection<Card> Deck { get; } = new();
        public ObservableCollection<Card> PlayerHand { get; } = new();
        public ObservableCollection<Card> DealerHand { get; } = new();

        public int PlayerScore
        {
            get => playerScore;
            private set { playerScore = value; OnPropertyChanged(nameof(PlayerScore)); }
        }

        public int DealerScore
        {
            get => dealerScore;
            private set { dealerScore = value; OnPropertyChanged(nameof(DealerScore)); }
        }

        public bool PlayerBusted => PlayerScore > 21;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void StartNewGame()
        {
            Deck.Clear();
            PlayerHand.Clear();
            DealerHand.Clear();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    Deck.Add(new Card(suit, rank));

            Shuffle();
            DealPlayer();
            DealDealer();
            DealPlayer();
            DealDealer();
        }

        public void Shuffle()
        {
            
            for (int i = Deck.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                if (i != j)
                    Deck.Move(j, i);
            }
        }

        public bool DealPlayer()
        {
            if (!DealTo(PlayerHand)) return false;
            PlayerScore = CalculateScore(PlayerHand);
            OnPropertyChanged(nameof(PlayerBusted));
            return true;
        }

        public bool DealDealer()
        {
            if (!DealTo(DealerHand)) return false;
            DealerScore = CalculateScore(DealerHand);
            return true;
        }

        private bool DealTo(ObservableCollection<Card> hand)
        {
            if (Deck.Count == 0) return false;
            Card top = Deck[0];
            Deck.RemoveAt(0);
            hand.Add(top);
            return true;
        }

        private static int CalculateScore(IEnumerable<Card> hand)
        {
            int score = hand.Sum(c => c.Value);
            int aces = hand.Count(c => c.Rank == Rank.Ace);
            while (score > 21 && aces > 0)
            {
                score -= 10;
                aces--;
            }
            return score;
        }
    }
}
