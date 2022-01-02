using System;
using System.Linq;
using System.Threading.Tasks;

namespace Work_System_Parallel_Csharp_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1-----");

            Task task = Task.Run(() => Console.WriteLine(DateTime.Now));
            Task task1 = Task.Factory.StartNew(() => Console.WriteLine(DateTime.Now));
            Task task2 = new Task(() => Console.WriteLine(DateTime.Now));
            task2.Start();
            task.Wait();
            task1.Wait();
            task2.Wait();

            Console.WriteLine("Task 2-----");

            Task task3 = Task.Run(() =>
            {
                bool simple;
                for (int i = 1; i < 1000; i++)
                {
                    simple = true;
                    for (int j = 2; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            simple = false;
                            break;
                        }
                    }
                    if (simple)
                    {
                        Console.Write(i + " ");
                    }
                }
            });

            task3.Wait();

            Console.WriteLine("\nTask 3-----");

            Console.WriteLine("Enter min => ");
            int min = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter max => ");
            int max = Int32.Parse(Console.ReadLine());

            Task<int> task4 = Task.Run(() => Task3(min, max));

            Console.WriteLine($"Result: {task4.Result}");

            Console.WriteLine("Task 4-----");

            Task task5 = Task.Run(() => Task4());

            task5.Wait();

            Console.WriteLine("Task 5-----");

            Random random = new Random();

            int[] arr = new int[50];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(0, 100);
            }

            Task<int[]> task6 = Task.Run(() => arr.Distinct().ToArray());
            task6.ContinueWith(array => { Array.Sort(array.Result); return array; });
            int find = arr[random.Next(0, arr.Length - 1)];
            Console.WriteLine($"Find: {find}");
            task6.ContinueWith(array =>
            {
                Console.WriteLine($"Found:{array.Result[Array.BinarySearch(array.Result, find)]}");
            });

            arr.Distinct();
            arr.OrderBy(x => x);

            Console.ReadLine();
        }

        private static int Task3(int min, int max)
        {
            int numbers = 0;
            bool simple;
            for (int i = min; i < max; i++)
            {
                simple = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        simple = false;
                        break;
                    }
                }
                if (simple)
                {
                    numbers++;
                }
            }
            return numbers;
        }

        private static void Task4()
        {
            Random random = new Random();

            int[] arr = new int[10];

            for (int i = 0; i < arr.Length; i++)
            {
                int num = random.Next(0, 100);
                if (arr.Contains(num))
                {
                    i--;
                }
                else
                {
                    arr[i] = num;
                }
            }

            Console.WriteLine(arr.Max());
            Console.WriteLine(arr.Min());
            Console.WriteLine(arr.Average());
            Console.WriteLine(arr.Sum());
        }
    }
}
