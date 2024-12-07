using System.Diagnostics;

namespace _1
{
    internal class Program
    {
        static readonly List<int> listOne = new List<int>();
        static readonly List<int> listTwo = new List<int>();
        static bool debug = false;


        static void Init()
        {
            listOne.Clear();
            listTwo.Clear();
            if (debug)
            {
                listOne.AddRange([3, 4, 2, 1, 3, 3]);
                listTwo.AddRange([4, 3, 5, 3, 9, 3]);
                return;
            }
            using (FileStream fs = new FileStream("input.txt", FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    var parts = line.Split(' ').Where(x => x.Length > 0).ToArray();
                    listOne.Add(int.Parse(parts[0]));
                    listTwo.Add(int.Parse(parts[1]));
                }
            }
        }

        static void Task1()
        {
            Init();

            listOne.Sort();
            listTwo.Sort();

            long totalDistance = 0;
            for (int i = 0; i < listOne.Count; i++)
            {
                totalDistance += Math.Abs(listOne[i] - listTwo[i]);
            }

            Console.WriteLine($"total distance is {totalDistance}");
        }

        static void Task2()
        {
            Init();

            long similarityScore = 0;
            for (int i = 0; i < listOne.Count; i++)
            {
                var itemOne = listOne[i];
                var occurence = listTwo.Where(x => x == itemOne).Count();
                similarityScore += occurence * itemOne;
            }

            Console.WriteLine($"similary score is {similarityScore}");
        }

        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            Task1();
            Task2();
            sw.Stop();
            Console.WriteLine($"Execution took {sw.Elapsed}");
        }
    }
}
