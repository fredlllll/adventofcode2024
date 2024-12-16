using System.Diagnostics;
using System.Net.Http.Headers;

namespace _15
{
    internal class Program
    {
        static Warehouse warehouse;
        static string movements;

        static void Init()
        {
            var input = File.ReadAllText("input.txt").Replace("\r", "");
            var parts = input.Split("\n\n");

            warehouse = new Warehouse(parts[0].Trim());
            movements = parts[1].Replace("\n", "").Trim();
        }

        static void Task1()
        {
            Init();

            warehouse.ProcessMovement(movements);

            Console.WriteLine($"sum of all gps coordinates: {warehouse.GetSumOfGpsCoordinates()}");
        }

        static void Task2()
        {
            Init();
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
