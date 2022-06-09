using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //var text = File.ReadAllText(@"D:\Downloads\zagruzki s c\WordsStockRus.txt");
            var lines = File.ReadAllLines(@"D:\Downloads\zagruzki s c\WordsStockRus.txt");
            /*int[] ist = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                ist[i] = lines[i].Length;
            }
            Console.WriteLine($"{ist.Max()}, {ist.Min()}");*/

            Random rnd = new Random();
            int number = rnd.Next(0, lines.Length);
            string word = lines[number];
            string[] output = new string[word.Length];
            bool isWin = false;
            int left = 6;
            string remember = "";
            for (int i = 0; i < word.Length; i++)
            {
                output[i] = "_";
            }
            
            while(left!= 0 && !isWin)
            {
                ShowField(output);
                Console.WriteLine();
                string variant = Console.ReadLine();
                if (remember.Contains(variant))
                {
                    Console.WriteLine("Wrong meaning. Please try another one.");
                    Console.WriteLine($"Attempts left:{left}");
                }
                else
                {
                    if (!CheckingVariant(output, word, variant))
                    {
                        left--;
                    }
                    remember += variant;
                    IsWin(output);
                    Console.WriteLine($"Attempts left:{left}");
                }
            }
            GameOver(left, word);
            
        }
        static void ShowField(string[] output)
        {
            foreach (var item in output)
            {
                Console.Write($"{item} ");
            }
        }

        // сделать возвратное значение угадал/ не угадал 
        // и убрать  ref int left - делать уменьшение в основном цикле программы
        // и сделать как ыбл remember в прошлый раз
        static bool CheckingVariant(string[] output, string word, string variant)
        {
            int find = word.IndexOf(variant);
          
            if (find == -1)
            {
                return false;
            }
            else
            {
                while (find != -1)
                {
                    output[find] = variant;
                    find = word.IndexOf(variant, find + 1);
                }
                return true;
            }
        }
        static bool IsWin(string[] output)
        {
            int count = 0;
            for (int i = 0; i < output.Length; i++)
            {
                if (output[i] == "_")
                {
                    count++;
                }
                
            }
            if (count == 0)
            {
                Console.WriteLine("You're winn!");
                return true;
            }
            return false;
        }
        static void GameOver(int left, string word)
        {
            Console.WriteLine($"The game is over. Attempts left:{left}. Word:{word}");
        }
    }
}
