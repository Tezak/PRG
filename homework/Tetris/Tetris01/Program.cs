using System.IO;
using Tetris01;
using System.Windows.Input;
using System.Xml.Linq;


internal class Program
{
    public static List<Tetromino> tetrominos = TetrominoSet.tetrominos;
    public static DateTime startTime = DateTime.Now;

    private static void Main(string[] args)
    {
        int score = 0;
        int[,] playField = createPlayField();
        Draw draw = new Draw(playField.GetLength(1), playField.GetLength(0));
        draw.All(playField);
        int current = new Random().Next(0, 7);
        int next = new Random().Next(0, 7);
        int x = 4;
        int y = 0;
        int rotation = 0;
        //ConsoleKeyInfo keyInfo;
        Console.CursorVisible = false;
        while (true)
        {
            draw.Next(tetrominos[next]);
            draw.Tetromino(x, y, tetrominos[current].shapeRotation[rotation]);
            while (time() < 1000)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        break;
                    }
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        int rotSave = rotation;
                        rotation = (rotation + 1) % tetrominos[current].shapeRotation.Count();
                        if (colisionCheck(x, y, tetrominos[current].shapeRotation[rotation], playField)) rotation = rotSave;
                        else
                        {
                            draw.DelTetromino(x, y, tetrominos[current].shapeRotation[rotSave]);
                            draw.Tetromino(x, y, tetrominos[current].shapeRotation[rotation]);
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        int xSave = x;
                        x = keyInfo.Key == ConsoleKey.RightArrow ? x + 1 : x - 1;
                        if (colisionCheck(x, y, tetrominos[current].shapeRotation[rotation], playField)) x = xSave;
                        else
                        {
                            draw.DelTetromino(xSave, y, tetrominos[current].shapeRotation[rotation]);
                            draw.Tetromino(x, y, tetrominos[current].shapeRotation[rotation]);
                        }
                    }
                }
                Thread.Sleep(10);
            }
            startTime = DateTime.Now;

            y++;

            if (colisionCheck(x, y, tetrominos[current].shapeRotation[rotation], playField))
            {
                playField = AddTetromino(x, y - 1, tetrominos[current].shapeRotation[rotation], playField, draw);
                current = next;
                next = new Random().Next(0, 7);
                x = 4;
                y = 0;
                rotation = 0;
                draw.Score(10);
                draw.Field(playField);

            }
            else draw.DelTetromino(x, y - 1, tetrominos[current].shapeRotation[rotation]);

        }
    }
    
    private static int time()
    {
        int time = (int)(DateTime.Now - startTime).TotalMilliseconds;
        return time;
    }

    private static int[,] AddTetromino(int x, int y, bool[,] tetromino, int[,] playField, Draw draw)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tetromino[i, j]) playField[y + i + 1, x + j + 3] = 8;
            }
        }

        if (y < 2) System.Environment.Exit(0);

        for (int i = 0; i < playField.GetLength(0) - 3; i++)
        {
            for (int j = 3; j < playField.GetLength(1) - 3; j++)
            {
                if (playField[i, j] == 0) break;
                if (playField[i, j] == 8 && j == playField.GetLength(1) - 4)
                {
                    draw.Score(100);

                    for (int k = i; k > 0; k--)
                    {
                        for (int l = 3; l < playField.GetLength(1) - 3; l++)
                        {
                            playField[k, l] = playField[k - 1, l];
                        }
                    }
                }
                
            }

        }

        return playField;
    }

    private static bool colisionCheck(int x, int y, bool[,] tetromino, int[,] playField)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tetromino[i, j] && playField[y + i + 1, x + j + 3] == 8) return true;
            }
        }
        return false;
    }

    private static int[,] createPlayField()
    {
        int[,] playField = new int[24, 16];
        for (int i = 2; i < playField.GetLength(0) - 2; i++)
        {
            playField[i, 2] = 8;
            playField[i, playField.GetLength(1) - 3] = 8;
        }

        for (int i = 2; i < playField.GetLength(1) - 2; i++)
        {
            playField[playField.GetLength(0) - 3, i] = 8;
        }

        return playField;
    }
}