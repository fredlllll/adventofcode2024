using System.Data;
using System.Diagnostics;

namespace _8
{
    internal class Program
    {
        static Map? map;

        static void Init()
        {
            map = new Map(File.ReadAllText("input.txt"));
        }

        static void Task1()
        {
            Init();
            if (map == null)
            {
                return;
            }

            var antennas = map.GetAntennas();

            HashSet<(int, int)> antiNodes = new();

            foreach (var kv in antennas)
            {
                var ants = kv.Value;
                for (int i = 0; i < ants.Count; i++)
                {
                    for (int j = 0; j < ants.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        var a = ants[i];
                        var b = ants[j];

                        int diffX = a.position.x - b.position.x;
                        int diffY = a.position.y - b.position.y;

                        int nodeX = a.position.x + diffX;
                        int nodeY = a.position.y + diffY;
                        if (map.isInside(nodeX, nodeY))
                        {
                            antiNodes.Add((nodeX, nodeY));
                        }
                    }
                }
            }

            Console.WriteLine($"Number of unique locations with antinode: {antiNodes.Count}");
        }

        static void Task2()
        {
            Init();
            if (map == null)
            {
                return;
            }

            var antennas = map.GetAntennas();

            HashSet<(int, int)> antiNodes = new();

            foreach (var kv in antennas)
            {
                var ants = kv.Value;
                for (int i = 0; i < ants.Count; i++)
                {
                    for (int j = 0; j < ants.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        var a = ants[i];
                        var b = ants[j];

                        int diffX = a.position.x - b.position.x;
                        int diffY = a.position.y - b.position.y;

                        int nodeX = a.position.x;
                        int nodeY = a.position.y;
                        while (map.isInside(nodeX, nodeY))
                        {
                            antiNodes.Add((nodeX, nodeY));
                            nodeX += diffX;
                            nodeY += diffY;
                        }
                    }
                }
            }

            Console.WriteLine($"Number of unique locations with antinode including harmonics: {antiNodes.Count}");
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
