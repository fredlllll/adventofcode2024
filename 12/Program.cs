using Shared;
using System.Diagnostics;

namespace _12
{
    internal class Program
    {
        static Garden? garden;
        static bool debug = false;

        static void Init()
        {
            if (debug)
            {
                garden = new Garden("RRRRIICCFF\r\nRRRRIICCCF\r\nVVRRRCCFFF\r\nVVRCCCJFFF\r\nVVVVCJJCFE\r\nVVIVCCJJEE\r\nVVIIICJJEE\r\nMIIIIIJJEE\r\nMIIISIJEEE\r\nMMMISSJEEE");
            }
            else
            {
                garden = new Garden(File.ReadAllText("input.txt"));
            }
        }

        static void Task1()
        {
            Init();
            if (garden == null)
            {
                return;
            }

            var price = garden.GetFencePrice();
            Console.WriteLine($"Fence price is {price}");
        }

        static void Task2()
        {
            if (garden == null)
            {
                return;
            }

            var price = garden.GetFenceBulkPrice();
            Console.WriteLine($"Fence bulk price is {price}");
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
