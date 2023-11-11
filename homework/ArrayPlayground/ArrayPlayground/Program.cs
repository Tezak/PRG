using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Made by Jan Borecky for PRG seminar at Gymnazium Voderadska, year 2023-2024.
 * Extended by students.
 */

namespace ArrayPlayground
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TODO 1: Vytvoř integerové pole a naplň ho pěti čísly.
            Random random = new Random();
            int[] array = new int[100];

            //TODO 2: Vypiš do konzole všechny prvky pole, zkus klasický for, kde i využiješ jako index v poli, a foreach (vysvětlíme si).
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, 10);    
            }
            foreach(int i in array) { Console.Write(i + " "); }

            //TODO 3: Spočti sumu všech prvků v poli a vypiš ji uživateli.
            int sum = array.Sum();
            Console.WriteLine("\nSuma je: " + sum);

            //TODO 4: Spočti průměr prvků v poli a vypiš ho do konzole.
            int average = (int)array.Average();
            Console.WriteLine("Průměr je: " + average);

            //TODO 5: Najdi maximum v poli a vypiš ho do konzole.
            int max = array.Max();
            Console.WriteLine("Maximum je: " + max);

            //TODO 6: Najdi minimum v poli a vypiš ho do konzole.
            int maxValue = int.MaxValue;
            int min = array.Min();
            Console.WriteLine("Minimum je: " + min);

            //TODO 7: Vyhledej v poli číslo, které zadá uživatel, a vypiš index nalezeného prvku do konzole.
            int index = 0;
            bool isValid = false;
            Console.Write("napiš číslo a já ho vyhledám: ");
            int cursorColumn = Console.CursorLeft;
            int number;
            while (!isValid)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out number);
                if (!isNumber)
                {
                    again(cursorColumn);                    
                }
                else 
                {
                    try { index = Array.IndexOf(array, number); isValid = true; }
                    catch { isValid = false; again(cursorColumn); }
                }

            }
            Console.WriteLine("index je: " + index);
            //TODO 8: Změň tvorbu integerového pole tak, že bude obsahovat 100 náhodně vygenerovaných čísel od 0 do 9. Vytvoř si na to proměnnou typu Random.

            //TODO 9: Spočítej kolikrát se každé číslo v poli vyskytuje a spočítané četnosti vypiš do konzole.
            int[] counts = new int[10];
            for (int i = 0; i < counts.Length; i++)
            {
                int count = 0;
                foreach (int ii in array)
                {
                    if (array[ii] == i) count++; 
                }
                counts[i] = count;
            }

            //TODO 10: Vytvoř druhé pole, do kterého zkopíruješ prvky z prvního pole v opačném pořadí.

            int[] arrayReversed = new int[array.Length];
            for (int i = 0; i < arrayReversed.Length; i++)
            {
                arrayReversed[i] = array[array.Length - 1 - i];
            }

            foreach (int i in arrayReversed) { Console.Write(i + " "); }
            Console.ReadKey();

        }

        private static void again(int cursorColumn)
        {
            Console.SetCursorPosition(cursorColumn, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth - cursorColumn));
            Console.SetCursorPosition(cursorColumn, Console.CursorTop);
        }
    }
}
