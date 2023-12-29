using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tetris01
{
    internal class Draw
    {

        int playFieldLeft = 0;
        static int playFieldTop = 2;

        int playFieldWidth;
        int playFieldHeight;

        int textLeft;
        int textTop = playFieldTop + 2;

        int score = 0;
        int hiScore = getHiScore();

        static string file = "C:\\Users\\klark\\OneDrive\\Documents\\programování\\PRG\\homework\\Tetris\\Tetris01\\score.txt";

        private static int getHiScore()
        {
            if (File.Exists(file))
            {
                StreamReader reader = new StreamReader(file);
                int sc = Convert.ToInt32(reader.ReadLine());
                reader.Close();
                return sc;
            }
            return 20;
        }

        private void rewriteHiScore()
        {
            if (score > hiScore)
            {
                //StreamWriter writer = new StreamWriter(filepath, false);
                //writer.Close();
                StreamWriter writer = new StreamWriter(file);
                writer.WriteLine(score);
                writer.Close();
            }
        }

        public Draw(int playFieldWidth, int playFieldHeight)
        {
            this.playFieldHeight = playFieldHeight;
            this.playFieldWidth = playFieldWidth;
            this.textLeft = playFieldLeft + playFieldWidth;
            Console.CursorVisible = false;
        }

        public void All(bool[,] playField)
        {
            Headings();
            Score(score);
            HiScore();
            FieldBig(playField);
        }

        public void Next(Tetromino nextTetromino)
        {
            Console.SetCursorPosition(textLeft, textTop + 7);
            for (int i = 0; i < 2; i++)
            { 
                for (int j = 0; j < 4; j++)
                {
                    if (nextTetromino.shapeRotation[0][i, j]) Console.Write("█");
                    else Console.Write(" ");
                }
                Console.SetCursorPosition(textLeft, textTop + 8);
            }
        }

        public void GameOver()
        {
            Console.SetCursorPosition(playFieldLeft + playFieldWidth / 2 - 4, playFieldTop + 10);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("GAMEOVER");
            Console.ForegroundColor = ConsoleColor.White;
            rewriteHiScore();
        }

            public void Line(int line)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(playFieldLeft + 3, playFieldTop + line);
            for(int i = 0; i < 10; i++)
            {
                Console.Write("█");
                Thread.Sleep(30);
            }
            Console.ForegroundColor = ConsoleColor.White;
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

        public void Score(int addScore)
        {
            Console.SetCursorPosition(textLeft + 7, textTop + 2);
            score += addScore;
            Console.Write(score);
        }

        public void HiScore()
        {
            Console.SetCursorPosition(textLeft + 10, textTop);
            Console.Write(hiScore);
        }

        public void FieldBig(bool[,] playField)
        {
            Console.SetCursorPosition(playFieldLeft, playFieldTop);
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int j = 0; j < playField.GetLength(1); j++)
                {
                    if (playField[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.Write(" ");
                }
                Console.Write("\n");
                Console.CursorLeft = playFieldLeft;
            }
        }

        public void Field(bool[,] playField)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(playFieldLeft + 3, playFieldTop);
            for (int i = 0; i < playField.GetLength(0) - 3; i++)
            {
                for (int j = 3; j < playField.GetLength(1) - 3; j++)
                {
                    if (playField[i, j]) Console.Write("█");
                    else Console.Write(" ");
                }
                Console.Write("\n");
                Console.CursorLeft = playFieldLeft + 3;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal void Tetromino(int x, int y, bool[,] tetromino)
        {
            TetrominoDraw(x, y, tetromino, '█');
        }
        internal void DelTetromino(int x, int y, bool[,] tetromino)
        {
            TetrominoDraw(x, y, tetromino, ' ');
        }

        internal void TetrominoDraw(int x, int y, bool[,] tetromino, char ch)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tetromino[i, j])
                    {
                        Console.SetCursorPosition(playFieldLeft + 3 + x + j, playFieldTop + y + 1 + i);
                        Console.Write(ch);
                    }
                }
                Console.Write("\n");
                Console.CursorLeft = playFieldLeft;
            }
        }
    }
}
