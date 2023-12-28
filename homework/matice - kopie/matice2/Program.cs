using System;
using System.Drawing;
using System.Runtime.CompilerServices;

internal class Program
{
    private static int[,]? matrix;
    private static string[] operations = { 
        /*0*/ "Vypiš n-tý řádek pole", 
        /*1*/ "Vypiš n-tý sloupec pole", 
        /*2*/ "Prohoď 2 prvky", 
        /*3*/ "Prohoď n-tý řádek v poli s m-tým řádkem", 
        /*4*/ "Prohoď n-tý sloupec v poli s m-tým sloupcem",
        /*5*/ "Vynásobení matice",
        /*6*/ "Vynásobení řádku",
        /*7*/ "Vynásobení sloupce",
        /*8*/ "Transpozice matice",
        /*9*/ "Vypiš prvky na hlavní diagonále",
        /*10*/ "Otoč pořadí prvků na hlavní diagonále",
        /*11*/ "Sečti matice",
        /*12*/ "Odečti matice",
        /*13*/ "Vynásob matice",
        /*14*/ "Vyznač barevně čísla",
                //pole a*a
        /*15*/ "Vypiš prvky na vedlejší diagonále",
        /*16*/ "Otoč pořadí prvků na vedlejší diagonále"
    };
    private static int p;

    private static void Main(string[] args)
    {
        //DucksAnimation();
        FillMatrix();
        WriteMatrix();

        do
        {
            switch (Choose(operations)) //spouští vybranou operaci
            {
                //case 0: writeNRow(); break;
                case 0: row(write); break;
                case 1: column(write); break;
                case 2: swapTwoNumbers(); break;
                case 3: row(swap); break;
                case 4: column(swap); break;
                case 5: multiplyMatrice(); break;
                case 6: row(multiply); break;
                case 7: column(multiply); break;
                case 8: Transposition(); break;
                case 9: writeMainDiagonal(); break;
                case 10: reverseMainDiagonal(); break;
                case 11: addTwoMAtrix(); break;
                case 12: substractTwoMAtrix(); break;
                case 13: multiplyTwoMAtrix(); break;
                case 14: hilightNumber(); break;
                // pole a*a
                case 15: writeSecondaryDiagonal(); break;
                case 16: reverseSecondaryDiagonal(); break;
            }
        } while (askIfAgain()); //dokud chce uživatel pokračovat v počítání
    }

    private static Action<int> swap(bool row)
    {
        if (row)
        {
            Console.Write("Prohození řádku: ");
            int nRowSwap = NumberCheck(0);
            Console.Write("s řádkem: ");
            int mRowSwap = NumberCheck(0);
            return (x) =>
            {
                int save = matrix[nRowSwap, x];
                matrix[nRowSwap, x] = matrix[mRowSwap, x];
                matrix[mRowSwap, x] = save;
            };
        }

        else
        { 
            Console.Write("Prohození sluopce: ");
            int nColSwap = NumberCheck(1);
            Console.Write("se sloupcem: ");
            int mColSwap = NumberCheck(1);
            return (r) =>
            {
                int save = matrix[r, nColSwap];
                matrix[r, nColSwap] = matrix[r, mColSwap];
                matrix[r, mColSwap] = save;
            };
        }        
    }

    private static Action<int> write(bool row)
    {
        Console.Write("Vypsání " + (row? "řádku" : "sluopce") + ": ");
        int nRow = 0;
        int nColumn = 0;
        if (row) nRow = NumberCheck(0);
        else nColumn = NumberCheck(1);

        return row 
            ? (c) => Console.Write(matrix[nRow, c] + " ")
            : (r) => Console.Write(matrix[r, nColumn] + " ");
    }

    private static Action<int> multiply(bool row)
    {
        Console.Write("Vynásobit " + (row ? "řádek" : "sloupec") + ": ");
        int nRow = 0;
        int nColumn = 0;
        if (row) nRow = NumberCheck(0);
        else nColumn = NumberCheck(1);

        Console.Write("číslem: ");
        int number = NumberCheck();

        return row
            ? (c) => matrix[nRow, c] *= number
            : (r) => matrix[r, nColumn] *= number;
    }


