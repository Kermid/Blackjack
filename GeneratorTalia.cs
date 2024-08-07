using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Blackjack_1
{
    internal class GeneratorTalia
    {
        public List<Karta> dostepneKarty = new();
        public List<Karta> wygenerowaneKartyGracza = new();
        public List<Karta> wygenerowaneKartyKasyna = new();
        public Random random = new();
        public void Talia()
        {
            string[] znaki = { "Kier", "Trefl", "Pik", "Karo" };
            string[] wartosci = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Walet", "Dama", "Król", "As" };
            int[] punkty = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 10 };
            int index = 0;
            foreach (var znak in znaki)
            {
                foreach (var wartosc in wartosci)
                {
                    dostepneKarty.Add(new Karta { znak = znak, wartosc = wartosc });
                }

            }
            for (int i = 0; i < znaki.Length; i++)
            {
                foreach (var punkt in punkty)
                {
                    dostepneKarty[index].punkt = punkt;
                    index++;
                }
            }

        }
        public Karta GenerowanieKartyGracza()
        {
            if (dostepneKarty.Count == 0)
            {
                Console.WriteLine("Brak dostępnych kart w talii.");
                return null;
            }

            int index = random.Next(dostepneKarty.Count);
            Karta losowaKarta = dostepneKarty[index];

            dostepneKarty.RemoveAt(index);
            wygenerowaneKartyGracza.Add(losowaKarta);

            return losowaKarta;
        }
        public Karta GenerowanieKartyKasyna()
        {
            if (dostepneKarty.Count == 0)
            {
                Console.WriteLine("Brak dostępnych kart w talii.");
                return null;
            }

            int index = random.Next(dostepneKarty.Count);
            Karta losowaKarta = dostepneKarty[index];

            dostepneKarty.RemoveAt(index);
            wygenerowaneKartyKasyna.Add(losowaKarta);

            return losowaKarta;
        }
        public void CzyszczenieTalii()
        {
            dostepneKarty.Clear();
            wygenerowaneKartyGracza.Clear();
            wygenerowaneKartyKasyna.Clear();
        }
     
    }

}
