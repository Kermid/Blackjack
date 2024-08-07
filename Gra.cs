using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blackjack_1
{
    class Gra
    {
        public int obstawianaKwota;
        public static int Obstawianie(ZarzadzanieGraczami uzytkownicy)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Podaj ile obstawiasz pieniędzy.Twoj aktualny stan konta wynosi {uzytkownicy.zalogowanyGracz.waluta}");
            int.TryParse(Console.ReadLine(), out int obstawianaKwota);
            if (uzytkownicy.zalogowanyGracz.waluta > 0)
            {
                uzytkownicy.zalogowanyGracz.waluta = uzytkownicy.zalogowanyGracz.waluta - obstawianaKwota;
            }
            return obstawianaKwota;
        }

        public static void Wygrana(GeneratorTalia generowanieKart)
        {
            ZarzadzanieGraczami.Gracz.wygrana = 0;

            generowanieKart.CzyszczenieTalii();
            int sumaPunktowGracza = 0;
            int sumaPunktowKasyna = 0;
            bool kontynuujGre = true;
            while (kontynuujGre && sumaPunktowGracza <= 21 && sumaPunktowGracza != 21)
            {

                generowanieKart.CzyszczenieTalii();
                generowanieKart.Talia();
                sumaPunktowGracza = 0;
                sumaPunktowKasyna = 0;
                generowanieKart.GenerowanieKartyKasyna();
                generowanieKart.GenerowanieKartyKasyna();
                generowanieKart.GenerowanieKartyGracza();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Wylosowana karta kasyna: {generowanieKart.wygenerowaneKartyKasyna[generowanieKart.wygenerowaneKartyKasyna.Count - 1].znak}  {generowanieKart.wygenerowaneKartyKasyna[generowanieKart.wygenerowaneKartyKasyna.Count - 1].wartosc}");
                Console.WriteLine($"Punkty: {generowanieKart.wygenerowaneKartyKasyna[generowanieKart.wygenerowaneKartyKasyna.Count - 1].punkt}.");

                bool kontynuuj = true;
                while (kontynuuj == true && sumaPunktowGracza < 21 && sumaPunktowGracza != 21)
                {
                    generowanieKart.GenerowanieKartyGracza();
                    if (generowanieKart.wygenerowaneKartyGracza.Count <= 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        for (int i = 0; i < generowanieKart.wygenerowaneKartyGracza.Count; i++)
                        {

                            Console.WriteLine($"Wylosowana karta gracza: {generowanieKart.wygenerowaneKartyGracza[i].znak}  {generowanieKart.wygenerowaneKartyGracza[i].wartosc}.");
                            Console.WriteLine($"Punkty: {generowanieKart.wygenerowaneKartyGracza[i].punkt}.");
                            sumaPunktowGracza += generowanieKart.wygenerowaneKartyGracza[i].punkt;

                        }
                    }
                    else if (generowanieKart.wygenerowaneKartyGracza.Count > 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        foreach (Karta kartaGracza in generowanieKart.wygenerowaneKartyGracza)
                        {

                            Console.WriteLine($"Wylosowana karta gracza: {kartaGracza.znak} {kartaGracza.wartosc}");
                            Console.WriteLine($"Punkty: {kartaGracza.punkt}");

                        }
                        for (int i = 2; i < generowanieKart.wygenerowaneKartyGracza.Count; i++)
                        {
                            sumaPunktowGracza += generowanieKart.wygenerowaneKartyGracza[i].punkt;
                        }
                    }


                    if (sumaPunktowGracza == 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Wygrana!Udało ci się trafić 21!");
                        kontynuuj = false;
                        kontynuujGre = false;
                        ZarzadzanieGraczami.Gracz.wygrana = 1;
                    }
                    else if (sumaPunktowGracza > 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Przegrana!Masz za dużo punktów");
                        kontynuuj = false;
                        kontynuujGre = false;
                        ZarzadzanieGraczami.Gracz.wygrana = 2;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("1-Dobierz Kartę.");
                        Console.WriteLine("2-Pasuj.");
                        int.TryParse(Console.ReadLine(), out int wybor);
                        switch (wybor)
                        {
                            case 1:
                                kontynuuj = true;
                                break;
                            case 2:
                                kontynuuj = false;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Nieprawidłowa opcja.");
                                break;
                        }


                    }
                }
                if (sumaPunktowGracza != 21 && sumaPunktowGracza < 21)
                {
                    bool kontynuujKasyno = true;
                    while (kontynuujKasyno)
                    {

                        int index = 0;

                        while (sumaPunktowKasyna < sumaPunktowGracza)
                        {
                            if (sumaPunktowKasyna < sumaPunktowGracza && index >= 2)
                            {
                                generowanieKart.GenerowanieKartyKasyna();
                            }
                            sumaPunktowKasyna += generowanieKart.wygenerowaneKartyKasyna[index].punkt;
                            index++;

                        }
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (Karta kartaKasyna in generowanieKart.wygenerowaneKartyKasyna)
                        {
                            Console.WriteLine($"Wylosowana karta kasyna: {kartaKasyna.znak} {kartaKasyna.wartosc}");
                            Console.WriteLine($"Punkty: {kartaKasyna.punkt}");
                        }
                        Console.WriteLine($"Suma punktów Kasyna: {sumaPunktowKasyna}");

                        if (sumaPunktowGracza < 21 && sumaPunktowGracza > sumaPunktowKasyna)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Wygrywasz!Masz wiecej punktów od kasyna!");
                            Console.WriteLine($"Punkty kasyna:{sumaPunktowKasyna}");
                            Console.WriteLine($"Punkty gracza:{sumaPunktowGracza}");
                            kontynuujGre = false;
                            kontynuujKasyno = false;
                            ZarzadzanieGraczami.Gracz.wygrana = 1;

                        }
                        else if (sumaPunktowKasyna == 21)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Kasyno wygrywa!");
                            Console.WriteLine($"Punkty kasyna:{sumaPunktowKasyna}");
                            Console.WriteLine($"Punkty gracza:{sumaPunktowGracza}");
                            kontynuujGre = false;
                            kontynuujKasyno = false;
                            ZarzadzanieGraczami.Gracz.wygrana = 2;
                        }
                        else if (sumaPunktowKasyna > 21)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Wygrywasz!Kasyno ma zbyt wiele punktów.");
                            Console.WriteLine($"Punkty kasyna:{sumaPunktowKasyna}");
                            Console.WriteLine($"Punkty gracza:{sumaPunktowGracza}");
                            kontynuujGre = false;
                            kontynuujKasyno = false;
                            ZarzadzanieGraczami.Gracz.wygrana = 1;

                        }
                        else if (sumaPunktowKasyna < 21 && sumaPunktowKasyna > sumaPunktowGracza)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Kasyno wygrywa!Ma wiecej punktów od ciebie!");
                            Console.WriteLine($"Punkty kasyna:{sumaPunktowKasyna}");
                            Console.WriteLine($"Punkty gracza:{sumaPunktowGracza}");
                            kontynuujGre = false;
                            kontynuujKasyno = false;
                            ZarzadzanieGraczami.Gracz.wygrana = 2;

                        }
                        else if (sumaPunktowGracza == sumaPunktowKasyna)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Remis!Macie tyle samo punktów!");
                            Console.WriteLine($"Punkty kasyna:{sumaPunktowKasyna}");
                            Console.WriteLine($"Punkty gracza:{sumaPunktowGracza}");
                            kontynuujGre = false;
                            kontynuujKasyno = false;
                            ZarzadzanieGraczami.Gracz.wygrana = 3;
                            
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}


