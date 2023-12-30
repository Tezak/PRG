using System.ComponentModel;
using System.Text;
using Tetris;

internal class Program
{
    public static List<Tetromino> tetrominos = TetrominoSet.tetrominos;

    private static void Main(string[] args)
    {
        int[,] playField = createPlayField();
        DrawIt drawIt = new DrawIt(playField.GetLength(0), playField.GetLength(1));
        drawIt.All(playField);
        int current = new Random().Next(0, 7);
        int next = new Random().Next(0, 7);
        int x = 4;
        int y = 0;
        int rotation = 0;
        while (true)
        {
            Thread.Sleep(1000);
            drawIt.Next(tetrominos[next]);

            y++;
            if (colisionCheck(x, y, tetrominos[current].shapeRotation[rotation], playField))
            {
                playField = AddTetromino(x, y, tetrominos[current].shapeRotation[rotation], playField);
                current = next;
                next = new Random().Next(0, 7);
                x = 4;
                y = 0;
                rotation = 0;
            }
            else
            {
                drawIt.Field(playField);
                drawIt.Tetromino(x, y, tetrominos[current].shapeRotation[rotation]);
            }

            //next = new Random().Next(0, 7);
            //drawIt.Next(tetrominos[next]);
        }
        Console.ReadKey();
    }

    private static int[,] AddTetromino(int x, int y, int[,] tetromino, int[,] playField)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                playField[y + i - 2, x + j] = tetromino[i, j];
            }
        }
        return playField;
    }

    private static bool colisionCheck(int x, int y, int[,] tetromino, int[,] playField)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (tetromino[i, j] == 1 && playField[x + i + 3, y + j] == 8) return true;
            }
        }
        return false;
    }

    private static int[,] createPlayField()
    {
        int [,] playField =  new int[16, 24];
        for (int i = 2; i < playField.GetLength(0) - 2; i++)
        {
            playField[i, playField.GetLength(1) - 3] = 8;
        }

        for (int i = 2; i < playField.GetLength(1) - 2; i++)
        {
            playField[2, i] = 8;
            playField[playField.GetLength(0) - 3, i] = 8;
        }

        return playField;
    }
}