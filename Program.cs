using System.ComponentModel.Design;
//problem jest taki ze zmiany zapisuja sie tylko jak wyjde z programu przez wybranie opcjinr 5 w menu
// kiedy obstawie wszystko to warunek zle dziala i mowi ze nie moge nic zrobic bo nie mam kasy
// as nie ma wyboru kiedy wypradnie pomiedzy 1 a 11 tutaj jest sztywno 10
namespace Blackjack_1
{
    internal class Program
    {
        

        public static void Menu(ZarzadzanieGraczami uzytkownicy)
        {
            GeneratorTalia generowanieKart = new();
            ZarzadzanieGraczami.Gracz nowyGracz = new();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Aktualnie zalogowany gracz:{uzytkownicy.zalogowanyGracz.username}");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Witaj w grze Blackjack.");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("1-Zagraj partie.");
                Console.WriteLine("2-Stwórz konto.");
                Console.WriteLine("3-Zobacz liste graczy");
                Console.WriteLine("4-Zaloguj sie na inne konto.");
                Console.WriteLine("5-Wyjdź z gry.");
                int.TryParse(Console.ReadLine(), out int wyborMenu);

                switch (wyborMenu)
                {
                    case 1:
                        //rozpoczecie gry i sprawdzanie czy gracz ma wystarczajaco pieniedzy na koncie
                        int obstawianaKwota = Gra.Obstawianie(uzytkownicy);
                       if(uzytkownicy.zalogowanyGracz.waluta > 0)
                        {
                            Gra.Wygrana(generowanieKart);
                            if (ZarzadzanieGraczami.Gracz.wygrana == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"Wygrana wynosi: {obstawianaKwota * 2}");
                                uzytkownicy.zalogowanyGracz.waluta += obstawianaKwota * 2;
                            }
                            else if (ZarzadzanieGraczami.Gracz.wygrana == 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine($"Przegrałeś, straciłeś: {obstawianaKwota}");
                            }
                            else if (ZarzadzanieGraczami.Gracz.wygrana == 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Pieniądze wracaja na konto");
                                uzytkownicy.zalogowanyGracz.waluta += obstawianaKwota;
                            }
                        }
                        else if (uzytkownicy.zalogowanyGracz.waluta < uzytkownicy.zalogowanyGracz.waluta - obstawianaKwota)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Nie stać cie na taki zakład.");
                        }
                       else
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Nie masz nic na koncie.");
                        }


                        break;
                    case 2:
                        uzytkownicy.Rejestracja();
                        ZarzadzanieGraczami.ZapiszGraczy();
                        break;
                    case 3:
                        
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("----------------------------------------");
                        ZarzadzanieGraczami.ListaGraczy();
                        Console.WriteLine("----------------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        
                        break;
                    case 4:
                        uzytkownicy.Logowanie(nowyGracz);
                        break;
                    case 5:
                        ZarzadzanieGraczami.ZapiszGraczy();
                        Environment.Exit(0);
                        
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            ZarzadzanieGraczami.Gracz nowyGracz = new();
            ZarzadzanieGraczami lista = new();
            ZarzadzanieGraczami.WczytajGraczy();
            while (true)
            {
                
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1-Zaloguj się.");
                Console.WriteLine("2-Stwórz konto.");
                Console.WriteLine("3-Wyjdź.");
                int.TryParse(Console.ReadLine(), out int wyborLogowanie);
                switch (wyborLogowanie)
                {
                    case 1:
                        Console.WriteLine("----------------------------------------");
                        lista.Logowanie(nowyGracz);
                        if (nowyGracz.CzyZalogowany == true)
                        {
                            Menu(lista);
                        }
                        break;
                    case 2:
                        Console.WriteLine("----------------------------------------");
                        lista.Rejestracja();
                        ZarzadzanieGraczami.ZapiszGraczy();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }

    }


}
