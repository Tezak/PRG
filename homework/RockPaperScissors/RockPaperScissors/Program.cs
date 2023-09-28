using RockPaperScissors;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] YesNo = { "Ano", "Ne" };
        string[] KNP = { "Kámen", "Nůžky", "Papír" };

        Console.WriteLine("Ahoj, chceš si zahrát kámen nůžky papír?\n");

        while (true)
        {
            if (1 == Class1.choose(YesNo, Console.CursorTop)) break;
            Console.Clear();

            Console.WriteLine("Vyber si\n");
            int humanChoice = Class1.choose(KNP, Console.CursorTop);
            Console.WriteLine("Tvoje volba: " + KNP[humanChoice]);
            int computerChoice = new Random().Next(0, 3);
            Console.WriteLine("Moje volba: " + KNP[computerChoice]);

            if (humanChoice == computerChoice) Console.WriteLine("Remíza");
            else if ((humanChoice + 4) % 3 == computerChoice) Console.WriteLine("Vyhrál jsi");
            else Console.WriteLine("Vyhrál jsem :D");

            Console.WriteLine("\nChceš hrát znovu?");
        }
    }
}