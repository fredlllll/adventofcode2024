using System.Diagnostics;

namespace _5
{
    internal class Program
    {
        static RuleBook rules = new();
        static List<Update> updates = new();

        static void Init()
        {
            using (var fs = new FileStream("input.txt", FileMode.Open))
            using (var sr = new StreamReader(fs))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }

                    var or = new OrderingRule(line);
                    rules.Add(or);
                }

                while (true)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }

                    updates.Add(new Update(line));
                }
            }
        }

        static void Task1()
        {
            Init();
            var inOrder = updates.Where(x=> x.IsInOrder(rules)).ToList();
            Console.WriteLine($"{inOrder.Count}/{updates.Count} updates are in order");

            var middlePageSum = inOrder.Sum(x => x.GetMiddlePage());

            Console.WriteLine($"Sum of middle pages of ordered updates: {middlePageSum}");
        }

        static void Task2()
        {
            var notInOrder = updates.Where(x => !x.IsInOrder(rules)).ToList();
            int middlePageSum = 0;
            foreach (var update  in notInOrder)
            {
                update.PutInOrder(rules);
                middlePageSum += update.GetMiddlePage();
            }
            Console.WriteLine($"Sum of middle pages of reordered updates: {middlePageSum}");
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
