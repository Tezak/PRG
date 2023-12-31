using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Tetris01
{
    /// <summary>vykresluje na konzoli</summary>
    internal class Draw
    {

        int playFieldLeft = 0;
        public static int playFieldTop = 2;

        int playFieldWidth;
        int playFieldHeight;

        int textLeft;
        int textTop = playFieldTop + 2;

        int score = 0;
        int hiScore = getHiScore();

        static string scoreFile = "score.txt";

        /// <summary>Přečte si skóze ze souboru</summary>
        private static int getHiScore()
        {
            if (File.Exists(scoreFile))
            {
                StreamReader reader = new StreamReader(scoreFile);
                int score = Convert.ToInt32(reader.ReadLine());
                reader.Close();
                return score;
            }
            return 0;
        }

        /// <summary>Pokud bylo překonáno dosud největší skóre, přepíše soubor kde je uloženo</summary>
        private void rewriteHiScore()
        {
            if (score > hiScore)
            {
                StreamWriter writer = new StreamWriter(scoreFile);
                writer.WriteLine(score);
                writer.Close();
            }
        }

        /// <summary>Nastavuje proměné závislé na velikosti herního pole</summary>
        public Draw(int playFieldWidth, int playFieldHeight)
        {
            this.playFieldHeight = playFieldHeight;
            this.playFieldWidth = playFieldWidth;
            this.textLeft = playFieldLeft + playFieldWidth;
            Console.CursorVisible = false;
        }

        /// <summary>Vykreslí hru</summary>
        public void StartOfGame(bool[,] playField, Tetromino tetromino)
        {
            Console.ForegroundColor = ConsoleColor.White;
            score = 0;
            Console.Clear();
            Headings();
            Score(0, 1);
            HiScore();
            Level(1);
            Next(tetromino);
            FieldBig(playField);
        }

        /// <summary>Vykreslí nadcházející dílek</summary>
        public void Next(Tetromino nextTetromino)
        {
            cursorPosition(textLeft, textTop + 7);
            for (int i = 0; i < 2; i++)
            { 
                for (int j = 0; j < 4; j++)
                {
                    if (nextTetromino.shapeRotation[0][i, j]) Console.Write("█");
                    else Console.Write(" ");
                }
                cursorPosition(textLeft, textTop + 8);
            }
        }

        /// <summary>Vykreslí GAME OVER a přepíše skóre</summary>
        public void GameOver()
        {
            rewriteHiScore();
            cursorPosition(playFieldLeft + playFieldWidth / 2 - 4, playFieldTop + 10);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("GAMEOVER");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>Nastaví pozici kurzoru</summary>
        private void cursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        /// <summary>Animace při mizení řádku</summary>
        public void Line(int line)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for(int i = 0; i < 10; i++)
            {
                DucksAnimation(i / 4);
                cursorPosition(playFieldLeft + 3 + i, playFieldTop + line);
                Console.Write("█");
                Thread.Sleep(50);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal void DucksAnimation(int i)
        {
            Console.CursorVisible = false;
            List<string[]> frames = new List<string[]>();
            frames.Add(new string[] {
               "   -      ",
               " >(')     ",
               "   ( --,  ",
               "   \\ =~/  ",
               "    `-'   "});
            frames.Add(new string[] {
               "      -   ",
               "     (')< ",
               "  ,-- )   ",
               "  \\~= /   ",
               "   `-'    "});
            frames.Add(new string[] {
               "          ",
               "          ",
               "          ",
               "          ",
               "          "});
            for (int j = 0; j < 5; j++)
            {
                cursorPosition(textLeft, textTop + 13 + j);
                Console.WriteLine(frames[i][j]);
            }
        }

        /// <summary>Vykreslí nadpisy</summary>
        public void Headings()
        {
            cursorPosition(playFieldLeft + playFieldWidth / 2 - 3, 1);
            Console.WriteLine("TETRIS\n");
            cursorPosition(textLeft, textTop);
            Console.Write("HI-SCORE: ");
            cursorPosition(textLeft, textTop + 2);
            Console.Write("SCORE: ");
            cursorPosition(textLeft, textTop + 5);
            Console.Write("NEXT");
            cursorPosition(textLeft, textTop + 11);
            Console.Write("LEVEL: ");
        }

        public void Level(int level)
        {
            cursorPosition(textLeft + 7, textTop + 11);
            Console.Write(level);
        }

        /// <summary>Vypisuje skóre a updatuje ho</summary>
        public void Score(int addScore, int level)
        {
            cursorPosition(textLeft + 7, textTop + 2);
            score += addScore * (
                level == 1 ? 1 :
                level < 4 ?  2 :
                level < 6 ?  3 :
                level < 8 ?  4 : 5);
            Console.Write(score);
        }
        /// <summary>vypisuje nejvyšší skóre</summary>
        internal void HiScore()
        {
            hiScore = getHiScore();
            cursorPosition(textLeft + 10, textTop);
            Console.Write(hiScore);
        }

        /// <summary>Vykreslí herní pole</summary>
        public void FieldBig(bool[,] playField)
        {
            cursorPosition(playFieldLeft, playFieldTop);
            for (int i = 0; i < playField.GetLength(0); i++)
            {
                for (int j = 0; j < playField.GetLength(1); j++)
                {
                    if (playField[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.Write(" ");
                }
                Console.Write("\n");
                Console.CursorLeft = playFieldLeft;
            }
        }

        /// <summary>Vykreslí pouze vnitřek herního pole</summary>
        public void Field(bool[,] playField)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            cursorPosition(playFieldLeft + 3, playFieldTop);
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

        /// <summary>Vykreslí tetramino</summary>
        internal void Tetromino(int x, int y, bool[,] tetromino) => tetrominoDraw(x, y, tetromino, '█');
        

        /// <summary>Vymaže tetramino</summary>
        internal void DelTetromino(int x, int y, bool[,] tetromino) => tetrominoDraw(x, y, tetromino, ' ');
        

        private void tetrominoDraw(int x, int y, bool[,] tetromino, char ch)
        {
            Console.CursorVisible = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tetromino[i, j])
                    {
                        cursorPosition(playFieldLeft + 3 + x + j, playFieldTop + y + 1 + i);
                        Console.Write(ch);
                    }
                }
                Console.Write("\n");
                Console.CursorLeft = playFieldLeft;
            }
        }
    }
}
