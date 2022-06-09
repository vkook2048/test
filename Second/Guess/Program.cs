using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guess
{
    // Сделать через 3 переменных
    // Сделать расчет вероятности для 1 милиона попыток

    class Program
    {
        const int MaxTries = 5;


        static void TestMillion()
        {
            Random rnd = new Random();

            for (int SNumber = 0; SNumber < 101; SNumber++)
            {

                int winCount = 0;
                int loseCount = 0;

                for (int i = 0; i < 1000000; i++)
                {
                    bool isWin = false;
                    // загадать рандомное число

                    int min = 0;
                    int max = 100;
                    int number = SNumber;
                    //int number = rnd.Next(0, 101);

                    for (int j = 0; j < MaxTries; j++)
                    {
                        // делаем предположение
                        int guess = (max + min + 1) / 2;

                        if (j == 0)
                        {
                            //int gmin = (int)(0.45 * (max + min) + 0.5);
                            //int gmax = (int)(0.55 * (max + min) + 0.5);

                            guess = rnd.Next(45, 56);
                        }


                        if (j == MaxTries - 1)
                        {
                            guess = rnd.Next(min, max + 1);
                        }

                        // Сравниваем и получаем ответ (больше меньше угадал)

                        if (number > guess)
                        {
                            min = guess + 1;
                            continue;
                        }
                        else if (number < guess)
                        {
                            max = guess - 1;
                            continue;
                        }
                        // если угадал - заканчиваем игру
                        else if (number == guess)
                        {
                            isWin = true;
                            break;
                        }


                    }

                    if (isWin)
                        winCount++;
                    else
                        loseCount++;
                }

                Console.WriteLine(SNumber + ": " + (100.0 * winCount / (winCount + loseCount)).ToString("F2") + " %");
                //Console.WriteLine($"winCount: {winCount} loseCount: " + loseCount);
             
                //Console.ReadLine();
            }

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            TestMillion();
            return;

            while (true)
            {
                Console.WriteLine("Who guesses? Press '1' for player or '0' for computer."); //кто угадывает?
                int player = int.Parse(Console.ReadLine());
                if (player == 1)
                {
                    GuessIsHuman();
                }
                else if (player == 0)
                {

                    GuessIsComputer();
                }
                else
                {
                    Console.WriteLine("Wrong");
                }

                Console.WriteLine("Do you want to play again? (yes/no)");
                string res = Console.ReadLine();
                if (res != "yes")
                {
                    break;
                }
            }
        }
        static void GuessIsHuman()
        {
            Random rnd = new Random(); // Объявлять один раз

            int numbercomputer = rnd.Next(0, 100);

            int countgame = 0;
            int version = int.Parse(Console.ReadLine());
            while (countgame < MaxTries)
            {
                if (numbercomputer == version)
                {
                    Console.WriteLine("You win!");
                    break;
                }
                else
                {
                    if (numbercomputer > version)
                        Console.WriteLine(">");
                    else
                        Console.WriteLine("<");

                    countgame++;
                    if (countgame == MaxTries)
                    {
                        Console.WriteLine("Game over");
                        Console.WriteLine(numbercomputer);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Try again");
                        version = int.Parse(Console.ReadLine());
                    }

                }
            }
        }

        static void GuessIsComputer()
        {
            Console.WriteLine("Think of a number. Press Enter when you're ready.");
            Console.ReadLine();
            int Max = 100;
            int Min = 0;
            
            int tryes = 0;

            do
            {
                int guess = (Max + Min + 1) / 2; 
                Console.WriteLine($"Is it {guess}? Please write Yes or if the answer is No write < or >.");
                string answer = Console.ReadLine();
                if (answer == "Yes")
                {
                    Console.WriteLine("Computer wins. Thanks for playing!");
                    break;
                }
                else if (answer == "<")
                {
                    Max = guess - 1;
                    tryes++;
                }
                else if (answer == ">")
                {
                    Min = guess + 1;
                    tryes++;
                }
                else
                {
                    Console.WriteLine("Wrong meaning. Please try again");
                }
            }
            while (tryes < MaxTries);
            if (tryes == MaxTries)
            {
                Console.WriteLine("You're win!");
            }
        }
    }
}
