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
            string[] operations = { /*0*/"sčítání", /*1*/"odčítání", /*2*/"násobení", /*3*/"dělení", /*4*/"mocnění", /*5*/"převod na jinou sooustavu" };
            float a, b;

            int operation = choose(operations);

            getNumbers(out a,out b, operation);

            string result = countIt(a, b, operation);

            Console.WriteLine(new string('_', ("Výsledek" + result.ToString()).Length + 2));

            Console.WriteLine("Výsledek: " + result);

            Console.ReadKey(); 
        }

        private static string countIt(float a, float b, int operation)
        {
            double result =
                operation == 0 ? a + b :
                operation == 1 ? a - b :
                operation == 2 ? a * b :
                operation == 3 ? a / b :
                operation == 4 ? Math.Pow(a, b):
                Convert.ToSingle(transmission(a, b));

            return result.ToString();
        }

        private static string transmission(float a, float b)
        {
            string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";
            int numeralSystem = Convert.ToInt32(b);
            int number = Convert.ToInt32(a);
            Boolean negative = number < 0;
            number *= negative ? -1 : 1;
            while (number != 0)
            { 
                result = alphabet[number % numeralSystem] + result;
                number = (number - number % numeralSystem) / numeralSystem;
            }
            result = negative ? "-" + result : result;
            return Convert.ToString(result);
        }

        static void getNumbers(out float a, out float b, int operation)
        {
            string[] textA = {"sčítanec: ","menšenec: ","činitel: ","dělenec: ","základ: ","číslo: "};
            a = operation == 5? numberCheck(textA[operation], 2): numberCheck(textA[operation]);
            string[] textB = { "sčítanec: ", "menšitel: ", "činitel: ", "dělitel: ", "exponent: ", "soustava: "};
            b = operation == 5 ? numberCheck(textB[operation], 3) : operation == 3? numberCheck(textB[operation], 1) : numberCheck(textB[operation]);
        }

        private static int choose(string[] option)
        {
            Console.Clear();
            for(int i = 0; i < option.Length; i++) Console.WriteLine((i == 0? " > ": "   ") + option[i]);
            int row = 0; // pozice kurzoru
            ConsoleKeyInfo keyInfo; // informace o stisknuté klávese
            Console.CursorVisible = false; //zneviditelním kurzor
            do // dělej
            {
                keyInfo = Console.ReadKey(true); // čte klávesy
                if (keyInfo.Key == ConsoleKey.DownArrow || (keyInfo.Key == ConsoleKey.UpArrow)) //pokud je klávesa šipka nahoru nebo dolu
                {
                    Console.SetCursorPosition(1, row); // nastaví pozici kurzoru
                    Console.Write(" "); //napíše mezeru tam kde byla šipka/kurzor
                    row += (keyInfo.Key == ConsoleKey.DownArrow) ? 1 : -1; // změní pozici kurzoru
                    row = (row + option.Length) % option.Length;
                    Console.SetCursorPosition(1, row); // nastaví pozici
                    Console.Write(">"); //napíše šipku tam kde je šipka/kurzor
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter); // dokud není zmáčknut enter
            Console.Clear();
            return row;
        }
        private static float numberCheck(string text)
        {
            return numberCheck(text, 0);
        }


        private static float numberCheck(string text, int anotherCondition) //1 - nula, 2 - celé číslo, 3 - rozsah
        {
            float number = 0;
            Boolean isValid = false;
            Boolean isVhole, isBetween, isNotZero, isNumber;
            while (!isValid)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                isNumber = float.TryParse(input, out number);
                isNotZero = !(anotherCondition == 1 && number == 0);
                isVhole = !(anotherCondition == 2 && number % 1 != 0);
                isBetween = !(anotherCondition == 3 && ((number > 35) || (number < 2) || (number % 1 != 0)));

                if (isNumber && isVhole && isNotZero && isBetween)
                {
                    isValid = true;
                }
                else
                {
                    MessageBox.Show(
                        !isNotZero? "Tak teoreticky je to ±∞, ale oficiálně ti musím sdělit, že toto není validní vstup, protože nulou dělit nelze. Zkus to znovu.":
                        !isVhole? "Prosím zadej celé číslo, necelé zatím neumím.":
                        !isBetween? "Prosím zadej celé číslo v rozahu 2-35":
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
