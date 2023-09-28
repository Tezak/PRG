using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    internal class Class1
    {
        public static int choose(string[] options, int cursorPosition)
        {
            Console.SetCursorPosition(0, cursorPosition);
            foreach (string option in options) Console.WriteLine((Console.CursorTop - cursorPosition == 0 ? " > " : "   ") + option);
            int currentRow = 0;
            ConsoleKeyInfo keyInfo;
            Console.CursorVisible = false;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.DownArrow || (keyInfo.Key == ConsoleKey.UpArrow))
                {
                    rewriteTo(currentRow + cursorPosition, " ");
                    currentRow += (keyInfo.Key == ConsoleKey.DownArrow) ? 1 : -1;
                    currentRow = (currentRow + options.Length) % options.Length;
                    rewriteTo(currentRow + cursorPosition, ">");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter); // dokud není zmáčknut enter
            Console.Clear();
            Console.CursorVisible = true;
            return currentRow;
        }
        private static void rewriteTo(int currentRow, string replacement)
        {
            Console.SetCursorPosition(1, currentRow);
            Console.Write(replacement);
        }
    }
}
