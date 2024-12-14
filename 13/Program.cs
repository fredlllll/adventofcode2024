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

            long sum = 0;
            int prizesSum = 0;
            foreach (var machine in machines)
            {
                var cost = machine.CalculateMinCost();
                if (cost > 0)
                {
                    sum += cost;
                    prizesSum++;
                }
            }
            Console.WriteLine($"total winnable prizes: {prizesSum} cost: {sum}");
        }

        static void Task2()
        {
            Init();

            long sum = 0;
            long prizesSum = 0;
            foreach (var machine in machines)
            {
                machine.AddToPrizePos(10000000000000);
                var cost = machine.CalculateMinCost();
                if (cost > 0)
                {
                    sum += cost;
                    prizesSum++;
                }
            }
            Console.WriteLine($"total winnable prizes: {prizesSum} cost: {sum}");
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