    private static void row(Func<bool, Action<int>> actionfn)
    {
        Action<int> rowAction = actionfn(true); 
        for (int c = 0; c < matrix.GetLength(1); c++)
        {
            rowAction(c);
        }
        Console.WriteLine("\n");
        WriteMatrix();
    }

    private static void column(Func<bool, Action<int>> actionfn)
    {
        Action<int> columnAction = actionfn(false);
        for (int r = 0; r < matrix.GetLength(0); r++)
        {
            columnAction(r);
        }
        Console.WriteLine("\n");
        WriteMatrix();
    }

    private static void hilightNumber()
    {
        Dictionary<int, int> quantity = new Dictionary<int, int>();

        //největší četnost
        Console.Write("Číslo s nejvyšší četností ");

        //int max = matrix.Cast<int>().Max();
        //int min = matrix.Cast<int>().Min();

        //int[] sum = new int[max - min + 1];

        for (int r = 0; r < matrix.GetLength(0); r++)
        {
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                int key = matrix[r, c];
                if (quantity.ContainsKey(key))
                    quantity[key] += 1;
                else
                    quantity.Add(key, 1);

                //int index = key - min;
                //sum[index] += 1;
            }
        }
        var pairs = quantity.OrderByDescending(pair => pair.Value).ToArray();

        //Console.WriteLine(Array.IndexOf(sum, sum.Max()) + min);

        Console.Write("Číslo: ");
        int number = NumberCheck();

