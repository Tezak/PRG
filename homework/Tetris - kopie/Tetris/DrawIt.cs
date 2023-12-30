using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Tetris
{
    internal class DrawIt
    {
        int playFieldLeft = 0;
        static int playFieldTop = 2;

        int playFieldWidth;
        int playFieldHeight;

        int textLeft = 0;
        int textTop = playFieldTop + 2;

        public DrawIt(int playFieldWidth, int playFieldHeight) 
        {
            this.playFieldHeight = playFieldHeight;
            this.playFieldWidth = playFieldWidth;
            this.textLeft = playFieldLeft + playFieldWidth;
            Console.CursorVisible = false;
        }

        public void All(int[,] playField)
        {
            Headings();
            Score();
            FieldBig(playField);
        }

        public void Next(Tetromino nextTetromino)
        {
            Console.SetCursorPosition(textLeft, textTop + 7);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (nextTetromino.shapeRotation[0][i, j] == 1) Console.Write("█");
                    else Console.Write(" ");
                }
                Console.SetCursorPosition(textLeft, textTop + 8);
            }
        }

        public void Headings()
        {
            Console.SetCursorPosition(playFieldLeft + playFieldWidth / 2 - 3, 1);
            Console.WriteLine("TETRIS\n");
            Console.SetCursorPosition(textLeft, textTop);
            Console.Write("HI-SCORE: ");
            Console.SetCursorPosition(textLeft, textTop + 2);
            Console.Write("SCORE: ");
            Console.SetCursorPosition(textLeft, textTop + 5);
            Console.Write("NEXT");
        }

        public void Score()
        {
            Console.SetCursorPosition(textLeft + 10, textTop);
            Console.Write("0");
            Console.SetCursorPosition(textLeft + 7, textTop + 2);
            Console.Write("0");
        }

        public void FieldBig(int[,] playField)
        {
            playField[4, 9] = 8;
            Console.SetCursorPosition(playFieldLeft, playFieldTop);
            for (int j = 0; j < playField.GetLength(1); j++)
            {
                for (int i = 0; i < playField.GetLength(0); i++)
                {
                    if (playField[i, j] == 8)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (playField[i, j] == 0) Console.Write(" ");
                }
                Console.Write("\n");
                Console.CursorLeft = playFieldLeft;
            }
        }

        public void Field(int[,] playField)
        {

            Console.SetCursorPosition(playFieldLeft + 3, playFieldTop);
            for (int j = 0; j < playField.GetLength(1) - 3; j++)
            {
                for (int i = 3; i < playField.GetLength(0) - 3; i++)
                {
                    if (playField[i, j] == 8)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (playField[i, j] == 0) Console.Write(" ");
                }
                Console.Write("\n");
                Console.CursorLeft = playFieldLeft + 3;
            }
        }

        internal void Tetromino(int x, int y, int[,] tetromino)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (tetromino[i, j] == 1)
                    {
                        Console.SetCursorPosition(playFieldLeft + 3 + x + i, playFieldTop + y + 1 + j);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.Write("\n");
                Console.CursorLeft = playFieldLeft;
            }
        }
    }
}
