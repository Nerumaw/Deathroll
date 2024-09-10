using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Deathrolling_WoW
{
    internal class Spieler
    {
        string _name;
        int _gold;

        public string Name { get => _name; set => _name = value; }
        public int Gold { get => _gold; set => _gold = value; }

        public Spieler()
        {

        }

        public Spieler(string name, int gold)
        {
            Name = name;
            Gold = gold;
        }

        public static Spieler Create()
        {
            Console.WriteLine("Wie lautet ihr Name?\n"); //Spieler wählt seinen Namen
            Console.Write("Spieler: ");
            string userName = Console.ReadLine();

            Console.WriteLine("Sie starten mit 1.000.000 Gold."); //Gold -- automatisch 1 Millionen
            int userBank = 1000000;

            return  new Spieler(userName, userBank);
        }

        public static void Spiel()
        {
            Console.WriteLine("Willkommen in Sturmwind!\nIch hoffe sie genießen ihren Aufenthalt.\n"); //Initialisierung
            Console.WriteLine("Anweisung: Nachdem sie ihre Namen angegeben haben, wählt der 1. Spieler " +
                "eine Nummer aus, um die gewettet wird.\nDie Zahl, die erwürfelt wird, wird gewürfelt.\n" +
                "Wenn jemand eine 1 würfelt, hat dieser verloren und muss die originelle Zahl in Gold and" +
                "2. Spieler auszahlen.\n\n");

            Spieler spieler1 = Spieler.Create();
            Spieler spieler2 = Spieler.Create();

            Console.Write($"\n\n{spieler1.Name}:/roll "); //Spieler 1 wählt den Betrag um den gewettet wird.
            int deathrollStart = Convert.ToInt32(Console.ReadLine());

            int goldPool = deathrollStart;

            Random rnd = new();

            for (int i = 1; deathrollStart > 0; i++) //Tradechat exchange | Welcher Spieler ist dran
            {
                deathrollStart = rnd.Next(1, deathrollStart + 1); //Gamba-Random Teil
                if (i % 2 == 0)//2. Spieler ist dran
                {
                    Console.WriteLine($"{spieler2.Name} rollt {deathrollStart}."); //Gibt dem Spieler an was er tippen muss

                    if (deathrollStart == 1) //Würfelt eine 1 -> Verloren
                    {
                        Console.WriteLine($"\aI am sorry {spieler2.Name}, you lose."); //Verloren

                        Console.ReadKey();
                        Console.Clear();

                        spieler2.Gold -= goldPool; //Geld wird folgend übergeben
                        spieler1.Gold += goldPool;
                        
                        Console.WriteLine($"{spieler2.Name} has traded {goldPool} Gold to {spieler1.Name}.\n");
                        Console.WriteLine($"{spieler2.Name} hat nun {spieler2.Gold} Gold in seinem Account.");
                        Console.WriteLine($"{spieler1.Name} hat nun {spieler1.Gold} Gold in seinem Account.");
                        
                        break;
                    }

                    Console.Write($"{spieler1.Name}:/roll "); //Eigene Entscheidung nicht vom Computer "generiert"
                    
                    deathrollStart = Convert.ToInt32(Console.ReadLine());
                }
                else //1.Spieler ist dran
                {
                    Console.WriteLine($"{spieler1.Name} rollt {deathrollStart}."); //Gibt dem Spieler an was er tippen muss

                    if (deathrollStart == 1) //Würfelt eine 1 -> Verloren
                    {
                        Console.WriteLine($"\aI am sorry {spieler1.Name}, you lose."); //Nachricht, dass verloren

                        Console.ReadKey();
                        Console.Clear();

                        //Folgender Code: Gold-Austausch...
                        spieler1.Gold -= goldPool;
                        spieler2.Gold += goldPool;
                        
                        Console.WriteLine($"{spieler1.Name} has traded {goldPool} Gold to {spieler2.Name}.\n");
                        Console.WriteLine($"{spieler1.Name} hat nun {spieler1.Gold} Gold in seinem Account.");
                        Console.WriteLine($"{spieler2.Name} hat nun {spieler2.Gold} Gold in seinem Account.");
                        
                        break;
                    }

                    Console.Write($"{spieler2.Name}:/roll "); //Eigener Input, sodass Fair fühlt
                    
                    deathrollStart = Convert.ToInt32(Console.ReadLine()); 
                }
            }
        }
    }
}
