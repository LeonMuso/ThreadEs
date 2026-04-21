namespace ThreadEs3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Thread(ProcessoBreve);
            var t2 = new Thread(ProcessoInfinito);
            t2.IsBackground = true;
            t1.Start(); t2.Start();
            t1.Join(); t2.Join();
        }

        static void ProcessoInfinito()
        {
            while (true)
            {
                Console.WriteLine(Random.Shared.Next(0, 11));
                Thread.Sleep(300);
            }
        }
        static void ProcessoBreve()
        {
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(300);
            }
        }
    }
}
