namespace ThreadEs
{
    internal class Program
    {
        static int contatore = 0;
        static object lockContatore = new object();
        static void Main(string[] args)
        {
            var t1 = new Thread(IncrementaValore);
            var t2 = new Thread(IncrementaValore);
            t1.Start(); t2.Start();
            t1.Join(); t2.Join();
            Console.WriteLine(contatore);
        }

        static void IncrementaValore()
        {
            for (int i = 0; i < 1000000; i++)
            {
                lock (lockContatore)
                {
                    contatore++;
                }
            }
        }
    }
}
