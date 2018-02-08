using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayingCards
{
    // klasa do porownania dwoch kart
    class CardComparer : Comparer<IPlayingCard>
    {
        // de facto bardzo uproszczona - mozna by osobne zajecia na to poswiecic:
        // - klasa bazowa -> pochodne definiuja wlasne wagi (np. dla roznych gier) -> singletony
        // - fabryka singletonow
        // - ...

        // kontener na wagi kart
        private Dictionary<Rank, int> weights;

        // domyslny konstruktor
        public CardComparer()
        {
            SetDefaultWeights();
        }

        // metoda ustwaiajaca wagi poszczegolnych kart
        private void SetDefaultWeights()
        {
            this.weights = new Dictionary<Rank, int>();
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                int weight = 0;
                switch (r)
                {
                    case Rank.Two: weight = 2; break;
                    case Rank.Three: weight = 3; break;
                    case Rank.Four: weight = 4; break;
                    case Rank.Five: weight = 5; break;
                    case Rank.Six: weight = 6; break;
                    case Rank.Seven: weight = 7; break;
                    case Rank.Eight: weight = 8; break;
                    case Rank.Nine: weight = 9; break;
                    case Rank.Ten: weight = 10; break;
                    case Rank.Knave: weight = 11; break;
                    case Rank.Queen: weight = 12; break;
                    case Rank.King: weight = 13; break;
                    case Rank.Ace: weight = 14; break;
                }
                this.weights.Add(r, weight);
            }
        }

        // wymuszone przez klase bazowa Comparer (oraz IComparer)
        public override int Compare(IPlayingCard x, IPlayingCard y)
        {
            int xw = this.weights[x.Rank]; // pobiez wage dla figury - karty x
            int yw = this.weights[y.Rank]; // pobiez wage dla figury - karty y
            int result = xw - yw;
            return result;
        }

        // nadmiarowo: konwersja z wyniku porownania do bytu biznesowego
        // ale OK tylko dla gry w wojne
        public static GameResult GameResultRemaper(int result)
        {
            if (result > 0)
            { return GameResult.Player1Wins; }
            else if (result < 0)
            { return GameResult.Player2Wins; }
            else
            { return GameResult.War; }
        }
    }
}
