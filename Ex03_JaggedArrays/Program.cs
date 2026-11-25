using System.Globalization;

namespace Ex03_JaggedArrays
{
    internal class Program
    {
        static int[][] Pascal(int rows)
        {
            int[][] triangle = new int[rows][];

            for(int i = 0; i < rows; i++)
            {
                triangle[i] = new int[i + 1];

                for (int j = 0; j < triangle[i].Length; j++)
                {
                    if (j < 1 || j + 1 == triangle[i].Length)
                    {
                        triangle[i][j] = 1;
                    }
                    else
                    {
                        triangle[i][j] = triangle[i - 1][j - 1] + triangle[i - 1][j];
                    }
                }
            }

            return triangle;
        }

        static int MinPathDown(int[][] m)
        {
            int[][] sum = new int[m.Length][];
            sum[m.Length - 1] = m[m.Length - 1];

            for(int i = m.Length - 2 ; i >= 0; i--)
            {
                sum[i] = new int[m[i].Length];
                for(int j = 0; j < m[i].Length; j++)
                {
                    if (sum[i + 1][j] <= sum[i + 1][j + 1])
                    {
                        sum[i][j] = m[i][j] + sum[i + 1][j];
                    }
                    else
                    {
                        sum[i][j] = m[i][j] + sum[i + 1][j + 1];
                    }
                }
            }

            return sum[0][0];
        }

        static int[] MinPath(int[][] m)
        {
            int[][] sum = new int[m.Length][];
            sum[m.Length - 1] = m[m.Length - 1];

            for (int i = m.Length - 2; i >= 0; i--)
            {
                sum[i] = new int[m[i].Length];
                for (int j = 0; j < m[i].Length; j++)
                {
                    if (sum[i + 1][j] <= sum[i + 1][j + 1])
                    {
                        sum[i][j] = m[i][j] + sum[i + 1][j];
                    }
                    else
                    {
                        sum[i][j] = m[i][j] + sum[i + 1][j + 1];
                    }
                }
            }

            int[] path = new int[m.Length];
            path[0] = m[0][0];
            int col = 0;

            for(int i = 1; i < sum.Length; i++)
            {
                if (sum[i][col] > sum[i][col + 1])
                {
                    col++;
                }

                path[i] = m[i][col];
            }

            return path;
        }

        static void PrintReportCard(string[] students, int[][] grades)
        {
            double[] averages = new double[students.Length];

            for(int i = 0; i < grades.Length; i++)
            {
                if (grades[i].Length == 0)
                {
                    averages[i] = 0.0;
                    Console.WriteLine(students[i] + ": no grades");
                }
                else
                {
                    for (int j = 0; j < grades[i].Length; j++)
                    {
                        averages[i] += grades[i][j];
                    }

                    averages[i] = averages[i] / grades[i].Length;

                    Console.WriteLine($"{students[i]}: {averages[i].ToString("0.00", CultureInfo.InvariantCulture)}"); ;
                }
            }

            double best = averages[0];
            int index = 0;

            for(int i = 1; i < averages.Length; i++)
            {
                if (averages[i] > best)
                {
                    best = averages[i];
                    index = i;
                }
            }

            Console.WriteLine($"Best: {students[index]} ({averages[index].ToString("0.00", CultureInfo.InvariantCulture)})");
        }

        static void Main(string[] args)
        {
            int[][] mountain =
            {
                new[] { 2 },
                new[] { 3, 4 },
                new[] { 6, 5, 7 },
                new[] { 4, 1, 8, 3 },
            };

            string[] students = { "Anna", "Bob", "Carl", "Dina" };
            int[][] grades =
            {
                new[] { 90, 85, 77 },
                new[] { 60 },
                new int[0],
                new[] { 100, 95, 98, 91 },
            };


            int[][] triangle = Pascal(5);
            Console.WriteLine("Pascal(5)");
            foreach (int[] row in triangle)
            {
                foreach (int num in row)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            int minPathDown = MinPathDown(mountain);
            Console.Write("MinPathDown(mountain) → ");
            Console.WriteLine(minPathDown);

            int[] minPath = MinPath(mountain);
            Console.Write("MinPath(mountain) → ");
            foreach(int num in minPath)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            PrintReportCard(students, grades);
        }
    }
}
