using System.Globalization;
using System.Security.Cryptography;
using System.Threading;

internal class Program
{
    private static int[,]? matrix;
    private static string[] operations = { 
        /*0*/ "Vypiš n-tý řádek pole", 
        /*1*/ "Vypiš n-tý sloupec pole", 
        /*2*/ "Prohoď prvek na souřadnicích[xFirst, yFirst] s prvkem na souřadnicích[xSecond, ySecond]", 
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
        /*13*/ "vynásob matice",
                //pole a*a
        /*14*/ "Vypiš prvky na vedlejší diagonále",
        /*15*/ "Otoč pořadí prvků na vedlejší diagonále"
    };

    private static void Main(string[] args)
    {

        FillMatrix();
        WriteMatrix();

        do
        {
            switch (Choose(operations)) //spouští vybranou operaci
            {
                case 0: writeNRow(); break;
                case 1: writeNColumn(); break;
                case 2: swapTwoNumbers(); break;
                case 3: swapRows(); break;
                case 4: swapColumns(); break;
                case 5: multiplyMatrice(); break;
                case 6: multiplyRow(); break;
                case 7: multiplyColumn(); break;
                case 8: Transposition(); break;
                case 9: writeMainDiagonal(); break;
                case 10: reverseMainDiagonal(); break;
                case 11: addTwoMAtrix(); break;
                case 12: substractTwoMAtrix(); break;
                case 13: multiplyTwoMAtrix(); break;
                // pole a*a
                case 14: writeSecondaryDiagonal(); break;
                case 15: reverseSecondaryDiagonal(); break;
            }
        } while (askIfAgain()); //dokud chce uživatel pokračovat v počítání
    }

    private static void multiplyTwoMAtrix()
    {
        twoMatrixOperations(2);
    }

    private static void substractTwoMAtrix()
    {
        twoMatrixOperations(1);
    }

    private static void addTwoMAtrix()
    {
        twoMatrixOperations(0);
    }

