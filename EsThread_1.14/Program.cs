using System.ComponentModel;

namespace EsThread_1._14
{
    internal class Program
    {
        private static bool _fermatiAdesso = false;
        static void Main(string[] args)
        {
            var t1 = new Thread(() => Worker());
            t1.Start();
            Thread.Sleep(1000);
            _fermatiAdesso = true;
            t1.Join();
            Console.WriteLine("FINITO");
        }

        private static void Worker()
        {
            while (!_fermatiAdesso)
            {
                Console.WriteLine($"numero casuale: {Random.Shared.Next(1, 50)}");
                Thread.Sleep(200);
            }
        }
    }
}
