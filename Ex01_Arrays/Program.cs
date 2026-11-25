namespace Ex01_Arrays
{
    internal class Program
    {
        static int BestStreak(int[] arr, out int start, out int end)
        {
            if (arr == null || arr.Length == 0)
            {
                throw new ArgumentException("Array cannot be empty");
            }

            int current = arr[0];
            int best = arr[0];
            start = 0;
            end = 0;
            int tempStart = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                if (current < 0)
                {
                    current = arr[i];
                    tempStart = i;
                }
                else
                {
                    current += arr[i];
                }

                if (current > best)
                {
                    best = current;
                    start = tempStart;
                    end = i;
                }

            }

            return best;
        }

        static void Reverse(int[] arr, int from, int to)
        {
            if (arr == null || arr.Length == 0)
            {
                return;
            }

            while (from < to)
            {  
                int temp = arr[from];
                arr[from] = arr[to];
                arr[to] = temp;

                from++;
                to--;
            }
        }

        static void RotateRight(int[] arr, int k)
        {
            k = k % arr.Length;

            if (arr == null || arr.Length <= 1 || k == 0)
                return;

            Reverse(arr, 0, arr.Length - 1);
            Reverse(arr, 0, k - 1);
            Reverse(arr, k, arr.Length - 1);
        }

        static int[] DaysUntilBetter(int[] scores)
        {
            if (scores == null || scores.Length == 0)
                return new int[0];

            int[] result = new int[scores.Length];
            int[] stack = new int[scores.Length];
            int top = 0;

            for(int i = 0; i < scores.Length; i++)
            {
                while(top > 0 && scores[i] > scores[stack[top - 1]])
                {
                    top--;
                    int Day = stack[top];
                    result[Day] = i - Day;
                }

                stack[top] = i;
                top++; 
            }

            return result;
        }

        static void Main(string[] args)
        {
            int[] changes = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            int[] nums1 = { -3 , -1, -2};
            int[] nums2 = { 1, 2, 3, 4, 5, 6, 7 };
            int[] levelScores = { 73, 74, 75, 71, 69, 72, 76, 73 };

            int bestStreak = BestStreak(changes, out int startIndex, out int endIndex);

            Console.WriteLine($"Best Streak: {bestStreak}, Start Index: {startIndex}, End Index: {endIndex}");

            bestStreak = BestStreak(nums1, out startIndex, out endIndex);

            Console.WriteLine($"Best Streak: {bestStreak}, Start Index: {startIndex}, End Index: {endIndex}");

            RotateRight(nums2, 10);

            foreach(int num in nums2)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine();

            int[] Result = DaysUntilBetter(levelScores);

            foreach (int num in Result)
            {
                Console.Write(num + " ");
            }
        }
    }
}
