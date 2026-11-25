namespace Ex04_Delegates
{
    internal class Program
    {
        static int[] Filter(int[] items, IntCheck check)
        {
            int count = 0;
            for(int i = 0; i < items.Length; i++)
            {
                if(check(items[i]) == true)
                {
                    count++;
                }
            }

            int[] result = new int[count];

            int x = -1;
            for (int i = 0; i < items.Length; i++)
            {
                
                if (check(items[i]) == true)
                {
                    x++;
                    result[x] = items[i];
                }
            }

            return result;
        }

        static string[] FilterText(string[] items, TextCheck check)
        {
            int count = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (check(items[i]) == true)
                {
                    count++;
                }
            }

            string[] result = new string[count];

            int x = -1;
            for (int i = 0; i < items.Length; i++)
            {

                if (check(items[i]) == true)
                {
                    x++;
                    result[x] = items[i];
                }
            }

            return result;
        }

        static int[] Map(int[] items, IntMap map)
        {
            int[] result = new int[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                result[i] = map(items[i]);
            }

            return result;
        }

        static int Reduce(int[] items, int seed, IntStep step)
        {
            int acc = seed;

            for(int i = 0; i < items.Length; i++)
            {
                acc = step(acc, items[i]);
            }

            return acc;
        }

        static string ReduceText(string[] items, string seed, TextStep step)
        {
            string acc = seed;

            for (int i = 0; i < items.Length; i++)
            {
                acc = step(acc, items[i]);
            }

            return acc;
        }

        static void Main(string[] args)
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string[] words = { "apple", "kiwi", "banana", "fig" };

            int[] evens = Filter(numbers, n => n % 2 == 0);
            foreach(int num in evens)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            string[] longWords = FilterText(words, w => w.Length > 3);
            foreach (string word in longWords)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();

            int[] squares = Map(evens, n => n * n);
            foreach (int x in squares)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine();

            Console.WriteLine(Reduce(squares, 0, (acc, n) => acc + n));
            Console.WriteLine(ReduceText(words, "", (acc, w) => acc + w[0]));
            Console.WriteLine(Reduce(numbers, numbers[0], (acc, n) => n > acc ? n:acc));
            Console.WriteLine(ReduceText(words, "", (acc, w) => w.Length > acc.Length? w:acc));
        }


        public delegate bool IntCheck(int n);           
        public delegate int IntMap(int n);              
        public delegate int IntStep(int acc, int n);  

        public delegate bool TextCheck(string s);               
        public delegate string TextMap(string s);               
        public delegate string TextStep(string acc, string s);
    }
}
