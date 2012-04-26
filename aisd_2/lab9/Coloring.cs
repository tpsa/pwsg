using System.Collections.Generic    ;
using System;


namespace ASD.Graph
{

public static class ColoringExtender
    {

    // koloruje graf algorytmem zachlannym (byc moze niepotymalnie)
    public static int GreedyColor(this IGraph g, out int[] colors)
        {
        // kazdemu wierzcholkowi 
        // przydzielamy najmniejszy kolor nie kolidujacy z juz pokolorowanymi sasiadami
        // (wpisujemy go do tablicy colors)
        // zwracamy liczbe uzytych kolorow

            int n = g.VerticesCount;
            colors = new int[n];

            int nr_koloru = 1;
        
            for (int i = 0; i < n; i++)
            {
                HashSet<int> NodeColors = new HashSet<int>();
                foreach (Edge e in g.OutEdges(i))
                {
                    int kolor = colors[e.To];
                    if (!NodeColors.Contains(kolor) && kolor != 0)
                        NodeColors.Add(kolor);
                }

                if (NodeColors.Count == nr_koloru)
                {
                    ++nr_koloru;
                    colors[i] = nr_koloru;
                }
                else
                {
                    for (int j = 1; j <= nr_koloru; j++)
                    {
                        if (!NodeColors.Contains(j))
                        {
                            colors[i] = j;
                            break;
                        }
                    }
                }
            }
        
        /* ZMIENIC */ //int k=g.VerticesCount;
        /* ZMIENIC */ //colors=new int[k];
        /* ZMIENIC */ //for ( int i=0 ; i<k ; ++i )
        /* ZMIENIC */ //    colors[i]=i+1;
            /* ZMIENIC */

        /*
            if (Test(g, colors))
                Console.WriteLine("Check POSITIVE");
            else
                Console.WriteLine("Check NEGATIVE");
         */

            return nr_koloru;
        }

    // koloruje graf algorytmem z powrotami (optymalnie)
    public static int BacktrackingColor(this IGraph g, out int[] colors)
        {
            if (g == null)
            {
                colors = null;
                return 0;
            }
            if (g.VerticesCount == 0)
            {
                colors = null;
                return 0;
            }

            if (g.VerticesCount == 1)
            {
                colors = new int[1];
                colors[0] = 1;
                return 1;
            }

        var gc = new Coloring(g);
        
        int EdgesCountFull = -1;
        if (g.Directed)
            EdgesCountFull = g.VerticesCount * (g.VerticesCount - 1);
        else
            EdgesCountFull = g.VerticesCount * (g.VerticesCount - 1) / 2;

        if (g.EdgesCount == EdgesCountFull)
        {
            colors = new int[g.VerticesCount];
            int n = g.VerticesCount;
            for (int i = 1; i <= g.VerticesCount; i++)
                colors[i-1] = i;
            return n;
        }
        
        gc.Color(0, new int[g.VerticesCount], 0);
        colors = gc.bestColors;

        /*
        if (Test(g, colors))
            Console.WriteLine("Check POSITIVE");
        else
            Console.WriteLine("Check NEGATIVE");
         */

        return gc.bestColorsNumber;
        }

    internal static bool Test(IGraph g, int[] colors)
    {
        int n = g.VerticesCount;
        for (int i = 0; i < n; i++)
        {
            int moj_kolor = colors[i];
            foreach (Edge e in g.OutEdges(i))
            {
                if (colors[e.To] == moj_kolor)
                    return false;
            }
        }
        return true;
    }

    // klasa pomocnicza dla algorytmu z powrotami
    private sealed class Coloring
        {
        
        // tablica pamietajaca najlepsze dotychczas znalezione pokolorowanie
        internal int[] bestColors=null;

        // zmienna pamietajaca liczbe kolorow w najlepszym dotychczas znalezionym pokolorowaniu
        internal int bestColorsNumber;

        // badany graf
        private IGraph g;
        
        // konstruktor
        internal Coloring(IGraph g)
            {
            this.g=g;
            bestColorsNumber=g.VerticesCount+1;
            }

        // rekurencyjna metoda znajdujaca najlepsze pokolorowanie
        // v - wierzcholek do pokolorowania
        // colors - tablica kolorow
        // n - liczba kolorow uzytych w pokolorowaniu zapisanym w colors

        internal void Color(int v, int[] colors, int n)
            {
            // tu zaimplementowac algorytm z powrotami
            
            /* ZMIENIC */ //int k=g.VerticesCount;
            /* ZMIENIC */ //bestColors=new int[k];
            /* ZMIENIC */ //for ( int i=0 ; i<k ; ++i )
            /* ZMIENIC */ //    bestColors[i]=i+1;
            /* ZMIENIC */ //bestColorsNumber=k;
            
            //if (n >= colors.Length)
            //    throw new Exception("Coœ siê zwali³o");

            if (n >= bestColorsNumber)
                return;

            if (v >= g.VerticesCount)
            {
                if (n < bestColorsNumber)
                {
                    bestColorsNumber = n;
                    bestColors = (int[])colors.Clone();
                  //  Console.WriteLine("ALA");
                }
                return;
            }

            HashSet<int> NodeColors = new HashSet<int>();
            foreach (Edge e in g.OutEdges(v))
            {
                if (e.To >= v)
                    continue;
                    int color = colors[e.To];
                    if (color != 0 && !NodeColors.Contains(color))
                        NodeColors.Add(color);
            }

            if (n < NodeColors.Count)
                throw new Exception();

            int OutDegree = g.OutDegree(v);

            if (v % 2 == 0)
            {
                for (int i = 1; i <= n; i++)
                    if (!NodeColors.Contains(i))
                    {
                        colors[v] = i;
                        Color(v + 1, colors, n);
                    }
            }
            else
            {
                for (int i = n; i >= 1; i--)
                    if (!NodeColors.Contains(i))
                    {
                        colors[v] = i;
                        Color(v + 1, colors, n);
                    }
            }
                colors[v] = n + 1;
                Color(v + 1, colors, n + 1);
            
                //colors[v] = 0;
            }
        }  // class Coloring
    }  // class ColoringExtender
}  // namespace ASD.Graph
