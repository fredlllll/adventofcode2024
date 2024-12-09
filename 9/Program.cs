using System.Diagnostics;

namespace _9
{
    internal class Program
    {
        static FileSystem? fileSystem;
        static bool debug = false;

        static void Init()
        {
            if (debug)
            {
                fileSystem = new FileSystem("2333133121414131402");
                Console.WriteLine(fileSystem.ToString());
            }
            else
            {
                fileSystem = new FileSystem(File.ReadAllText("input.txt").TrimEnd());
            }
        }

        static void Task1()
        {
            Init();

            if (fileSystem == null)
            {
                return;
            }

            fileSystem.BunchUp();

            if (debug)
            {
                Console.WriteLine(fileSystem.ToString()); 
            }

            Console.WriteLine($"Checksum with BunchUp: {fileSystem.GetChecksum()}");
        }

        static void Task2()
        {
            Init();
            if (fileSystem == null)
            {
                return;
            }

            fileSystem.Defrag();

            if (debug)
            {
                Console.WriteLine(fileSystem.ToString());
            }

            Console.WriteLine($"Checksum with Defrag: {fileSystem.GetChecksum()}");
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
