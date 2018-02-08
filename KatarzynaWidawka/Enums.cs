using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayingCards
{
    // wynik gry: remis==wojna, wygral gracz 1, gracz 2
    public enum GameResult { War, Player1Wins, Player2Wins };
    
    // kolor karty: kier, karo, trefl, pik 
    public enum Suit { Heart, Tile, Clover, Pike };
    
    // figura karty: 2-10, walet, dama, król, as
    public enum Rank { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Knave, Queen, King, Ace }
}