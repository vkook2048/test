using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*namespace First
{
    class Car
    {
        public static string Factory;

        public static void PrintFactory()
        {
            Console.WriteLine(Factory);
        }


        public string Name;

        public void PrintName()
        {
            Console.WriteLine(Name);
        }
        
    }

    class Program
    {

        static string p2;

        static Car car4;

        static void Main(string[] args)
        {
            Car.Factory = "zavod";
            Car.PrintFactory();

            var car1 = new Car();
            car1.Name = "Toyota";
            car1.PrintName();

            var car2 = new Car();
            car2.Name = "Lada";
            car2.PrintName();

            string p1;
            Car car3;
            int i1;

            /*Console.WriteLine(p1);
            Console.WriteLine(car3);
            Console.WriteLine(i1);*/

            //Console.WriteLine(p2.ToString());

            

            //car1.Name.Contains()

            /* Console.WriteLine("Hello, World!");
             var y = (int)(63.0 / 10.0);
             string s = "Goodbye.";
             double x = Math.Pow(34 ,2);
             double d = x + y + 1.34;
             char o = 'O';
             Console.WriteLine(y);
             Console.WriteLine(s);
             Console.WriteLine("x: " + x);
             Console.WriteLine(d);
             Console.WriteLine(o);
             Console.Write("Mission ");
             Console.Write("complated");
            Console.Read();
        }
    }
} */
namespace First
{

    class IMyNullable<T>
         where T:new()
    {

        public T GetValueOrDefault()
        {
            if (_hasValue)
                return _value;

            return new T();
        }

        private T _value;
        private bool _hasValue = false;

        public bool HasValue 
        { 
            get { return _hasValue; }
        }

        int _prop1;
        public int Prop1
        {
            get { return _prop1; }
            set { _prop1 = value; }
        }

        public int Prop2 { get; private set; }

        public T Value
        {
            get 
            {
                if (!_hasValue)
                    throw new Exception("NULL!!!!");

                return _value;
            }
            set
            {
                _value = value;
                _hasValue = _value != null;
            }
        }

        //public static explicit operator Digit(byte b) => new Digit(b);
        public static explicit operator IMyNullable<T>(T val)
        {
            return new IMyNullable<T>() { Value = val };
        }

    }

    class Program
    {
        // Lazy load

        private static bool? _isOs32 = null;

        public static bool IsOs32()
        {
            if (!_isOs32.HasValue)
            {
                _isOs32 = IntPtr.Size == 4;
            }

            return _isOs32.Value;
        }


        static void Main(string[] args)
        {
            /* IMyNullable<int> nulableInt = null;
             nulableInt.Value = 5;
             nulableInt = (IMyNullable<int>) 6;*/

            /*int[] test = new int[11];
            int mem = 0;
            int[] change = new int[test.Length / 2 + 1];
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = mem;
                mem++;
            }
            for (int i = 0; i < test.Length; i++)
            {
                if(test[i] < 6)
                {
                    change[i] = test[i];
                }
            }
            test = change;
            for (int i = 0; i < test.Length; i++)
            {
                if (test[i] > 5)
                {
                    Array.Clear(test, i, 1);                
                }
            }
            
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine(); */
            const int MaxTries = 5;

            Console.WriteLine("Who guesses? Press '1' for player or '0' for computer."); //кто угадывает?
            int player = int.Parse(Console.ReadLine());
            if(player == 1)
            {
                Random rnd = new Random();
                int numbercomputer = rnd.Next(0, 100);
                
                int countgame = 0;                
                while (countgame < MaxTries)
                {
                    int version = int.Parse(Console.ReadLine());
                    if (numbercomputer == version)
                    {
                        Console.WriteLine("You win!");
                        break;
                    }
                    else
                    { 
                        countgame++;
                        if (countgame == MaxTries)
                        {
                            Console.WriteLine("Game over");
                            Console.WriteLine($"Number: {numbercomputer}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Try again");                            
                        }
                        
                    }
                }
            }
            else if(player == 0)
            {
                Console.WriteLine("Think of a number. Press Enter when you're ready.");
                Console.ReadLine();
                int[] variants = new int[101];
                int member = 0;
                for (int i = 0; i < variants.Length; i++)
                {
                    variants[i] = member;
                    member++;
                }
                int tryes = 0;
                int guess = (variants.Max()) / 2;

                do
                {
                    
                    Console.WriteLine($"Is it {guess}? Please write Yes or if the answer is No write < or >.");
                    string answer = Console.ReadLine();
                    if (answer == "Yes")
                    {
                        Console.WriteLine("Computer wins. Thanks for playing!");
                        break;
                    }
                    else if (answer == "<")
                    {
                       
                        for (int i = 0; i < variants.Length; i++)
                        {
                            if (variants[i] > guess)
                            {
                                Array.Clear(variants, i, 1);
                                
                            }
                        }
                        if (variants[1] == 0)
                        {
                            int Min = 0;
                            foreach (var item in variants)
                            {
                                if (item > 0)
                                {
                                    Min = item;
                                    break;
                                }
                            }
                            
                            guess = (variants.Max() + Min) / 2;
                        }
                        else
                        {
                            guess = (variants.Max()) / 2;
                        }

                        

                        tryes++;
                        continue;
                    }
                    else if (answer == ">")
                    {

                        for (int i = 0; i < variants.Length; i++)
                        {
                            if (variants[i] < guess)
                            {
                                Array.Clear(variants, i, 1);
                            }
                        }
                        tryes++;
                        int Min = 0;
                        foreach (var item in variants)
                        {
                            if (item > 0)
                            {
                                Min = item;
                                break;
                            }
                        }
                        
                        guess = (variants.Max() + Min) / 2;
                    }
                    else
                    {
                        Console.WriteLine("Wrong meaning. Please try again");
                        continue;
                    }

                }
                while (tryes <= MaxTries - 1);
                if (tryes == MaxTries)
                {
                    Console.WriteLine("You're win!");
                }

            }
            else
            {
                Console.WriteLine("Wrong");
            }

            Console.ReadLine();
            Console.WriteLine("Another program");
            Console.ReadLine();

            double[] numb = new double[4];
            int count = 0;
            do
            {
                double a = double.Parse(Console.ReadLine());
                numb[count] = a;
                count++;
            }
            while (count < numb.Length);

            ComplexNumber c1 = new ComplexNumber(numb[0], numb[1]);
            ComplexNumber c2 = new ComplexNumber(numb[2], numb[3]);

            ComplexNumber result_addition = c1.Plus(c2);
            ComplexNumber result_subtraction = c1.Minus(c2);

            Console.WriteLine($"Result of addition: Real = {result_addition.Real}, Imaginary = {result_addition.Imaginary}");

            Console.WriteLine($"Result of subtraction: Real = {result_subtraction.Real}, Imaginary = {result_subtraction.Imaginary}");

            Console.ReadLine();

            Calculator calc = new Calculator();
            double square1 = calc.CulcTriangleSquare(5, 12, 13);
            double square2 = calc.CulcTriangleSquare(8, 5);
            double square3 = calc.CulcTriangleSquare(12, 13, 30);
            Console.WriteLine($"square1 = {square1}, square2 = {square2}, square3 = {square3}");
            Console.ReadKey();
        }
        static void List(string[] args)
        {
            var list = new List<int>();
            string b = Console.ReadLine();
            double a = double.Parse(b);
            string s = string.Format("{0:f1}", a);
            Console.WriteLine(s);
            Console.WriteLine(a.ToString("f1"));

            Console.WriteLine(Math.Round(17.5, MidpointRounding.AwayFromZero));
        }
        static void First_first(string[] args)
        {
            string name = "";
            Console.Write("What's your name?");

            Console.WriteLine();

            name = Console.ReadLine();

            Console.WriteLine("Hello, " + name);
        }
        static void First_second()
        {
            string x = Console.ReadLine();
            string y = Console.ReadLine();

            int ix = int.Parse(x);
            int iy = int.Parse(y);

            Console.WriteLine($"x: {ix}, y: {iy}");
            int ij = ix;
            ix = iy;
            iy = ij;
            Console.WriteLine($"x: {ix}, y: {iy}");
        }
        static void First_third(string[] args)
        {
            string a = Console.ReadLine();
            Console.WriteLine(a.Length);
        }
        static void Second()
        {
            string a = "";
            string b = "";
            string c = "";
            Console.WriteLine($"a = {a = Console.ReadLine()} b = {b = Console.ReadLine()} c = {c = Console.ReadLine()}");
            double p = (double.Parse(a) + double.Parse(b) + double.Parse(c)) / 2;
            double s = Math.Sqrt(p * (p - double.Parse(a)) * (p - double.Parse(b)) * (p - double.Parse(c)));
            Console.WriteLine($"S = {s}");
        }
        static void Third()
        {
            string surname = Console.ReadLine();
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());
            string weight = Console.ReadLine();
            string hight = Console.ReadLine();
            /*Console.WriteLine($"Surname: {surname = Console.ReadLine()}");
            Console.WriteLine($"Name: {name = Console.ReadLine()}");
            Console.WriteLine($"Age: {age = Console.ReadLine()}");
            Console.WriteLine($"Weight: {weight = Console.ReadLine()}");
            Console.WriteLine($"Hight: {hight = Console.ReadLine()}");*/
            double bmi = double.Parse(weight) / double.Parse(hight) * double.Parse(hight);
            Console.WriteLine("Your profile:");
            Console.WriteLine($"Full Name: {surname}  {name}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Weight: {weight}");
            Console.WriteLine($"Hight: {hight}");
            Console.WriteLine($"Body Mass Index: {bmi}");

            if (bmi <= 18.5)
            {

            }
            else if (bmi <= 25)
            {

            }
            else if (bmi <= 30)
            {

            }
            else
            {

            }
            string d = age >= 14 ? "y" : "n"; 
        }
        static void Sdacha()
        {
            int money = int.Parse(Console.ReadLine());
            int[] kup = { 5000, 2000, 1000, 500, 200, 100, 50, 10, 5, 2, 1 };
            int[] kupCount = new int[kup.Length];


            for (int i = 0; i < kup.Length;)
            {
                if (money >= kup[i])
                {
                    money = money - kup[i];
                    kupCount[i]++;
                }
                else
                {
                    i++;
                }
            }
            for (int i = 0; i < kup.Length; i++)
            {
                if (kupCount[i] > 0)
                {
                    Console.WriteLine($"{kup[i]} : {kupCount[i]}");
                }
                else
                    continue;
            }
            Console.ReadKey();
        }
        static void Factorial()
        {
            int chislo = int.Parse(Console.ReadLine());
            int fact = 1;
            for (int i = 1; i <= chislo; i++)
            {
                fact = fact * i;
            }
            Console.WriteLine(fact);
            Console.ReadKey();
        }

        static void Enter() // Authentification!!!
        {
            string login = "johnsilver";
            string password = "qwerty";
            for (int i = 0; i <= 3;)
            {
                if (i == 3)
                {
                    Console.WriteLine("The number of available tries have been exceeded");
                    break;
                }
                else
                {
                    string userlogin = Console.ReadLine();
                    string userpassword = Console.ReadLine();
                    if (login == userlogin & password == userpassword) //&& !!!!
                    {
                        Console.WriteLine("Enter the System");
                        break;
                    }
                    else
                        i++;
                }
            }
        }

        static void EnterFather() // Authentification!!!
        {
            string login = "johnsilver";
            string password = "qwerty";
            bool auth = false;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Please enter the login");
                string userlogin = Console.ReadLine();
                Console.WriteLine("Please enter the passowrd");
                string userpassword = Console.ReadLine();

                if (login == userlogin && password == userpassword)
                {
                    auth = true;
                    break;
                }
                Console.WriteLine("login or password is incorrect");
            }
           
            if (!auth)
            {
                Console.WriteLine("The number of available tries have been exceeded");
                return;
            }
            Console.WriteLine("Enter the System");
        }

        static void Midle() // Average AVG
        {
            int[] numbers = new int[10];
            int count = 0;
            while (count < 10)
            {
                int number = int.Parse(Console.ReadLine());
                if (number == 0)
                    break;
                else
                {
                    numbers[count] = number;
                    count++;
                }
            }
            int sum = 0;
            int count_2 = 0; //
            foreach (int n in numbers)
            {
                if (n > 0 && n % 3 == 0)
                {
                    sum += n;
                    count_2++;
                }
            }
            double result = sum / count_2; // not working!!!
            //double result = (double)sum / count_2; // working fine!!!
            //result = 1.0 * sum / count_2; // working fine!!!
            
            Console.WriteLine("avg=" + result); // !!!!!
        }
        static void Fibonachi()
        {
            int kol_vo = 0;
            kol_vo = int.Parse(Console.ReadLine());
            int count = 3;
            int prevprev = 1;
            int prev = 1;
            int now = 0;
            if (kol_vo < 2)
            {
                Console.WriteLine(prevprev);
            }
            else
            {
                Console.Write($"{prevprev} {prev} ");
                if (kol_vo >= 3)
                {
                    while (count <= kol_vo)
                    {
                        now = prevprev + prev;
                        Console.Write($"{now} ");
                        prevprev = prev;
                        prev = now;
                        count++;
                    }
                    Console.WriteLine();
                }

            }
        }

        static void FibonachiFather()
        {
            int count = 0;
            count = int.Parse(Console.ReadLine());
            if (count <= 0)
            {
                Console.Write("count > 0");
                return;
            }
            int[] fibs = new int[count];
            fibs[0] = 1;
            if (count > 1)
                fibs[1] = 1;

            for (int i = 2; i < count; i++)
            {
                fibs[i] = fibs[i - 1] + fibs[i - 2];
            }
            for (int i = 0; i < count; i++)
            {
                Console.Write("{0} ", fibs[i]);
            }
        }
        static void Roman_numerals(string[] args)
        {
            string roman = Console.ReadLine();
            var list = new List<int>();

            var meaning = new Dictionary<char, int>()
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 },
            };

            for (int i = 0; i < roman.Length; i++)
            {
                int num = meaning[roman[i]];
                list.Add(num);
            }

            int result = 0;
            for (int i = 0; i < list.Count; i++)
            {
                bool isLast = i == list.Count - 1;
                if (isLast || list[i] >= list[i + 1])
                {
                    result += list[i];
                }
                else
                {
                    result -= list[i];
                }
            }
            Console.WriteLine(result);
        }
        
    }
}
