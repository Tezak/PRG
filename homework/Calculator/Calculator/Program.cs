using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

/*
 * Made by Jan Borecky for PRG seminar at Gymnazium Voderadska, year 2023-2024.
 * Extended by students.
 */

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * Pokud se budes chtit na neco zeptat a zrovna budu pomahat jinde, zkus se zeptat ChatGPT ;) - https://chat.openai.com/
             * 
             * ZADANI
             * Vytvor program ktery bude fungovat jako kalkulacka. Kroky programu budou nasledujici:
             * 1) Nacte vstup pro prvni cislo od uzivatele (vyuzijte metodu Console.ReadLine() - https://learn.microsoft.com/en-us/dotnet/api/system.console.readline?view=netframework-4.8.
             * 2) Zkonvertuje vstup od uzivatele ze stringu do integeru - https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number.
             * 3) Nacte vstup pro druhe cislo od uzivatele a zkonvertuje ho do integeru. (zopakovani kroku 1 a 2 pro druhe cislo)
             * 4) Nacte vstup pro ciselnou operaci. Rozmysli si, jak operace nazves. Muze to byt "soucet", "rozdil" atd. nebo napr "+", "-", nebo jakkoliv jinak.
             * 5) Nadefinuj integerovou promennou result a prirad ji prozatimne hodnotu 0.
             * 6) Vytvor podminky (if statement), podle kterych urcis, co se bude s cisly dit podle zadane operace
             *    a proved danou operaci - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements.
             * 7) Vypis promennou result do konzole
             * 
             * Mozna rozsireni programu pro rychliky / na doma (na poradi nezalezi):
             * 1) Vypis do konzole pred nactenim kazdeho uzivatelova vstupu co po nem chces
             * 2) a) Kontroluj, ze uzivatel do vstupu zadal, co mel (cisla, popr. ciselnou operaci). Pokud zadal neco jineho, napis mu, co ma priste zadat a program ukoncete.
             * 2) b) To same, co a) ale misto ukonceni programu opakovane cti vstup, dokud uzivatel nezada to, co ma
             *       - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-while-statement
             * 3) Umozni uzivateli zadavat i desetinna cisla, tedy prekopej kalkulacku tak, aby umela pracovat s floaty
             */
            string[] operations = { /*0*/"sčítání", /*1*/"odčítání", /*2*/"násobení", /*3*/"dělení", /*4*/"mocnění", /*5*/"převod na jinou sooustavu" };
            float a, b;

            int operation = choose(operations);

            getNumbers(out a,out b, operation);

            string result = (operation == 5)? result = transmission(a): countIt(a, b, operation);

            Console.WriteLine("Výsledek: " + result);

            Console.ReadKey(); //Toto nech jako posledni radek, aby se program neukoncil ihned, ale cekal na stisk klavesy od uzivatele.
        }

        private static string countIt(float a, float b, int operation)
        {
            double result =
                operation == 0 ? a + b :
                operation == 1 ? a - b :
                operation == 2 ? a * b :
                operation == 3 ? a / b :
                operation == 4 ? Math.Pow(a, b) :0;

            return result.ToString();
        }

        private static string transmission(float a)
        {
            string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";
            int numeralSystem = 16;
            int number = Convert.ToInt32(a);
            while (number != 0)
            { 
                result = alphabet[number % numeralSystem] + result;
                number = (number - number % numeralSystem) / numeralSystem;
            }

            return Convert.ToString(result);
        }

        static void getNumbers(out float a, out float b, int operation)
        {
            string[] textA = {"sčítanec: ","menšenec: ","činitel: ","dělenec: ","základ: ","číslo: "};
            a = numberCheck(textA[operation]);
            string[] textB = { "sčítanec: ", "menšitel: ", "činitel: ", "dělitel: ", "exponent: "};
            b = operation == 3? numberCheck(textB[operation],true) : operation != 5? numberCheck(textB[operation]): 0;
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
            return numberCheck(text, false);
        }


        private static float numberCheck(string text, Boolean division)
        {
            float number = 0;
            Boolean isValid = false;
            while (!isValid)
            {
                Console.Write(text);
                string input = Console.ReadLine();

                if (float.TryParse(input, out number) && !(division && number == 0))
                {
                    isValid = true;
                }
                else
                {
                    MessageBox.Show((division && number == 0)? "Tak teoreticky je to ±∞, ale oficiálně ti musím sdělit, že toto není validní vstup, protože nulou dělit nelze. Zkus to znovu.":"Toto není validní vstup. Zkus to znovu", "Nevalidní vstup");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
            }
            return number;
        }
    }
}
