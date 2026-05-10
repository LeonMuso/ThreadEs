namespace EsThread_2._9
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var n1 = DammiUnNumero();
            var n2 = DammiUnNumero();
            var n3 = DammiUnNumero();

            Console.WriteLine("Attendiamo....\n");

            await Task.WhenAll(n1, n2, n3);
            Console.WriteLine($@"I numeri sono:
-numero1 {n1.Result}
-numero2 {n2.Result}
-numero3 {n3.Result}
");

            int somma = n1.Result + n2.Result + n3.Result;
            Console.WriteLine("La somma dei tre numeri e': "+ somma);
        }

        private static async Task<int> DammiUnNumero()
        {
            await Task.Delay(10000);
            int n = Random.Shared.Next(0, 50);
            return n;
        }
    }
}
