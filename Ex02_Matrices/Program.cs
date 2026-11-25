namespace Ex02_Matrices
{
    internal class Program
    {
        const int MaxPerSlot = 3;

        static int GateTotal(int[,] flights, int gate)
        {
            int total = 0;
            for (int i = 0; i < flights.GetLength(1); i++)
            {
                int x = flights[gate, i];
                total += x;
            }
            return total;
        }

        static int HourTotal(int[,] flights, int hour)
        {
            int total = 0;
            for (int i = 0; i < flights.GetLength(0); i++)
            {
                int x = flights[i, hour];
                total += x;
            }
            return total;
        }

        static void PrintBoard(int[,] flights, string[] gates, string[] hours)
        {
            if (flights.GetLength(0) != gates.Length || flights.GetLength(1) != hours.Length)
            {
                throw new ArgumentException("The number of rows and columns must correspond.");
            }

            Console.Write("      ");

            foreach(string hour in hours)
            {
                Console.Write($"{hour, 7}");
            }

            Console.WriteLine($" | {"Total",5}");

            for(int i = 0; i < flights.GetLength(0); i++)
            {
                Console.Write($"{gates[i],-6}");

                for (int j =0; j < flights.GetLength(1); j++)
                {
                    Console.Write($"{flights[i,j], 7}");
                }

                int gateTotal = GateTotal(flights, i);
                Console.WriteLine($" | {gateTotal,5}");
            }

            Console.WriteLine("-------------------------------------------------");
            Console.Write($"{"Total",-6}");
            int total = 0;
            for (int i = 0; i < flights.GetLength(1); i++)
            {
                int hourTotal = HourTotal(flights, i);
                total += hourTotal;
                Console.Write($"{hourTotal,7}");
            }
            Console.WriteLine($" | {total,5}");
            Console.WriteLine();
            Console.WriteLine();
        }

        static int BusiestGate(int[,] flights)
        {
            int gateTotal = 0;
            int best = GateTotal(flights, 0);
            int index = 0;

            for(int i = 1; i < flights.GetLength(0); i++)
            {
                gateTotal = GateTotal(flights, i);

                if(gateTotal > best)
                {
                    best = gateTotal;
                    index = i;
                }
            }

            return index;
        }

        static int RushHour(int[,] flights)
        {
            int hourTotal = 0;
            int best = HourTotal(flights, 0);
            int index = 0;

            for (int i = 1; i < flights.GetLength(1); i++)
            {
                hourTotal = HourTotal(flights, i);

                if (hourTotal > best)
                {
                    best = hourTotal;
                    index = i;
                }
            }

            return index;
        }

        static int QuietestHour(int[,] flights)
        {
            int hourTotal = 0;
            int best = HourTotal(flights, 0);
            int index = 0;

            for (int i = 1; i < flights.GetLength(1); i++)
            {
                hourTotal = HourTotal(flights, i);

                if (hourTotal < best)
                {
                    best = hourTotal;
                    index = i;
                }
            }

            return index;
        }

        static string[] FullSlots(int[,] flights, string[] gates, string[] hours)
        {
            int count = 0;
            for(int i = 0; i < flights.GetLength(0); i++)
            {
                for( int j = 0; j < flights.GetLength(1); j++)
                {
                    if (flights[i,j] == MaxPerSlot)
                    {
                        count++;
                    }
                }
            }

            string[] result = new string[count];
            int index = 0;

            for (int i = 0; i < flights.GetLength(0); i++)
            {
                for (int j = 0; j < flights.GetLength(1); j++)
                {
                    if (flights[i, j] == MaxPerSlot)
                    {
                        result[index] = $"{gates[i]} at {hours[j]}  ";
                        index++;
                    }
                }
            }

            return result;
        }

        static string[] IdleSlots(int[,] flights, string[] gates, string[] hours)
        {
            int count = 0;
            for (int i = 0; i < flights.GetLength(0); i++)
            {
                for (int j = 0; j < flights.GetLength(1); j++)
                {
                    if (flights[i, j] == 0)
                    {
                        count++;
                    }
                }
            }

            string[] result = new string[count];
            int index = 0;

            for (int i = 0; i < flights.GetLength(0); i++)
            {
                for (int j = 0; j < flights.GetLength(1); j++)
                {
                    if (flights[i, j] == 0)
                    {
                        result[index] = $"{gates[i]} at {hours[j]}  ";
                        index++;
                    }
                }
            }

            return result;
        }

        static string? AssignFlight(int[,] flights, string[] gates, int hour)
        {
            string? result = null;

            if (hour < 0 || hour > flights.GetLength(1))
            {
                throw new ArgumentException("Maximum index 4");
            }

            int best = flights[0, hour];
            int index = 0;

            for (int i = 1; i < flights.GetLength(0); i++)
            {
                if (flights[i,hour] < best)
                {
                    best = flights[i, hour];
                    index = i;
                }
            }

            if (best < MaxPerSlot)
            {
                flights[index, hour] += 1;
                result = gates[index];
            }

            return result;
        }

        static void Main(string[] args)
        {
            string[] gates = { "A1", "A2", "B1", "B2" };
            string[] hours = { "06:00", "07:00", "08:00", "09:00", "10:00" };

            int[,] flights =
            {
                { 2, 3, 1, 0, 2 }, // A1
                { 1, 3, 3, 2, 0 }, // A2
                { 0, 2, 3, 1, 1 }, // B1
                { 3, 1, 0, 0, 2 }, // B2
            };

            PrintBoard(flights, gates, hours);

            int busiestGate = BusiestGate(flights);
            Console.Write($"{"Busiest gate:",-18}");
            Console.WriteLine($"{gates[busiestGate]} ({GateTotal(flights, busiestGate)} flights)");

            int rushHour = RushHour(flights);
            Console.Write($"{"Rush hour:",-18}");
            Console.WriteLine($"{hours[rushHour]} ({HourTotal(flights, rushHour)} flights)");

            int quietestHour = QuietestHour(flights);
            Console.Write($"{"Quietest hour:",-18}");
            Console.WriteLine($"{hours[quietestHour]} ({HourTotal(flights, quietestHour)} flights)");

            Console.Write("Full:  ");
            string[] fullSlots = FullSlots(flights, gates, hours);
            foreach(string slot in fullSlots)
            {
                Console.Write(slot);
            }
            Console.WriteLine();
            Console.Write("Idle:  ");
            string[] idleSlots = IdleSlots(flights, gates, hours);
            foreach (string slot in idleSlots)
            {
                Console.Write(slot);
            }
            Console.WriteLine();
            Console.WriteLine();

            string? assignFlight = AssignFlight(flights, gates, 1);
            if(assignFlight == null)
            {
                Console.WriteLine("all gates are full");
            }
            else
            {
                Console.WriteLine(assignFlight);
            }
            Console.WriteLine();

            Console.WriteLine(HourTotal(flights, 1));
        }
    }
}
