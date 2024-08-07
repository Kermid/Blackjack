using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blackjack_1.ZarzadzanieGraczami;

namespace Blackjack_1
{
    internal class ZarzadzanieGraczami
    {
        static public List<Gracz> listaGraczy = new();
        public const string zapisGraczy = "gracze.txt";
        public Random random = new();
        public Gracz zalogowanyGracz;
        public class Gracz
        {
            public int id;
            public string username;
            public string password;
            public int waluta;
            public static int wygrana;
            public bool CzyZalogowany = false;
            
            

        }

        public void Rejestracja()
        {
            
            Console.WriteLine("Podaj swój nick.");
            string username = Console.ReadLine();
            //sprawdza czy podany gracz juz istnieje w liscie 
            Gracz RejestracjaGracza = listaGraczy.FirstOrDefault(x => x.username == username);

            //jak istnieje to pisze ze nie da sie stworzyc 
            if (RejestracjaGracza != null)
            {
                Console.WriteLine("Gracz z takim nickiem już istnieje.");
            }
            //jak nie jest nullem to znaczy ze mozna dodawac gracza
            else
            {
                Gracz gracz = new Gracz();
                Console.WriteLine("Podaj swoje hasło.");
                string password = Console.ReadLine();
                gracz.id = random.Next(1, 200);
                gracz.waluta = 500;
                gracz.username = username;
                gracz.password = password;
                
                listaGraczy.Add(gracz);
            }

        }
      public void Logowanie(Gracz gracz)
    {
        Console.WriteLine("Podaj swój nick");
        string nickLogowanie = Console.ReadLine();
        Console.WriteLine("Podaj swoje hasło.");
        string hasloLogowanie = Console.ReadLine();

        //szuka gracz ze wpisanym haslem i nickiem 
          zalogowanyGracz = listaGraczy.FirstOrDefault(g => g.username == nickLogowanie && g.password == hasloLogowanie);
        //jesli isnieje czyli nie jest nullem to przechodzi dalej
            if (zalogowanyGracz != null)
        {
            Console.WriteLine($"Zostałeś zalogowany jako {zalogowanyGracz.username}");
            gracz.CzyZalogowany = true;
        }
        else
        {
            Console.WriteLine("Złe dane logowania");
            gracz.CzyZalogowany = false;
        }

        }
       public static void ZapiszGraczy()
        {
            using var plik = new StreamWriter(zapisGraczy);
            foreach (Gracz gracz in listaGraczy)
            {
                plik.WriteLine($"{gracz.id};{gracz.username};{gracz.password};{gracz.waluta}");
            }
        }
        public static void WczytajGraczy()
        {
            listaGraczy.Clear();
            if(File.Exists(zapisGraczy) == true)
            {
                using var plik = new StreamReader(zapisGraczy);
                while(plik.EndOfStream == false)
                {
                    Gracz gracz = new();
                    var linia = plik.ReadLine();
                    ParsujDane(linia,gracz);
                    listaGraczy.Add(gracz);
                }
            }
           

        }
        public static void ParsujDane(string linia,Gracz gracz)
        {
            //kod wzialem z zajec bo nei wiedzialem jak to lepiej napisac
            var dane = linia.Split(';');
            int.TryParse(dane[0], out gracz.id);
            gracz.username = dane[1];
            gracz.password = dane[2];
            int.TryParse(dane[3], out gracz.waluta);
        }
        public static void ListaGraczy()
        {
            int index = 1;
            if (listaGraczy.Count > 0 )
            {
                foreach (Gracz gracz in listaGraczy)
                {
                    Console.WriteLine($"{index}.{gracz.username} Ilość pieniędzy: {gracz.waluta}.");
                    index++;
                }
            }
        }
    }
   
}
