namespace EsThread_1._22
{
    enum Piatti
    {
        Pizza,
        Pasta,
        Pizzo
    }
    internal class Program
    {
        private static Queue<Piatti> codaPietanze = new Queue<Piatti>();
        private static object blocco = new object();
        static int piattiFiniti = 0;

        static void Main(string[] args)
        {
            var ca1 = new Thread(() => ServiPietanza());
            var ca2 = new Thread(() => ServiPietanza());
            
            var c1 = new Thread(() => CucinaPiatto());
            var c2 = new Thread(() => CucinaPiatto());
            var c3 = new Thread(() => CucinaPiatto());
            var c4 = new Thread(() => CucinaPiatto());
            var c5 = new Thread(() => CucinaPiatto());
            var c6 = new Thread(() => CucinaPiatto());
            var c7 = new Thread(() => CucinaPiatto());
            var c8 = new Thread(() => CucinaPiatto());
            var c9 = new Thread(() => CucinaPiatto());
            var c10 = new Thread(() => CucinaPiatto());

            ca1.Start();
            ca2.Start();

            c1.Start();
            c2.Start();
            c3.Start();
            c4.Start();
            c5.Start();
            c6.Start();
            c7.Start();
            c8.Start();
            c9.Start();
            c10.Start();

            c1.Join();
            c2.Join();
            c3.Join();
            c4.Join();
            c5.Join();
            c6.Join();
            c7.Join();
            c8.Join();
            c9.Join();
            c10.Join();

            piattiFiniti = 10;

            ca1.Join();
            ca2.Join();

            Console.WriteLine("\nFiniti i servizi");

        }

        private static void CucinaPiatto()
        {
            for (int i = 0; i < 20; i++)
            {
                Piatti pCucinato = (Piatti)Random.Shared.Next(0, 3);
                lock (blocco)
                {
                    codaPietanze.Enqueue(pCucinato);
                    Console.WriteLine($"Ordinata: {pCucinato}");
                    Monitor.Pulse(blocco);
                }
                Thread.Sleep(1000);
            }

        }

        private static void ServiPietanza()
        {
            while (true)
            {
                Piatti ordine;
                lock (blocco)
                {
                    while (codaPietanze.Count == 0)
                    {
                        if (piattiFiniti == 10)
                        {
                            return;
                        }
                        Monitor.Wait(blocco);
                    }
                    ordine = codaPietanze.Dequeue();
                }

                Console.WriteLine($"Il cameriere ha servito: {ordine}");
                Thread.Sleep(300);
            }
        }
    }
}
