using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayingCards
{
    class Program
    {
        static void Main(string[] args)
        {
            // obiekt do porownania kart

            CardComparer comparer = new CardComparer();

            Deck deck = new Deck();
            // talia


            // rozdzielamy karty na 2 graczy, kazdy gracz powinien dostac tyle samo kart,
            // jaki typ koleckji powinien byc przeznaczony na "reke" gracza?

            //Queue<Card> GetCards(int count) //kolejka

            //{
            var pl1hand = deck.GetCards(26);
            var pl2hand = deck.GetCards(26);
            //}

            // miejsce na stole rozgrywki
            // jaki typ koleckji powinien byc przeznaczony na "stol" gracza?


            //Stack<Card> GetCards(int count) //stos 

            //{
            // var pl1table=deck.GetCards(0);
            //var pl2table=deck.GetCards(0);
            //}
            var pl1table = new Stack<Deck.Card>();
            var pl2table = new Stack<Deck.Card>();


            // zaczynamy
            int iteration = 0;
            Console.WriteLine("{0,5} - Player1: {1,3} <=> Player2: {2,3}", iteration++, pl1hand.Count, pl2hand.Count);

            // gramy dopoki obaj maja niezerowa liczbe kart
            while (pl1hand.Count > 0 && pl2hand.Count > 0)
            {
                // kazdy gracz wyciaga karte z _poczatku_ 
                // i daje na swoja "kupke" na stole
                // jak pobrac z reki i dorzucic na kupke na stole?
                // pl1hand -> pl1table
                //...
                // // pl1hand.Dequeue(); //Odjęcie katy z ręki 
                pl1table.Push(pl1hand.Dequeue()); //Dodanie na stół
                ///  pl2hand.Dequeue();
                pl2table.Push(pl2hand.Dequeue());
                // zerknijmy co jest na stole, ale nie zabieramy jeszcze
                // czym mozna podejrzec karte?  
                Deck.Card pl1card = pl1table.Peek();//...Zerknięcie co jest na pierwszym miejscu
                Deck.Card pl2card = pl2table.Peek();//...

                Console.WriteLine("Player1: {0,20} <=> Player2: {1,20}", pl1card, pl2card);

                // porownanie kart na stole rzuconych przez graczy
                // nalezy uzyc odpowiedniej metody porownojacej
                //Array.Sort(pl1card,pl2card,comparer);
                //    {
                //      pl1card.CompareTo(pl2card);
                //    }

                int cmp = comparer.Compare(pl1card, pl2card);//...

                if (cmp > 0)
                {
                    Console.WriteLine("Player1 wins!");
                    // wygrywa gracz 1 - zabiera ze stolu obie kupki i dodaje na koniec swoich katr
                    // na reke wygranego gracza przechodza karty ze stolu
                    //...
                    pl1hand.Enqueue(pl1table.Pop()); //Zabranie kart ze stołu swoich i gracza i wrzucenie na jedną kupkę
                    pl1hand.Enqueue(pl2table.Pop()); //Na reke gracza 1 przechodzą karty ze stołu
                }
                else if (cmp < 0)
                {
                    Console.WriteLine("Player2 wins!");
                    // wygrywa gracz 2 - zabiera ze stolu obie kupki i dodaje na koniec swoich katr
                    // na reke wygranego gracza przechodza karty ze stolu                   
                    //...
                    pl2hand.Enqueue(pl1table.Pop());
                    pl2hand.Enqueue(pl2table.Pop());

                }
                else // WOJNA!
                {
                    do
                    {
                        Console.WriteLine("WAR!!!");

                        // pobranie kart od graczy i polozenie ich na stole - zakryte
                        //... // jedna karta ukryta
                        //... // jedna karta ukryta

                        pl1table.Push(pl1hand.Dequeue());
                        //... // jedna karta ukryta
                        pl2table.Push(pl2hand.Dequeue());

                        // pobranie kart od graczy i polozenie ich na stole - odkryte
                        //...  // karta widoczna, ale juz na stole
                        pl1table.Push(pl1hand.Dequeue());
                        //...  // karta widoczna, ale juz na stole
                        pl2table.Push(pl2hand.Dequeue());

                        // sprawdzenie kart polozonych na stole przez graczy
                        pl1card = pl1table.Peek(); //... // zerknijmy jakie to karty?
                        pl2card = pl2table.Peek();//...
                        // sprawdzenie kart polozonych na stole przez graczy
                        // pl1card = null; //... // zerknijmy jakie to karty?
                        // pl2card = null;//...

                        // uzywajac odpowiedniej metody porownanie kart lezacych na stole
                        cmp = comparer.Compare(pl1card, pl2card);




                        Console.WriteLine("Player1: {0,20} <=> Player2: {1,20}", pl1card, pl2card);

                    } while (cmp == 0); // mozliwa kontynuacja -> wojna wielokrotna

                    // wojna zakonczona - sprawdzamy kto wygral
                    if (cmp > 0)
                    {
                        Console.WriteLine("Player1 wins!");
                        // gracz 1 zabiera obie kupki
                        while (pl1table.Count > 0)
                        {
                            pl1hand.Enqueue(pl1table.Pop());
                        }
                        while (pl2table.Count > 0)
                        {
                            pl1hand.Enqueue(pl2table.Pop());
                        }
                    }
                    else if (cmp < 0)
                    {
                        Console.WriteLine("Player2 wins!");
                        // gracz 2 zabiera obie kupki
                        while (pl2table.Count > 0)
                        {
                            pl2hand.Enqueue(pl2table.Pop());
                        }
                        while (pl1table.Count > 0)
                        {
                            pl2hand.Enqueue(pl1table.Pop());
                        }
                    }
                } // koniec wojny

                Console.WriteLine("{0,5} - Player1: {1,3} <=> Player2: {2,3}", iteration++, pl1hand.Count, pl2hand.Count);
               // Console.ReadKey();
            }


            // podsumowanie:

            Console.WriteLine("Player1 cards:");
            DisplaySortedCardsArray(pl1hand.ToArray(), comparer);

            Console.WriteLine("Player2 cards:");
            DisplaySortedCardsArray(pl2hand.ToArray(), comparer);

            //Console.ReadKey();
             
        }



        static void DisplaySortedCardsArray(Deck.Card[] cards, IComparer<IPlayingCard> comparer)
        {
            // metoda powinna posortować karty używając odpowiedniego komparatora, po posortowaniu
            // powinna wszystkie karty wyświetlić na ekran koncoli
            //...
            
                Array.Sort(cards, comparer.Compare);

                foreach (var karty in cards)
                {
                    Console.WriteLine(karty);
                }
          





        }
    }
}

