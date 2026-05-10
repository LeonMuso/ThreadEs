using System.Diagnostics;

namespace EsThread_1._13
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            var t1 = new Thread(() => Thread1());
            var t2 = new Thread(() => Thread2());
            var t3 = new Thread(() => Thread3());
            sw.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            sw.Stop();
            Console.WriteLine($"Per fare tutto ci ho messo {sw.ElapsedMilliseconds} ms");
        }

        private static void Thread1()
        {
            Thread.Sleep(3000);
        }

        private static void Thread2()
        {
            Thread.Sleep(4000);
        }

        private static void Thread3()
        {
            Thread.Sleep(5000);
        }
    }
}
