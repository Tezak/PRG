using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Made by Jan Borecky for PRG seminar at Gymnazium Voderadska, year 2023-2024.
 * Extended by students.
 */

namespace SortingPlayground
{

    internal class Program
    {
        //Pokud si nejsi jistý/á, co dělat, podívej se do prezentace, na videa na YT, co jsem doporučoval, googluj a nebo mě zavolej a já ti poradím.

        
            //throw new NotImplementedException();
        

        static int[] Sort(int[] arr, int start, int end)
        {
            int[] sortedArray = (int[])arr.Clone();

            if (end != start)
            {
                int middle = (end + start) / 2;
                Sort(arr, start, middle);
                Sort(arr, middle + 1, end);

                sortedArray = Merge(arr, start, middle, end);
            }
            return sortedArray;
        }
        static int[] Merge(int[] arr, int start, int middle, int end)
        {
            int nl = middle - start + 1;
            int nr = end - middle;

            int[] tmpL = new int[nl];
            int[] tmpR = new int[nr];

            int i, j;

            for (i = 0; i < nl; i++)
                tmpL[i] = arr[start + i];

            for (j = 0; j < nr; j++)
                tmpR[j] = arr[middle + j + 1];

            i = 0;
            j = 0;
            int k = start;
            while (i < nl && j < nr)
            {
                if (tmpL[i] < tmpR[j])
                {
                    arr[k] = tmpL[i];
                    i++;
                }
                else
                {
                    arr[k] = tmpR[j];
                    j++;
                }
                k++;
            }
            while (i < nl)
            {
                arr[k] = tmpL[i];
                i++;
                k++;
            }
            while (j < nr)
            {
                arr[k] = tmpR[j];
                j++;
                k++;
            }
            return arr;
        }

        static int[] QuickSort(int[] array)
        {
            int[] sortedAraay = (int[])array.Clone();
            if()
            int pivot = sortedArray.Last();
            throw new NotImplementedException();
        }

        static int[] BubbleSort(int[] array)
        {
            int[] sortedArray = (int[])array.Clone(); // Řaď v tomto poli, ve kterém je výchoze zkopírováno všechno ze vstupního pole.
            int i = 1;
            int j = 1;
            while (i != 0)
            {
                i = 0;
                for(int k = 0; k < sortedArray.Length - j; k++)
                {
                    if (sortedArray[k] > sortedArray[k + 1])
                    {
                        int value = sortedArray[k];
                        sortedArray[k] = sortedArray[k + 1];
                        sortedArray[k + 1] = value;
                        i++;
                    }
                }
            }
            return sortedArray;
        }

        static int[] SelectionSort(int[] array)
        {
            int[] sortedArray = (int[])array.Clone(); // Řaď v tomto poli, ve kterém je výchoze zkopírováno všechno ze vstupního pole.

            for (int i = 0; i < sortedArray.Length; i++)
            {
                int min = i;
                for (int j = i; j < sortedArray.Length; j++)
                {
                    if (sortedArray[j] < sortedArray[min]) min = j;
                }

                int value = sortedArray[i];
                sortedArray[i] = sortedArray[min];
                sortedArray[min] = value;
            }
                

            return sortedArray;
        }

        static int[] InsertionSort(int[] array)
        {
            int[] sortedArray = (int[])array.Clone(); // Řaď v tomto poli, ve kterém je výchoze zkopírováno všechno ze vstupního pole.
            for(int i = 0; i < sortedArray.Length - 1; i++)
            {
                int j = i + 1;
                int value = sortedArray[j];

                while (j > 0 && value < sortedArray[j - 1])
                {
                    sortedArray[j] = sortedArray[j - 1];
                    j--;
                }
                sortedArray[j] = value;

            }
            return sortedArray;
        }

        //Naplní pole náhodnými čísly mezi 1 a velikostí pole.
        static void FillArray(int[] array)
        {
            Random rng = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rng.Next(1, array.Length + 1);
            }
        }

        //Vypíše pole do konzole.
        static void WriteArrayToConsole(int[] array, string arrayName)
        {
            Console.Write(arrayName + " = [");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                if (i < array.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.Write("]\n\n");
        }

        //Zavolá postupně Bubble sort, Selection sort a Insertion sort pro zadané pole (a vypíše jeho jméno pro přehlednost)
        static void SortArray(int[] array, string arrayName)
        {
            Console.WriteLine($"Řadím {arrayName}:");
            int[] sortedArray;

            sortedArray = BubbleSort(array);
            WriteArrayToConsole(sortedArray, arrayName + " seřazené Bubble sortem");

            sortedArray = SelectionSort(array);
            WriteArrayToConsole(sortedArray, arrayName + " seřazené Selection sortem");

            sortedArray = InsertionSort(array);
            WriteArrayToConsole(sortedArray, arrayName + " seřazené Insertion sortem");

            sortedArray = Sort(array, 0, array.Length - 1);
            WriteArrayToConsole(sortedArray, arrayName + " seřazené Merge sortem");

            sortedArray = QuickSort(array);
            WriteArrayToConsole(sortedArray, arrayName + " seřazené Quick sortem");

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] smallArray = new int[10];
            FillArray(smallArray);

            int[] mediumArray = new int[100];
            FillArray(mediumArray);

            int[] largeArray = new int[1000];
            FillArray(largeArray);

            WriteArrayToConsole(smallArray, "Malé pole");
            SortArray(smallArray, "Malé pole");

            WriteArrayToConsole(mediumArray, "Střední pole");
            SortArray(mediumArray, "Střední pole");

            WriteArrayToConsole(largeArray, "Velké pole");
            SortArray(largeArray, "Velké pole");

            Console.ReadKey();
        }
    }
}
