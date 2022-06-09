using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GallowsComputer
{
    // 1 пользователь вводит количество букв в слове
    // 2 компьтер грузит словарь
    // 3 компьютер выкидывает все слова с иным количеством букв
    // 4 компьютер считает самую популярную букву в словаре
    // 5 компьютер спрашивает есть ли такая буква буква в слове
    // 6 если нет - компьтер убивает из словаря все слова с этой буквой
    // 7 если да - то пльзователь вводит на каих местах стоит эта буква _ _ _ _ _ => _ а _ _ _
    // 8 дальше переход к шагу 4
    // завершение игры
    // Если в словаре нет слов - он стал пустым - компьтер пишет я не знаю такого слова я сдаюсь
    // Если компьютер отгадал слово - он пишет что он выиграл
    // Если кончились попытки он пишет что проиграл и просит пользователя назвать слово
    // Если слово есть в его словаре он пишет ай ай ай
    // а если нет то пишет - обманщик - нет такого слова! (но сохраняет его в отдельный файл)
    class Program
    {
        static void Main(string[] args)
        {
            string word = Console.ReadLine();
            int numberOfLetters = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == '_')
                {
                    numberOfLetters++;
                }
            }
            var lines = File.ReadAllLines(@"D:\Downloads\zagruzki s c\WordsStockRus.txt");
            List<string> variants = new List<string> { };
            Dictionary<int, char> alphabet = new Dictionary<int, char>();
            int[] alf = new int[33];
            char mostPopularLetter = '!';
            string contains = "";
            int tries = 0;
            bool isEnd = false;
            List<string> toRemember = new List<string> { };
            List<char> alreadyhave = new List<char> { };
            string result = "";
            bool isAllWord = false;
            int index = -1;

            variants = Sorting(numberOfLetters, lines, variants);
            alphabet = AlphabetCreation(alphabet);

            while (!isEnd)
            {
                mostPopularLetter = CountingTheMostPopularLetter(alphabet, variants, alf, ref alreadyhave);
                Console.WriteLine($"Does your word contains {mostPopularLetter}?");
                contains = Console.ReadLine();
                if (contains == "no")
                {
                    variants = DeleteNo(variants, mostPopularLetter);
                    tries++;
                }
                else
                {
                    word = contains;
                    variants = DeleteYes(variants, mostPopularLetter, contains, index);
                }
                isAllWord = IsAllWord(word);
                isEnd = IsEnd(contains, tries, variants, isAllWord);
            }
            result = ResultOfTheGame(word, tries, variants);
            bool containedInDictionary = ContainedInDictionary(result, lines);
            if (!containedInDictionary && result != "-")
            {
                Console.WriteLine("You're a liar! There is no such word!");
                toRemember.Add(result);
            }
            /*else if (lines.Contains(result))
            {
                Console.WriteLine("Oh no! This word is already in my dictionary.");
            }*/

        }
        static List<string> Sorting(int numberOfLetters, string[] lines, List<string> variants)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length == numberOfLetters)
                {
                    variants.Add(lines[i]);
                }
            }
            return variants;
        }
        static Dictionary<int, char> AlphabetCreation(Dictionary<int, char> alphabet)
        {

            char[] alpchararray = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
            int key = 0;
            foreach (char value in alpchararray)
            {
                alphabet.Add(key, value);
                key++;
            }
            return alphabet;
        }
        static char CountingTheMostPopularLetter(Dictionary<int, char> alphabet, List<string> variants, int[] alf, ref List<char> alreadyhave)
        {
            Array.Clear(alf, 0, alf.Length);
            for (int i = 0; i < variants.Count; i++)
            {
                for (int j = 0; j < alphabet.Count; j++)
                {
                    if (variants[i].Contains(alphabet[j]))
                    {
                        alf[j]++;
                    }
                }
            }
            int indexofmax = Array.IndexOf(alf, alf.Max());
            for (int i = 0; i < alreadyhave.Count; i++)
            {
                if (alreadyhave.Contains(alphabet[indexofmax]))
                {
                    alf[indexofmax] = 0;
                }
                indexofmax = Array.IndexOf(alf, alf.Max());
            }
            alreadyhave.Add(alphabet[indexofmax]);
            
            return alphabet[indexofmax];
        }
        static List<string> DeleteNo(List<string> variants, char mostPopularLetter)
        {
            for (int i = 0; i < variants.Count;)
            {
                if (variants[i].Contains(mostPopularLetter))
                {
                    variants.Remove(variants[i]);
                }
                else
                {
                    i++;
                }
            }
            return variants;
        }
        static List<string> DeleteYes(List<string> variants, char mostPopularLetter, string contains, int index)
        {
            contains = contains.Replace(" ", "");

            index = contains.IndexOf(mostPopularLetter, 0, contains.Length);
            
            for (int i = 0; i < variants.Count;)
            {
                if (variants[i].IndexOf(mostPopularLetter, 0) != index)
                {
                    variants.Remove(variants[i]);
                }
                else
                {
                    i++;
                }
            }
            return variants;
        }
        static bool IsAllWord(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == '_')
                {
                    return true;
                }
            }
            return false;
        }
        static bool IsEnd(string word, int tries, List<string> variants, bool isAllWord)
        {
            if (tries > 5 || variants.Count == 0 || !isAllWord)
            {
                Console.WriteLine("Game over");

                return true;
            }
            
            return false;
        }
        static string ResultOfTheGame(string word, int tries, List<string> variants)
        {
            if (!word.Contains('_'))
            {
                Console.WriteLine("I winn!");
            }
            else if (tries == 6 && word.Contains('_'))
            {
                Console.WriteLine("You're winn! Please enter your word");
                return Console.ReadLine();
            }
            else if (variants.Count == 0)
            {
                Console.WriteLine("I don't know the word. I give up");
            }

            return "-";
        }
        static bool ContainedInDictionary(string result, string[] lines)
        {

            if (lines.Contains(result))
            {
                Console.WriteLine("Oh no! This word is already in my dictionary.");
                return true;
            }
            return false;
        }
       
    }
}
