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
            float a, b;
            int operation;
            string result;
            Boolean again;

            clean(true);

            do
            {
                chooseOperation(out operation, operations);

                Boolean notOK = false;
                do
                {
                    readNumbers(out a, out b, operation);

                    countIt(a, b, operation, out result);

                    if (result == "∞" || result == "-∞")
                    {
                        notOK = true;
                        MessageBox.Show("Promiň, toto nedokážu vypočítat :( \n Výsledek je mimo mé limity. Zkus to prosím znovu s jinými čísly.");
                    }


                } while (notOK);
                
                writeResault(result, operation);

                askIfAgain(out again);

                clean();

            } while (again);
            
        }

        private static void clean(){ clean(false); }

        private static void clean(Boolean first)
        {
            Console.Clear();
            Console.WriteLine("\n   MOJE EPICKÁ KALKULAČKA" + 
                (first? " (v menu se pohybuj pomocí šipek a potvrď klávesou ENTER)": " ") 
                + "\n");
        }

        private static void askIfAgain(out Boolean again)
        {
            string[] options = {
                "Chci počítat dál",
                "Ukončit program"};
            again = choose(options, Console.CursorTop + 1) == 0;
        }

        private static void chooseOperation(out int operation, string[] operations) { Console.WriteLine(); operation = choose(operations, Console.CursorTop - 1); }

        private static void writeResault(string result, int operation)
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
            Boolean negative = number < 0;
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
            string[] textA = { "sčítanec: ", "menšenec: ", "činitel: ", "dělenec: ", "základ: ", "číslo: " };
            a = numberCheck("   " + textA[operation], operation, 'a');
            string[] textB = { "sčítanec: ", "menšitel: ", "činitel: ", "dělitel: ", "exponent: ", "soustava: " };
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
            return currentRow;
        }

        private static void rewriteTo(int currentRow, string replacement)
        {
            Console.SetCursorPosition(1, currentRow);
            Console.Write(replacement);
        }

       


        

        private static float numberCheck(string text, int anotherCondition, char ab) //1 - nula, 2 - celé číslo, 3 - rozsah
        {
            float number = 0;
            Boolean isValid = false;
            Boolean isVhole, isBetween, isNotZero, isNumber, isA;
            isA = ab == 'a';

            while (!isValid)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                isNumber = float.TryParse(input, out number);
                isNotZero = !(anotherCondition == 3 && number == 0f);
                isVhole = !(isA && anotherCondition == 5 && number % 1 != 0);
                isBetween = !(!isA && anotherCondition == 5 && ((number > 36) || (number < 2) || (number % 1 != 0)));

                if (isNumber && isVhole && isNotZero && isBetween)
                {
                    isValid = true;
                }
                else
                {
                    MessageBox.Show(
                        !isNotZero ? "Tak teoreticky je to ±∞, ale oficiálně ti musím sdělit, že toto není validní vstup, protože nulou dělit nelze. Zkus to znovu." :
                        !isVhole ? "Prosím zadej celé číslo, necelé zatím neumím." :
                        !isBetween ? "Prosím zadej celé číslo v rozahu 2-36" :
                        "Toto není validní vstup. Zkus to znovu", "Nevalidní vstup");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
            }
            return number;
        }
    }
}
