using System.Diagnostics;
using System.Runtime.InteropServices;

namespace _6
{
    internal class Program
    {
        static Field? field;

        static void Init()
        {
            field = new Field(File.ReadAllText("input.txt"));
        }

        static void Task1()
        {
            Init();
            if (field == null)
            {
                return;
            }

            var guard = field.GetGuardStart();

            while (field.MoveGuard(guard)) { }

            Console.WriteLine($"Guard visited {field.visitedPositions.Count} positions");
        }

        static void Task2()
        {
            if (field == null)
            {
                return;
            }

            field.Reset();
            var startGuard = field.GetGuardStart();

            object numLock = new object();
            int numPossibleLoops = 0;

            ParallelOptions options = new ParallelOptions();

            Parallel.For(0, field.height, options, (y) =>
            {
                Parallel.For(0, field.width, options, (x) =>
                {
                    var posChar = field.Get(x, y);
                    if (posChar == '.') //is free, can place obstacle
                    {
                        var tempField = new Field(field);
                        var guard = new Guard(startGuard);
                        tempField.Set(x, y, '#');
                        tempField.visitedPositions.Add((guard.x, guard.y));
                        tempField.visitedPositionsWithOrientation.Add((guard.x, guard.y, guard.orientation));

                        try
                        {
                            while (tempField.MoveGuardLoopDetect(guard)) { }
                        }
                        catch (LoopException)
                        {
                            //endless loop detected
                            lock (numLock)
                            {
                                numPossibleLoops++;
                            }
                        }
                    }
                });
            });

            /*
            for (int y = 0; y < field.height; y++)
            {
                for (int x  = 0; x < field.width; x++)
                {
                    var posChar = field.Get(x, y);
                    if(posChar == '.') //is free, can place obstacle
                    {
                        var guard = new Guard(startGuard);
                        field.Reset();
                        field.Set(x, y, '#');
                        field.visitedPositions.Add((guard.x, guard.y));
                        field.visitedPositionsWithOrientation.Add((guard.x, guard.y, guard.orientation));

                        try
                        {
                            while (field.MoveGuardLoopDetect(guard)) { }
                        }catch(LoopException) {
                            //endless loop detected
                            numPossibleLoops++;
                        }

                        field.Set(x, y, '.');
                    }
                }
            }*/

            Console.WriteLine($"amount of possible loops: {numPossibleLoops}");
        }

        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            Task1();
            Console.WriteLine($"Task1 took {sw.Elapsed}");
            Task2();
            sw.Stop();
            Console.WriteLine($"Execution took {sw.Elapsed}");
        }
    }
}
