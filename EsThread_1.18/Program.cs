namespace EsThread_1._18
{
    enum Bevanda
    {
        LatteMacchiato,
        Caffe,
        Cappuccino
    }
    internal class Program
    {
        private static Queue<Bevanda> codaOrdinazioni = new Queue<Bevanda>();
        private static object blocco = new object();
        static int clientiFiniti = 0;

        static void Main(string[] args)
        {
            var b1 = new Thread(() => PreparaOrdini());
            b1.Start();
            var c1 = new Thread(() => Cliente());
            var c2 = new Thread(() => Cliente());
            var c3 = new Thread(() => Cliente());

            c1.Start();
            c2.Start();
            c3.Start();

            c1.Join();
            c2.Join();
            c3.Join();
            
            clientiFiniti = 3;
            
            b1.Join();

            Console.WriteLine("\nFinite le ordinazioni");

        }

        private static void Cliente()
        {
            for (int i = 0; i < 10; i++)
            {
                Bevanda bOrdinata = (Bevanda)Random.Shared.Next(0, 3);
                lock (blocco)
                {
                    codaOrdinazioni.Enqueue(bOrdinata);
                    Console.WriteLine($"Ordinata: {bOrdinata}");
                    Monitor.Pulse(blocco);
                }
                Thread.Sleep(500);
            }

        }

        private static void PreparaOrdini()
        {
            while (true)
            {
                Bevanda ordine;
                lock (blocco)
                {
                    while (codaOrdinazioni.Count == 0)
                    {
                        if (clientiFiniti == 3)
                        {
                            return;
                        }
                        Monitor.Wait(blocco);
                    }
                    ordine = codaOrdinazioni.Dequeue();
                }

                Console.WriteLine($"Il barista ha preparato: {ordine}");
                Thread.Sleep(800);
            }
        }
    }
}
