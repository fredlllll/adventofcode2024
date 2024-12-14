using System.Diagnostics;

namespace _14
{
    internal class Program
    {
        static Space space;

        static void Init()
        {
            var lines = File.ReadAllLines("input.txt");
            space = new Space(101, 103, lines);
            //space = new Space(11, 7, "p=0,4 v=3,-3\r\np=6,3 v=-1,-3\r\np=10,3 v=-1,2\r\np=2,0 v=2,-1\r\np=0,0 v=1,3\r\np=3,0 v=-2,-2\r\np=7,6 v=-1,-3\r\np=3,0 v=-1,-2\r\np=9,3 v=2,3\r\np=7,3 v=-1,2\r\np=2,4 v=2,-3\r\np=9,5 v=-3,-3".Split("\r\n"));
        }

        static void Task1()
        {
            Init();

            space.DoSteps(100);

            Console.WriteLine($"safety factor is {space.GetSafetyFactor()}");
        }

        static void Task2()
        {
            Directory.CreateDirectory("output");
            Init();
            for (int i = 0; i < 8000; i++)
            {
                space.SaveImage(i);
                space.DoSteps(1);
            }
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