        WriteMatrix(number);
    }

    private static void DucksAnimation()
    {

        Console.CursorVisible = false;
        string[] ducks = {
               "      _          _          _          _          _          ",
               "    >(')____,  >(')____,  >(')____,  >(')____,  >(') ___,    ",
               "      (` =~~/    (` =~~/    (` =~~/    (` =~~/    (` =~~/    ",
               "       `---'      `---'      `---'      `---'      `---'     ",
               "~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~^~"};
        for (int i = 0; i < ducks[4].Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int j = 0; j < 4; j++)
            {
                Console.WriteLine(ducks[j]);
                ducks[j] = ducks[j].Remove(0, 1);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorTop = Console.CursorTop - 1;
            for (int j = 1; j < ducks[4].Length - i; j++)
            {
                Console.CursorLeft = j;
                if (ducks[3][j - 1] == ' ') Console.Write(ducks[4][j]);
            }
            Thread.Sleep(100);
            Console.SetCursorPosition(0, 0);
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        Console.CursorVisible = true;
        DuckPackage package = GetQuack();
    }

    class DuckPackage {
        public int NumberOfQuack;
        public string Greeting;
    }

    private static DuckPackage GetQuack()
    {
        return new DuckPackage { NumberOfQuack = 3, Greeting = "QUACK" };
    }

    private static void multiplyTwoMAtrix() => twoMatrixOperations(2);
    
    private static void substractTwoMAtrix() => twoMatrixOperations(1);

    private static void addTwoMAtrix() => twoMatrixOperations(0);

    /// <summary>Provádí operace s dvěmi maticemi 0 - přičítá matici, 1 - odečítá matici, 2 - násobí maticí</summary>
    /// <returns>
    /// Vypíše výsledek maticové početní operace a nastaví naši matici na výsledek této operace
    /// </returns>
    private static void twoMatrixOperations(int operation)
    {
        Console.WriteLine("druhá matice: ");
        int[,] matrix2;
        bool random = Convert.ToBoolean(Choose(new string[] {"čísla od 1 do a*b", "náhodná čísla" }));

        // vytvoření matice pro násobení matic
        if (operation == 2) {
            Console.Write("počet řádků matice 2: ");
            int columnNumber = NumberCheck(2);
            matrix2 = GenerateValues(random, matrix.GetLength(1), columnNumber);
        }

        // vytvoření stejně velké matice
        else matrix2 = GenerateValues(random, matrix.GetLength(0), matrix.GetLength(1));

        WriteMatrix(matrix2);
        Console.WriteLine((operation == 0) ? "součet matic:": (operation == 1) ? "rozdíl matic:" : "součin matic");

        // násobení matic
        if (operation == 2)
        {
            int[,] matrix3 = new int[matrix.GetLength(0), matrix2.GetLength(1)];
            for (int r = 0; r < matrix3.GetLength(0); r++)
            {
                for (int c = 0; c < matrix3.GetLength(1); c++)
                {
                    matrix3[r, c] = 0;
                    for (int k = 0; k < matrix.GetLength(1); k++)
                    {
                        matrix3[r, c] += matrix[r, k] * matrix2[k, c];
                    }
                }
            }
            matrix = matrix3;
            WriteMatrix();
            return;
        }

        // odčítání nebo sčítání matic

        else 
        {
            int multiplier = (operation == 0) ? +1 : -1;

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    matrix[r, c] += multiplier * matrix2[r, c];
                }
            }
        }
        WriteMatrix();
    }

    /// <summary>Dává matici rozměry a naplní ji hodnotami</summary>
    /// <returns>
    /// Od uživatele přijme rozměry a, b. Vytvoří pole a*b. Dá uživateli na výběr, zda chce vyplnit pole čísly 1 - a*b nebo náhodnými čísly.
    /// </returns>
    private static void FillMatrix()
    {
        //přečte hodnoty rozměrů pole z konzole
        Console.WriteLine("Vytvoření matice a*b");
        Console.Write("a: ");
        int a = NumberCheck(2);
        Console.Write("b: ");
        int b = NumberCheck(2);

        Console.WriteLine();

        //plní pole hodnotami
        matrix = new int[a, b];
        string[] valuesType = { "čísla od 1 do a*b", "náhodná čísla" };
        bool random = Convert.ToBoolean(Choose(valuesType));
        matrix = GenerateValues(random, matrix.GetLength(0), matrix.GetLength(1));
    }

    /// <summary>Plní matici hodnotami</summary>
    /// <returns>
    /// Pokud je random true, jsou čísla náhodná, pokud ne, jsou od 1 do a*b.
    /// </returns>
    private static int[,] GenerateValues(bool random, int rowsNumber, int columnsNumber)
    {
        int max = 200;
        int[,] matrix = new int[rowsNumber, columnsNumber];
        
        Func<int> returnNumber = random? GetRandomFn(max): GetSequenceFn();

        for (int r = 0; r < rowsNumber; r++)
        {
            for (int c = 0; c < columnsNumber; c++)
            {
                matrix[r, c] = returnNumber();
            }
        }
        return matrix;
    }

    private static Func<int> GetRandomFn(int max)
    {
        Random rnd = new Random();
        return () => rnd.Next(0, max);
    }

    private static Func<int> GetSequenceFn()
    {
        int number = 1;
        return () => number++;
    }

    /// <summary>Ptá se, zda chce užitvatel pokračovat v počítání</summary>
    /// <returns>
    /// Pokud ano, vrací true, pokud ne, vrací false. 
    /// </returns>
    private static bool askIfAgain()
    {
        string[] options = {
            "Chci počítat dál",
            "Ukončit program"};
        return Choose(options) == 0;
    }

    /// <summary>Transpozice matice</summary>
    /// <returns>
    /// Převrátí matici kolem hlavní diagonály a vypíše matici do konzole.
    /// </returns>
    private static void Transposition()
    {
        Console.WriteLine("Transpozice matice");
        int[,] transposedMatrix = new int[matrix.GetLength(1), matrix.GetLength(0)];

        for (int r = 0; r < matrix.GetLength(0); r++)
        {
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                transposedMatrix[c, r] = matrix[r, c];
            }
        }
        matrix = transposedMatrix;
        WriteMatrix();
    }

    

    /// <summary>Vynásobení matice</summary>
    /// <returns>
    /// Vynásobí matici a vypíše matici do konzole.
    /// </returns>
    private static void multiplyMatrice()
    {
        Console.Write("Násobit číslem: ");
        int number = NumberCheck();

        for (int r = 0; r < matrix.GetLength(0); r++)
        {
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                matrix[r, c] = matrix[r, c] * number;
            }
        }
        WriteMatrix();
    }

    /// <summary>Obrátí vedlejší diagonálu</summary>
    /// <returns>
    /// Obrátí pořadí čísel na vedlejší diagonále a vypíše matici do kontole
    /// </returns>
    private static void reverseSecondaryDiagonal()
    {
        Console.WriteLine("Otočení pořadí prvků vedlejší diagonály");
        for (int r = 0; 2 * r < matrix.GetLength(0); r++)
        {
            int c = matrix.GetLength(0) - r - 1;
            int save = matrix[c, r];
            matrix[c, r] = matrix[r, c];
            matrix[r, c] = save;
        }
        WriteMatrix();
    }


    /// <summary>Obrátí hlavní diagonálu</summary>
    /// <returns>
    /// Obrátí pořadí čísel na hlavní diagonále a vypíše matici do kontole
    /// </returns>
    private static void reverseMainDiagonal()
    {
        int limit = matrix.GetLength(0) < matrix.GetLength(1) ? matrix.GetLength(0) : matrix.GetLength(1);
        Console.WriteLine("Otočení pořadí prvků hlavní diagonály");
        for (int i = 0; 2 * i < limit; i++)
        {
            int j = matrix.GetLength(0) - i - 1;
            int save = matrix[i, i];
            matrix[i, i] = matrix[j, j];
            matrix[j, j] = save;
        }
        WriteMatrix();
    }


    /// <summary>Vypíše vedlejší diagonálu</summary>
    /// <returns>
    /// Vypíše vedlejší diagonálu na konzoli
    /// </returns>
    private static void writeSecondaryDiagonal()
    {
        Console.WriteLine("Vypsání vedlejší diagonály");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write(matrix[i, matrix.GetLength(0) - 1 - i] + " ");
        }
        Console.WriteLine("\n");
    }

    /// <summary>Vypíše hlavní diagonálu</summary>
    /// <returns>
    /// Vypíše hlavní diagonálu na konzoli
    /// </returns>
    private static void writeMainDiagonal()
    {
        Console.WriteLine("Vypsání hlavní diagonály");
        int limit = matrix.GetLength(0) < matrix.GetLength(1) ? matrix.GetLength(0) : matrix.GetLength(1);
        for (int i = 0; i < limit; i++)
        {
            Console.Write(matrix[i, i] + " ");
        }
        Console.WriteLine("\n");
    }

    
    /// <summary>Prohodí dvě čísla</summary>
    /// <returns>
    /// Od uživatele přijme souřadnice čísel, které má prohodit, tato čísla prohodí a pak pole vypíše na konzoli.
    /// </returns>
    private static void swapTwoNumbers()
    {
        Console.Write("Prohození čísla na řádku: ");
        int rowFirst = NumberCheck(0);
        Console.Write("ve sloupci: ");
        int coulmnFirst = NumberCheck(1);
        Console.Write("s číslem číslem na řádku: ");
        int rowSecond = NumberCheck(0);
        Console.Write("ve sloupci: ");
        int columnSecond = NumberCheck(1);

        int save = matrix[rowFirst, coulmnFirst];
        matrix[rowFirst, coulmnFirst] = matrix[rowSecond, columnSecond];
        matrix[rowSecond, columnSecond] = save;

        WriteMatrix();
    }

    /// <summary>Zadá se pole, jehož prvky jsou následně vypsány do konzole a uživatel si pomocí šipky zvolí co chce a potvrdí entrem</summary>
    /// <returns>
    /// Vrací index vybrané možnosti z pole které bylo vloženo 
    /// </returns>
    private static int Choose(string[] options)
    {
        int cursorPosition = Console.CursorTop;
        int optionsCount = options.Length - (matrix.GetLength(0) == matrix.GetLength(1) || options.Length == 2 ? 0 : 2);
        Console.SetCursorPosition(0, cursorPosition);
        for (int i = 0; i < optionsCount; i++)
        {
            Console.WriteLine((i == 0 ? " > " : "   ") + options[i]);
        }
        cursorPosition = Console.CursorTop - optionsCount;
        //foreach (string option in options) Console.WriteLine((Console.CursorTop - cursorPosition == 0 ? " > " : "   ") + option);
        int currentRow = 0;
        ConsoleKeyInfo keyInfo;
        Console.CursorVisible = false;
        do
        {
            //currentRow = Console.CursorTop;
            keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.DownArrow || (keyInfo.Key == ConsoleKey.UpArrow))
            {
                rewriteTo(currentRow + cursorPosition, " ");
                currentRow += (keyInfo.Key == ConsoleKey.DownArrow) ? 1 : -1;
                currentRow = (currentRow + optionsCount) % optionsCount;
                rewriteTo(currentRow + cursorPosition, ">");
            }
        }
        while (keyInfo.Key != ConsoleKey.Enter); // dokud není zmáčknut enter

        Console.SetCursorPosition(0, cursorPosition + optionsCount);
        for (int i = 0; i < optionsCount; i++)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
        }
        Console.CursorVisible = true;
        Console.SetCursorPosition(0, cursorPosition);
        return currentRow;
    }

    /// <summary>Přepíše znak ve druhém sloupci na zadaném řádku zadaným znakem</summary>
    private static void rewriteTo(int currentRow, string replacement)
    {
        Console.SetCursorPosition(1, currentRow);
        Console.Write(replacement);
    }

    /// <summary>Vypíše naši hlavní matici do konzole</summary>
    private static void WriteMatrix() {
        WriteMatrix(matrix, 0, false); 
    }

    private static void WriteMatrix(int[,] matrix)
    {
        WriteMatrix(matrix, 0, false);
    }

    private static void WriteMatrix(int hNumber)
    { 
        WriteMatrix(matrix, hNumber, true);
    }

    /// <summary>Vypíše zadanou matici do konzole</summary>
    private static void WriteMatrix(int[,] matrix, int hNumber, bool hilight)
    {
        int digitsMax = 0; //maximální počet cifer

        for (int r = 0; r < matrix.GetLength(0); r++)
        {
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                int digits = Convert.ToString(matrix[r, c]).Length;
                if (digits > digitsMax) digitsMax = digits;
            }
        }

        int left = 0;
        for (int r = 0; r < matrix.GetLength(0); r++)
        {
            left = 0;
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                Console.CursorLeft = left; //určuje poici kurzoru, aby to bylo hezky zarovnané
                left += (digitsMax + 1);
                if (left > (Console.WindowWidth - digitsMax)) 
                {
                    left = 0;
                    Console.WriteLine();
                }
                if (hilight && matrix[r, c] == hNumber) Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(matrix[r, c]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    /// <summary>Kontroluje zda číslo zadané uživatelem je číslo</summary>
    /// <returns>
    /// Vrací vstup od užovatele
    /// </returns>
    private static int NumberCheck() { return NumberCheck(3); }

    /// <summary>Kontroluje zda číslo zadané uživatelem je číslo a splňuje podmínky: 0 - existující řádek, 1 - existující sloupec, 2 - kladné číslo</summary>
    /// <returns>
    /// Vrací vstup od užovatele
    /// </returns>
    private static int NumberCheck(int columnRow) //0-row, 1-column, 2-positive number, 3-just number
    {
        int number;
        int cursorColumn = Console.CursorLeft;
        int cursorRow = Console.CursorTop;
        string input = Console.ReadLine();
        bool isNumber = int.TryParse(input, out number);
        bool isPositive = (columnRow < 3) && !(number > 0);
        bool column = (columnRow == 1) && !(number <= matrix.GetLength(1));
        bool row = (columnRow == 0) && !(number <= matrix.GetLength(0));

        if (isNumber && !isPositive && !column && !row) return columnRow < 2? number - 1: number;

        deleteWhatWasWritten(cursorColumn, cursorRow);
        return NumberCheck(columnRow);
    }

    /// <summary>vymaže vče co je napasné za zadanou pozicí kurzoru</summary>
    private static void deleteWhatWasWritten(int cursorColumn, int cursorRow)
    {
        int curentRow = Console.CursorTop;
        Console.SetCursorPosition(cursorColumn, cursorRow);
        Console.Write(new string(' ', Console.WindowWidth - cursorColumn + Console.WindowWidth * (curentRow - cursorRow)));
        Console.SetCursorPosition(cursorColumn, cursorRow);
    }
}