    /// <summary>Provádí operace s dvěmi maticemi 0 - přičítá matici, 1 - odečítá matici, 2 - násobí maticí</summary>
    /// <returns>
    /// Vypíše výsledek maticové početní operace a nastaví naši matici na výsledek této operace
    /// </returns>
    private static void twoMatrixOperations(int operation)
    {
        Console.WriteLine("druhá matice: ");
        int[,] matrix2;
        string[] valuesType = { "čísla od 1 do a*b", "náhodná čísla" };
        bool random = Convert.ToBoolean(Choose(valuesType));
        if (operation == 2) {
            Console.Write("počet řádků matice 2: ");
            int b = NumberCheck(2);
            matrix2 = GenerateValues(random, matrix.GetLength(1), b);
        }
        else matrix2 = GenerateValues(random, matrix.GetLength(0), matrix.GetLength(1));
        WriteMatrix(matrix2);
        Console.WriteLine((operation == 0) ? "součet matic:": (operation == 1) ? "rozdíl matic:" : "součin matic");

        if (operation == 2)
        {
            int[,] matrix3 = new int[matrix.GetLength(0), matrix2.GetLength(1)];
            for (int i = 0; i < matrix3.GetLength(0); i++)
            {
                for (int j = 0; j < matrix3.GetLength(1); j++)
                {
                    matrix3[i, j] = 0;
                    for (int k = 0; k < matrix.GetLength(1); k++)
                    {
                        matrix3[i, j] += matrix[i, k] * matrix2[k, j];
                    }
                }
            }
            matrix = matrix3;
            WriteMatrix();
            return;
        }
        else 
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = (operation == 0) ? (matrix[i, j] + matrix2[i, j]) : (matrix[i, j] - matrix2[i, j]);
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
    private static int[,] GenerateValues(bool random, int columns, int rows)
    {
        int max = 2000;
        int[,] matrix = new int[columns, rows];
        int number = 1;
        Random rnd = new Random();
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                matrix[i, j] = random ? rnd.Next(0, max) : number;
                number++;
            }
        }
        if (!random) max = number; // nastavuje maximální honotu čísla, které se může vyskytovat v poli
        return matrix;
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

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                transposedMatrix[j, i] = matrix[i, j];
            }
        }
        matrix = transposedMatrix;
        WriteMatrix();
    }

    /// <summary>Vynásobení sloupce</summary>
    /// <returns>
    /// Vynásobí sloupec a vypíše matici do konzole.
    /// </returns>
    private static void multiplyColumn()
    {
        Console.Write("Vynásobit sloupec: ");
        int column = NumberCheck(1);
        Console.Write("číslem: ");
        int number = NumberCheck();

        for (int j = 0; j < matrix.GetLength(0); j++)
        {
            matrix[j, column] *= number;
        }
        WriteMatrix();
    }

    /// <summary>Vynásobení řádku</summary>
    /// <returns>
    /// Vynásobí řádek a vypíše matici do konzole.
    /// </returns>
    private static void multiplyRow()
    {
        Console.Write("Vynásobit řádek: ");
        int row = NumberCheck(0);
        Console.Write("číslem: ");
        int number = NumberCheck();

        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            matrix[row, j] *= number;
        }
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

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = matrix[i, j] * number;
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
        for (int i = 0; 2 * i < matrix.GetLength(0); i++)
        {
            int j = matrix.GetLength(0) - i - 1;
            int save = matrix[j, i];
            matrix[j, i] = matrix[i, j];
            matrix[i, j] = save;
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

    /// <summary>Prohodí dva sloupce</summary>
    /// <returns>
    /// Od uživatele přijme jaké sloupce má prohodit, tyto sloupce prohodí a pak pole vypíše na konzoli.
    /// </returns>
    private static void swapColumns()
    {
        Console.Write("Prohození sluopce: ");
        int nColSwap = NumberCheck(1);
        Console.Write("se sloupcem: ");
        int mColSwap = NumberCheck(1);

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            int save = matrix[i, nColSwap];
            matrix[i, nColSwap] = matrix[i, mColSwap];
            matrix[i, mColSwap] = save;
        }
        WriteMatrix();
    }

    /// <summary>Prohodí dva řádky</summary>
    /// <returns>
    /// Od uživatele přijme jaké řádky má prohodit, tyto řádky prohodí a pak pole vypíše na konzoli.
    /// </returns>
    private static void swapRows()
    {
        Console.Write("Prohození řádku: ");
        int nRowSwap = NumberCheck(0);
        Console.Write("s řádkem: ");
        int mRowSwap = NumberCheck(0);

        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            int save = matrix[nRowSwap, j];
            matrix[nRowSwap, j] = matrix[mRowSwap, j];
            matrix[mRowSwap, j] = save;
        }
        WriteMatrix();
    }

    /// <summary>Prohodí dvě čísla</summary>
    /// <returns>
    /// Od uživatele přijme souřadnice čísel, které má prohodit, tato čísla prohodí a pak pole vypíše na konzoli.
    /// </returns>
    private static void swapTwoNumbers()
    {
        Console.Write("Prohození čísla x1: ");
        int xFirst = NumberCheck(0);
        Console.Write("y1: ");
        int yFirst = NumberCheck(1);
        Console.Write("s číslem číslem x2: ");
        int xSecond = NumberCheck(0);
        Console.Write("y2: ");
        int ySecond = NumberCheck(1);

        int save = matrix[xFirst, yFirst];
        matrix[xFirst, yFirst] = matrix[xSecond, ySecond];
        matrix[xSecond, ySecond] = save;

        WriteMatrix();
    }

    /// <summary>Vypíše sloupec</summary>
    /// <returns>
    /// Vypíše sloupec na konzoli
    /// </returns>
    private static void writeNColumn()
    {
        Console.Write("Vypsání sloupce: ");
        int nColumn = NumberCheck(1);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write(matrix[i, nColumn] + " ");
        }
        Console.WriteLine("\n");
    }

    /// <summary>Vypíše řádek</summary>
    /// <returns>
    /// Vypíše řádek na konzoli
    /// </returns>
    private static void writeNRow()
    {
        Console.Write("Vypsání řádku: ");
        int nRow = NumberCheck(0);
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            Console.Write(matrix[nRow, i] + " ");
        }
        Console.WriteLine("\n");
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
            Console.WriteLine((Console.CursorTop - cursorPosition == 0 ? " > " : "   ") + options[i]);
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
        WriteMatrix(matrix); 
    }

    /// <summary>Vypíše zadanou matici do konzole</summary>
    private static void WriteMatrix(int[,] matrix)
    {
        int digitsMax = 0; //maximální počet cifer

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                int digits = Convert.ToString(matrix[i, j]).Length;
                if (digits > digitsMax) digitsMax = digits;
            }
        }

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Thread.Sleep(10);
                Console.CursorLeft = j * (digitsMax + 1);
                Console.Write(matrix[i, j]);
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