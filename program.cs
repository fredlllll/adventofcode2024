 internal class Program
 {
     static void Init()
     {

     }

     static void Task1()
     {
		Init();
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