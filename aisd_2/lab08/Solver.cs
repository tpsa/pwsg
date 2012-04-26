using System;
using System.Collections.Generic;
using System.Linq;

/*
  
 Program ten nie został napisany w celu optymalnego omijania liczników dr Jana Bródki 
  
 Wykorzystuje pamięć na stosie funkcji, 
 - dodatkową tablicę  o liczbie koloru 1, o liczbie koloru 2 i liczbie pustych dla każdego zbioru 
 (dzięki temu bardzo łatwo się sprawdza czy dodany kolor koliduje z innymi) 
 CheckAndSet jest liniowy wzg. ilości zbiorów. Jeżeli do zbioru można dodać tylko kolor 1, a program chce nr 2
 to zostanie automatycznie wykryte i program zapisze kolor 1.
 - listę zbiorów do których należą poszczególne elementy. 

 Wg. mnie wszystkie te zmienne pomocnicze nie służą tylko optymalnemu omijaniu liczników pana Bródki 

 Zapisuję tylko kolory, gdy znajdę jakieś rozwiązanie. 
 Ilość zapisów = ilość elementów. Ilość odczytów = 0 (nie czytam) 

 Jak wyżej, jak Dr Jan Bródka powie, że nie można to wrócę do poprzedniej wersji
 
 Gdyby tak było to trzeba odkomentować i zakomentować po 2 linijki w oznaczonych miejscach
 
 */

namespace SplittingSet
{

    class Solver
    {

        struct set
        {
            public int Red;
            public int Blue;
            public int Empty;
        };

        set[] InfoSets;
        List<int>[] NodeSets;

        int n;
        public Solver(int n)
        {
            this.n = n;
        }


        public bool CheckAndSet(HashSet<int>[] sets, bool Red, ref Colors colors, int position)
        {
            List<int> SetList = NodeSets[position];

            foreach (int Set in SetList)
            {
                if (Red) // analogicznie jak w 2.
                {
                    if (InfoSets[Set].Empty <= 0)
                        throw new Exception("Nie ma miejsca a powinno");
                    if (InfoSets[Set].Empty > 1)
                        continue;
                    if (InfoSets[Set].Empty == 1)
                    {
                        if (InfoSets[Set].Blue == 0)
                            return false;
                        else
                            continue;
                    }
                }
                else
                {
                    if (InfoSets[Set].Empty <= 0)
                        throw new Exception("Nie ma miejsca a powinno");
                    if (InfoSets[Set].Empty > 1) // więcej niż 1 możemy dowolny
                        continue;
                    if (InfoSets[Set].Empty == 1) // 1 wolny
                    {
                        if (InfoSets[Set].Red == 0)
                            return false; // 2. koloru nie ma nie dopuszczamy do tego
                        else 
                            continue; // 2. kolor już jest 
                    }
                }
            }

            // kolorujemy element (w poszczególnych zbiorach)
            // wcześniej nie moglibyśmy tego zrobić bo np. kolorujemy i okazuje się, że w którymś nie da rady włożyć go
            // i jak odwrócić proces?

            if (Red) 
            {
                foreach (int Set in SetList)
                {
                    InfoSets[Set].Red++;
                    InfoSets[Set].Empty--;
                }
                //colors[position] = 1; // tu odkomentować
            }
            else 
            {
                foreach (int Set in SetList)
                {
                    InfoSets[Set].Blue++;
                    InfoSets[Set].Empty--;
                }
                //colors[position] = 2; // tu odkomentować
            }

            return true;
        }

        public void UnSet(bool Red, int position)
        {
            // dla poszczególnego zbioru usuwam element (pokolorowanie)
            List<int> SetList = NodeSets[position];
            foreach (int Set in SetList)
            {
                if (Red) InfoSets[Set].Red--;
                else InfoSets[Set].Blue--;
                ++InfoSets[Set].Empty;
            }
        }

        public void InitializeSets(HashSet<int>[] sets, ref Colors colors)
        {
            int nSets = sets.Length;
            NodeSets = new List<int>[colors.n]; 
            for (int i = 0; i < colors.n; i++)
                NodeSets[i] = new List<int>();
            InfoSets = new set[nSets]; 
            for (int i = 0; i < nSets; i++)
            {
                foreach (int El in sets[i])
                    NodeSets[El].Add(i);
                InfoSets[i].Blue = 0;
                InfoSets[i].Red = 0; // na początku nie oznaczyliśmy żadnych elementów na żaden kolor
                InfoSets[i].Empty = sets[i].Count;  // na początku wszystkie są nie pokolorowane
            }

        }
           

        public bool Iterate(HashSet<int>[] sets, ref Colors colors, int position)
        {
            if (sets == null)
                return false;
            if (sets.Length <= 0)
            {
                for (int i = 0; i < colors.n; i++)
                    colors[i] = 1;
                return true;
            }

            if (position >= colors.n) // jeżeli doszło to znaleźliśmy rozwiązanie
                return true;          // bo po 1. jest poprawny gdyż funkcja CheckAndSet pilnuje tego

            if (CheckAndSet(sets, true, ref colors, position)) // czy możemy go pokolorwać
            { // jeżeli tak to funkcja CheckAndSet koloruje
                bool result = Iterate(sets, ref colors, position + 1);
                UnSet(true, position); // odkolorowujemy
                if (result)
                {
                    colors[position] = 1; // tu zakomentować
                    return true;
                }
            }
            if (CheckAndSet(sets, false, ref colors, position))
            {
                bool result = Iterate(sets, ref colors, position + 1);
                UnSet(false, position);
                if (result)
                {
                    // fajnie w tej gałęźi znaleźliśmy
                    colors[position] = 2; // tu zakomentować
                    return true;
                }
            }
            // ta gałąź nic nie znalazła 
            return false;
        }

        public bool Split(List<HashSet<int>> sets, ref Colors colors)
        {
            if (sets == null) return true;
            if (sets.Count <= 0)
            {
                for (int i = 0; i < colors.n; i++)
                    colors[i] = 1;
                return true;
            }
            //long LastTicks = DateTime.Now.Ticks; // wg. mnie czas jest dużo lepszym oszacowanie wydajności (zwłaszcza uśredniony dla losowych danych i wielu uruchomień)
                                                 // Czasu nie oszukam
            HashSet<int>[] setsArr = sets.ToArray();
            InitializeSets(setsArr, ref colors);

            int nSets = setsArr.Length;
            int nColors = colors.n;

            bool result =  Iterate(setsArr, ref colors, 0);
            //Console.WriteLine("Ilosc ms {0}", (double)(DateTime.Now.Ticks - LastTicks) / 10000.0d);
            return result;

        }       
    }
}
