internal class Program
{
    private static void Main(string[] args)
    {
        Random rnd = new Random();
        int rool = 0;
        int upperBand = 1000;
        bool computer = true;

        do
        {
            rool = rnd.Next(1, upperBand + 1);
            Console.WriteLine((computer? "Computer rools ": "Human rools ") + rool + "(0-" + upperBand + ")");
            upperBand = rool;
            computer = !computer;
            if(!computer) Console.ReadKey();
        } while (rool != 1);

        Console.WriteLine("\n" + (computer ? "I am the best! " : "You were just lucky. "));
        
        Console.ReadKey();
    }
}