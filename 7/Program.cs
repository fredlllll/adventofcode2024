using System.Diagnostics;

namespace _7
{
    internal class Program
    {
        static readonly List<Equation> equations = new();
        static readonly List<Equation2> equation2s = new();
        static bool debug = false;

        static void Init()
        {
            equations.Clear();
            if (debug)
            {
                equations.Add(new Equation("190: 10 19"));
                equations.Add(new Equation("3267: 81 40 27"));
                equations.Add(new Equation("83: 17 5"));
                equations.Add(new Equation("156: 15 6"));
                equations.Add(new Equation("7290: 6 8 6 15"));
                equations.Add(new Equation("161011: 16 10 13"));
                equations.Add(new Equation("192: 17 8 14"));
                equations.Add(new Equation("21037: 9 7 18 13"));
                equations.Add(new Equation("292: 11 6 16 20"));
            }
            else
            {
                var lines = File.ReadAllLines("input.txt");

                foreach (var line in lines)
                {
                    equations.Add(new Equation(line));
                }
            }
        }

        static void Init2() {
            if (debug)
            {
                equations.Add(new Equation("190: 10 19"));
                equations.Add(new Equation("3267: 81 40 27"));
                equations.Add(new Equation("83: 17 5"));
                equations.Add(new Equation("156: 15 6"));
                equations.Add(new Equation("7290: 6 8 6 15"));
                equations.Add(new Equation("161011: 16 10 13"));
                equations.Add(new Equation("192: 17 8 14"));
                equations.Add(new Equation("21037: 9 7 18 13"));
                equations.Add(new Equation("292: 11 6 16 20"));
            }
            else
            {
                var lines = File.ReadAllLines("input.txt");

                foreach (var line in lines)
                {
                    equation2s.Add(new Equation2(line));
                }
            }
        }

        static void Task1()
        {
            Init();

            //equations[0].CanBeTrue();

            object numLock = new object();
            long totalCalibrationResult = 0;

            Parallel.ForEach(equations, (x) =>
            {
                if (x.CanBeTrue())
                {
                    lock (numLock)
                    {
                        totalCalibrationResult += x.testvalue;
                    }
                }
            });

            Console.WriteLine($"total calibration result: {totalCalibrationResult}");
        }

        static void Task2()
        {
            Init2();
            object numLock = new object();
            long totalCalibrationResult = 0;

            Parallel.ForEach(equation2s, (x) =>
            {
                if (x.CanBeTrue())
                {
                    lock (numLock)
                    {
                        totalCalibrationResult += x.testvalue;
                    }
                }
            });

            Console.WriteLine($"total calibration result with concat: {totalCalibrationResult}");
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
