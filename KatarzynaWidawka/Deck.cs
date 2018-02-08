using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace PlayingCards
{
    class Deck // ustalona talia kart
    {
        // pojedyncza karta
        public class Card : IPlayingCard
        {
            // wewnetrzna klasa ze specyficznymi ograniczeniami aby 
            // w kodzie zewnetrznym nie bylo mozliwosci stworzenia (dodatkowej) karty
            // nienalezacej do talii (np. wyciganie z rekawa)
            // patrz tez http://stackoverflow.com/questions/1664793/how-to-restrict-access-to-nested-class-member-to-enclosing-class

            private Suit suit; // kolor
            private Rank rank; // figura

            public Suit Suit
            {
                get { return suit; }
            }

            public Rank Rank
            {
                get { return rank; }
            }

            // chroniony konstruktor
            protected Card(Suit suit, Rank rank)
            {
                this.suit = suit;
                this.rank = rank;
            }

            // wynika z IComparable
            public int CompareTo(IPlayingCard other)
            {
                CardComparer cc = new CardComparer();
                // albo w sposob przewidziany przez klase Comparer
                //IComparer<IPlayingCard> cc = CardComparer.Default;
                return cc.Compare(this, other);
            }

            // dla wygody
            public override string ToString()
            {
                string str = string.Format("{0} of {1}", rank, suit); // prosciej
                str = string.Format("{0} of {1}", Enum.GetName(typeof(Rank), rank), Enum.GetName(typeof(Suit), suit));
                return str;
            }
        }

        // prywatna klasa dziedziczaca po Card, ale z publicznym konstruktorem
        // dzieki temu mozemy tu wywolac konstruktor chroniony z Card
        // ale z kodu zewnetrznego nie bedzie to mozliwe
        private class CardInstance : Card
        {
            public CardInstance(Suit suit, Rank rank)
                : base(suit, rank)
            { }
        }

        private Random rnd;
        private List<Card> cards;

        // konstruktor talii
        public Deck()
        {
            this.rnd = new Random();
            this.cards = new List<Card>();  // kontener na karty

            FillCardsList();
        }

        // wypelnij talie wszystkimi mozliwymi kartami
        private void FillCardsList()
        {
            Array ranks = Enum.GetValues(typeof(Rank));
            Array suits = Enum.GetValues(typeof(Suit));

            // dla kazdego koloru
            foreach (Rank r in ranks)
            {
                // dla kazdej figury
                foreach (Suit s in suits)
                {
                    // stworz i dodaj karte
                    this.cards.Add(new Deck.CardInstance(s, r));
                }
            }
        }

        // liczba kart w talii
        public int Count
        {
            get
            {
                return cards.Count;
            }
        }

        // TODO Shuffle(), np.: http://www.dotnetperls.com/fisher-yates-shuffle
        // przydaloby sie jeszcze przetasowac...

        // ... ale tu zrobione na podstawie "losowego" wyciagania kart z talii
        // nie do konca odzwierciedla to rzeczywistosc - moze lepszy bylby stos, 
        // ale wtedy utrudniloby to prawidlowe dodawanie kart wygranych w wojnie
        // przyjmijmy, ze taka kolejnosc to tez wynik "tasowania"
        public Queue<Card> GetCards(int count)
        {
            // kontener na karty dla gracza
            Queue<Card> ret = new Queue<Card>();

            for (int i = 0; i < count; i++)
            {
                // wylosuj indeks
                int index = rnd.Next(cards.Count);
                // daj karte
                ret.Enqueue(cards[index]);
                // usun z talii
                cards.RemoveAt(index);
            }

            return ret;
        }
    }
}