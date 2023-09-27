using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] operations = { 
                /*0*/"sčítání", 
                /*1*/"odčítání", 
                /*2*/"násobení", 
                /*3*/"dělení", 
                /*4*/"mocnění", 
                /*5*/"převod na jinou soustavu" };

            string result;
            bool again;

            clean(true);

            do
            {
                chooseOperation(out int operation, operations);

                bool notOK;
                do
                {
                    notOK = false;

                    readNumbers(out float a, out float b, operation);

                    countIt(a, b, operation, out result);

                    if (result == "∞" || result == "-∞")
                    {
                        notOK = true;
                        MessageBox.Show("Promiň, toto nedokážu vypočítat :( \n Výsledek je mimo mé limity. Zkus to prosím znovu s jinými čísly.");
                    }


                } while (notOK);
                
                writeResult(result, operation);

                askIfAgain(out again);

                clean();

            } while (again);
            
        }


        private static void clean() {clean(false); }

        private static void clean(bool first)
        {
            Console.Clear();
            Console.WriteLine("" +
                "\n      _          _          _          _          _" +
                "\n    >(')____,  >(')____,  >(')____,  >(')____,  >(') ___," +
                "\n      (` =~~/    (` =~~/    (` =~~/    (` =~~/    (` =~~/" +
                "\n   ~^~^`---'~^~^~^`---'~^~^~^`---'~^~^~^`---'~^~^~^`---'~^~^~");

            Console.WriteLine("\n   MOJE KACHNÍ KALKULAČKA" + 
                (first? " (v menu se pohybuj pomocí šipek a potvrď klávesou ENTER)": " ") 
                + "\n");
        }

        private static void askIfAgain(out bool again)
        {
            string[] options = {
                "Chci počítat dál",
                "Ukončit program"};
            again = choose(options, Console.CursorTop + 1) == 0;
        }

        private static void chooseOperation(out int operation, string[] operations) { Console.WriteLine(); operation = choose(operations, Console.CursorTop - 1); }

        private static void writeResult(string result, int operation)
        {
            string[] text = { 
                "součet: ", 
                "rozdíl: ", 
                "součin: ", 
                "podíl: ", 
                "mocnina: ", 
                "převedené číslo: " };

            string textO = text[operation];
            Console.WriteLine("   " + new string('_', (textO + result.ToString()).Length + 2));
            Console.WriteLine("   " + textO + result);
        }

        private static void countIt(float a, float b, int operation, out string result)
        {
            float fResult =
                operation == 0 ? a + b :
                operation == 1 ? a - b :
                operation == 2 ? a * b :
                operation == 3 ? a / b :
                operation == 4 ? Convert.ToSingle(Math.Pow(a, b)) : 0;

            result = operation == 5? baseConvertion(a, b): fResult.ToString();
        }

        private static string baseConvertion(float a, float b)
        {
            string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";
            int @base = Convert.ToInt32(b);
            int number = Convert.ToInt32(a);
            bool negative = number < 0;
            number *= negative ? -1 : 1;
            while (number != 0)
            { 
                result = alphabet[number % @base] + result;
                number = (number - number % @base) / @base;
            }
            result = negative ? "-" + result : result;
            return Convert.ToString(result);
        }

       
        static void readNumbers(out float a, out float b, int operation)
        {
            clean();
            string[] textA = {
                "sčítanec: ",
                "menšenec: ", 
                "činitel: ", 
                "dělenec: ", 
                "základ: ",
                "zadávej pouze čísla celá a jako soustavu číslo 2-36 \n\n   číslo v desítkové soustavě: " };

            a = numberCheck("   " + textA[operation], operation, 'a');

            string[] textB = { 
                "sčítanec: ", 
                "menšitel: ", 
                "činitel: ", 
                "dělitel: ", 
                "exponent: ", 
                "soustava do které cheš převést: " };

            b = numberCheck("   " + textB[operation], operation, 'b');
        }

        private static int choose(string[] options, int cursorPosition)
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

        private static float numberCheck(string text, int operation, char ab)
        {
            float number = 0;
            bool isValid = false;
            bool isA = ab == 'a';
            Console.Write(text);
            int cursorColumn = Console.CursorLeft;

            while (!isValid)
            {
                Console.CursorVisible = true;
                string input = Console.ReadLine();
                string[] inputField = input.Split('=');
                if(inputField.Length == 2) input = inputField[1];
                Console.CursorVisible = false;
                bool isNumber = float.TryParse(input, out number);
                bool isNotZero = !(operation == 3 && number == 0f);
                bool isWhole = !(isA && operation == 5 && number % 1 != 0);
                bool isBetween = !(!isA && operation == 5 && ((number > 36) || (number < 2) || (number % 1 != 0)));

                isValid = validate(isNumber, "Toto není validní vstup. Zkus to znovu")
                    && validate(isNotZero, "Tak teoreticky je to ±∞, ale oficiálně ti musím sdělit, že toto není validní vstup, protože nulou dělit nelze. Zkus to znovu.")
                    && validate(isWhole, "Prosím zadej celé číslo, necelé zatím neumím.")
                    && validate(isBetween, "Prosím zadej celé číslo v rozahu 2-36");
                if(!isValid)
                {
                    Console.SetCursorPosition(cursorColumn, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth - cursorColumn));
                    Console.SetCursorPosition(cursorColumn, Console.CursorTop);
                }
                
            }
            return number;
        }

        private static bool validate(bool condition, string message)
        {
            if (condition) { 
                return true; 
            } 
            else { 
                MessageBox.Show("\n   (@_\n\\\\\\_\\\n<____)\n" + message, "Nevalidní vstup");
                return false; 
            }
        }
    }
}
