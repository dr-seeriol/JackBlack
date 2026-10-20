using System;
using System.Collections.Generic;
using System.Text;

namespace JackBlack
{
    public enum Suit { Clubs, Diamonds, Hearts, Spades }
    public enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        // Normal ace rules from blackjack, no idea if this is right because i don't gamble that much
        public int Value => Rank == Rank.Ace ? 11 : Rank >= Rank.Ten ? 10 : (int)Rank;

        public string Name => $"{Rank} of {Suit}";

        public override string ToString() => Name;
    }
}
