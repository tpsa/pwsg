namespace BooksStacking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Program
    {
        /// <summary>
        /// Procedura wyliczająca optymalny (najwyższy stos)
        /// </summary>
        /// <param name="books">Lista dostępnych książek</param>
        /// <param name="stack">Parametr wyjściowy, w którym powinien znaleźć się optymalny stos</param>
        /// <returns>Wysokość optymalnego stosu</returns>
        /// 

        public static List<Book> GetHighestStack(List<List<Book>> list, List<Book> s = null)
        {
            int max = 0;
            if (list.Count == 0) 
                return null;
            List<Book> maxStack = list[0];
            foreach (List<Book> stack in list)
            {
               
                int suma = 0;
                foreach (Book b in stack)
                    suma += b.Height;
                if (suma > max)
                {
                    max = suma;
                    maxStack = stack;
                }
            }
            list.Remove(maxStack);
            return maxStack;
        }

        public static int HighestStack(IList<Book> books, out IList<Book> stack)
        {
            int max = 0;
            Book[] ClBooks = new Book[books.Count];
            List<List<Book>> stacks = new List<List<Book>>();

            for (int i = 0; i < books.Count; i++)
            {
                List<Book> stackMy = new List<Book>();
                stackMy.Add(books[i]);
                stacks.Add(stackMy);
            }

            List<Book> highest = new List<Book>();
 
            List<Book> Max = null;

            while (true)
            {
                if (stacks.Count == 1)
                {
                    Max = stacks[0];
                    break;
                }
                List<Book> s = GetHighestStack(stacks);
                List<Book> s2 = GetHighestStack(stacks, s);
                if (s == null)
                {
                    Max = s;
                    break;
                }
                if (s2 == null) break;
                if (s == s2)
                {
                    throw new Exception();
                }

                List<Book> s3 = s.ToList<Book>();

                    Book LastBook = s[s.Count - 1];
                    Book SecBook = s2[0];

                    if (LastBook.Width > SecBook.Width
                        && LastBook.Height > SecBook.Height)
                    {
                        s3.AddRange(s2);
                        stacks.Add(s3);
                    }
                

                //stacks.Add(s3);

            }

            foreach (Book b in Max)
            {
                max += b.Thickness;
            }

            //throw new NotImplementedException();
            stack = Max;
            return max;
        }

      

    }
}