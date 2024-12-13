using System.Diagnostics;

namespace _13
{
    internal class Program
    {
        static List<Machine> machines = new();

        static void Init()
        {
            string input = File.ReadAllText("input.txt");
            input = input.Replace("\r", "");

            foreach(var str in input.Split("\n\n"))
            {
                machines.Add(new Machine(str));
            }
        }

        static void Task1()
        {
            Init();

            foreach (var machine in machines)
            {
                machine.CalculateMinCost();
            }
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
