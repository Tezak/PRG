using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris01
{
    internal class Game
    {
        public bool gameOver = false;
        // list of tetraminos and their rotations
        List<Tetromino> tetrominos = TetrominoSet.tetrominos;
        static bool[,] playField = createPlayField();
        Draw draw = new Draw(playField.GetLength(1), playField.GetLength(0));
        int currentTetromino = getRandomTetromino();
        int nextTetromino = getRandomTetromino();
        int x = 4;
        int y = 0;
        int rotation = 0;
        DateTime startTime = DateTime.Now;

        public void drawGame()
        {
            Console.CursorVisible = false;
            draw.All(playField);
        }

        private void updateTetrominoFall()
        {
            draw.Tetromino(x, y, tetrominos[currentTetromino].shapeRotation[rotation]);
        }

        public void update()
        {
            updateTetrominoFall();
            while (time() < 1000)
            {
                //check if key is pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.DownArrow) 
                        break;
                    if (keyInfo.Key == ConsoleKey.UpArrow) 
                        tryToRotate();
                    if (keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow) 
                        tryToMoveX(keyInfo);
                }
                Thread.Sleep(10);
            }
            startTime = DateTime.Now;
            tryToMoveY();
        }

        private void tryToRotate()
        {
            int rotSave = rotation;
            rotation = (rotation + 1) % tetrominos[currentTetromino].shapeRotation.Count();
            if (colisionCheck()) rotation = rotSave;
            else
            {
                draw.DelTetromino(x, y, tetrominos[currentTetromino].shapeRotation[rotSave]);
                draw.Tetromino(x, y, tetrominos[currentTetromino].shapeRotation[rotation]);
            }
        }

        private void tryToMoveX(ConsoleKeyInfo keyInfo)
        {
            int xSave = x;
            x = keyInfo.Key == ConsoleKey.RightArrow ? x + 1 : x - 1;
            if (colisionCheck()) x = xSave;
            else
            {
                draw.DelTetromino(xSave, y, tetrominos[currentTetromino].shapeRotation[rotation]);
                draw.Tetromino(x, y, tetrominos[currentTetromino].shapeRotation[rotation]);
            }
        }

        private void tryToMoveY()
        {
            y++;
            if (colisionCheck())
            {
                AddTetromino();
                if (!gameOver)
                {
                    currentTetromino = nextTetromino;
                    nextTetromino = getRandomTetromino();
                    draw.Next(tetrominos[nextTetromino]);
                    x = 4;
                    y = 0;
                    rotation = 0;
                    draw.Score(10);
                    draw.Field(playField);
                }
            }
            else draw.DelTetromino(x, y - 1, tetrominos[currentTetromino].shapeRotation[rotation]);
        }

        private int time()
        {
            int time = (int)(DateTime.Now - startTime).TotalMilliseconds;
            return time;
        }

        private void AddTetromino()
        {
            int y = this.y - 1;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tetrominos[currentTetromino].shapeRotation[rotation][i, j]) 
                        playField[y + i + 1, x + j + 3] = true;
                }
            }
            if (y < 2)
            {
                gameOver = true;
                draw.GameOver();
            }
            lookForWholeRow();
        }

        private void lookForWholeRow()
        {
            for (int i = playField.GetLength(0) - 4; i >=0 ; i--)
            {
                for (int j = 3; j < playField.GetLength(1) - 3; j++)
                {
                    if (playField[i, j] && j == playField.GetLength(1) - 4)
                    {
                        draw.Score(100);
                        draw.Line(i);
                        for (int k = i; k > 0; k--)
                        {
                            for (int l = 3; l < playField.GetLength(1) - 3; l++)
                            {
                                playField[k, l] = playField[k - 1, l];
                            }
                        }
                        draw.Field(playField);
                        i++;
                    }
                    if (!playField[i, j]) break;
                }
            }
        }

        private bool colisionCheck()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tetrominos[currentTetromino].shapeRotation[rotation][i, j] && playField[y + i + 1, x + j + 3]) return true;
                }
            }
            return false;
        }

        private static bool[,] createPlayField()
        {
            bool[,] playField = new bool[24, 16];
            for (int i = 2; i < playField.GetLength(0) - 2; i++)
            {
                playField[i, 2] = true;
                playField[i, playField.GetLength(1) - 3] = true;
            }

            for (int i = 2; i < playField.GetLength(1) - 2; i++)
            {
                playField[playField.GetLength(0) - 3, i] = true;
            }

            return playField;
        }

        private static int getRandomTetromino() => new Random().Next(0, 7);
    }
}
