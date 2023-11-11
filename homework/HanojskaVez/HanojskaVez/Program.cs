namespace HanojskaVez
{
    internal class Program
    {
        //mezerníkem vybírat držím nedržím
        //podmínky kdy platí stisknutí mezerníku
        //kde je kurzor tam je číslo
        //podmínky, kde může být číslo a posunutí uplně nahoru a uplně dolu


        static int cursorRow, cursorColumn = 0;
        static bool chosen = false;
        static void Main(string[] args)
        {
            game(8);
        }

        private static void game(int ringCount)
        {
            int[,] playField = new int[3, ringCount + 2];
            for (int i = 0;i < ringCount;i++) {
                playField[0, i] = i;
            }
            for (int i = 0; i < 3; i++)
            {
                playField[0, ringCount] = 1000;
            }
            for (int i = 1;i <= 20;i++)
            {
                draw(playField, ringCount);
                move(ringCount, playField);
            }
            


        }

        private static void move(int rengCount, int[,] playField)
        {

            ConsoleKeyInfo keyInfo;
            Console.CursorVisible = false;
            keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow: 
                    if(cursorRow < rengCount && !chosen) cursorRow++; break;
                case ConsoleKey.UpArrow: 
                    if (cursorRow > 0 && !chosen) cursorRow--; break;
                case ConsoleKey.LeftArrow: 
                    if (cursorColumn > 0) 
                    {
                        cursorColumn--;
                        if (chosen)
                        {
                            playField[cursorColumn, cursorRow] = playField[cursorColumn + 1, cursorRow];
                            playField[cursorColumn + 1, cursorRow] = 0;
                        }
                    }
                    break;
                case ConsoleKey.RightArrow: 
                    if (cursorColumn < 2)
                    {
                        cursorColumn++;
                        if (chosen)
                        {
                            playField[cursorColumn, cursorRow] = playField[cursorColumn - 1, cursorRow];
                            playField[cursorColumn - 1, cursorRow] = 0;
                        }
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (!chosen && (playField[cursorColumn, cursorRow] != 0 && playField[cursorColumn, cursorRow - 1] == 0))
                    {
                        playField[cursorColumn, 0] = playField[cursorColumn, cursorRow];
                        playField[cursorColumn, cursorRow] = 0;
                        Console.SetCursorPosition(cursorColumn, 0);
                        cursorRow = 0;
                        chosen = true;
                    } else {
                        int i = 1;
                        int underRing = playField[cursorColumn, i];
                        while (underRing == 0)
                        {  
                            underRing = playField[cursorColumn, i];
                        }
                        if (chosen && underRing > playField[cursorColumn, cursorRow])
                        {
                            chosen = false;
                        } 
                    }
                    break;
            }

        }

        private static void draw(int[,] playField, int ringCount)
        {
            Console.Clear();
            int length = 1 + 2 * ringCount;
            for (int i = 0; i < ringCount; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    Console.Write(playField[j, i]);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(cursorColumn, cursorRow);
            Console.WriteLine("+");
        }

        private static void rewriteTo(int row, int column, string replacement)
        {
            Console.SetCursorPosition(column, row);
            Console.Write(replacement);
        }
    }
}