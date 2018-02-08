using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayingCards
{
    // interfejs dla dowolnej karty do gry
    interface IPlayingCard : IComparable<IPlayingCard>
    {
        // kolor
        Suit Suit { get; }

        // figura
        Rank Rank { get; }
    }
}
