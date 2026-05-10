namespace EsThread_1._21
{
    internal class Program
    {
        private static Queue<int> codaProduzioni = new Queue<int>();
        private static object blocco = new object();

        static void Main(string[] args)
        {
            var p1 = new Thread(() => Produzione());
            var c1 = new Thread(() => Consumazione());

            c1.Start();
            p1.Start();

            c1.Join();
            p1.Join();

            Console.WriteLine("\nE' stato prodotto uno zero");
        }

        private static void Consumazione()
        {
            while (true)
            {
                int prodotto;
                lock (blocco)
                {
                    while (codaProduzioni.Count == 0)
                    {
                        Monitor.Wait(blocco);
                    }
                    prodotto = codaProduzioni.Dequeue();
                }

                Console.WriteLine($"Il consumatore ha usato: {prodotto}");
                if (prodotto == 0)
                {
                    return;
                }
                Thread.Sleep(1000);
            }
        }

        private static void Produzione()
        {
            while (true)
            {
                int bOrdinata = Random.Shared.Next(0, 11);
                lock (blocco)
                {
                    codaProduzioni.Enqueue(bOrdinata);
                    Console.WriteLine($"Prodotto: {bOrdinata}");
                    Monitor.Pulse(blocco);
                    if (bOrdinata == 0)
                    {
                        return;
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
