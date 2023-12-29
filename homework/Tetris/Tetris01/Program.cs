using System.IO;
using Tetris01;
using System.Windows.Input;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;


internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        game.drawGame();
        while (!game.gameOver)
        {
            game.update(); 
        }
        Console.ReadKey();
    }
}