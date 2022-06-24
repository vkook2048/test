using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad1
{
    class Program
    {
        // Структура
        // Класс (например точка)
        // private public static
        // Разница (функция которая принимает класс/структуру, меняет его внутри)
        // Конструктор класса
        // Функция расстояния между двумя точками
        // перенести функцию в класс

        // Наследование
        // Фигура (четырехугольник, прямоугольник квадрат)
        // Конструктор (перегрузка)
        // перегрузка функций, виртуальные функции
        // protected virtual override

        // Интерфейсы

        // тесты        
        // Большое целое число
        // сложение 
        // вычитание
        // умножение
        // деление
        // перегрузка операторов

        struct MyPointS
        {
            private double _x;

            public double GetX()
            {
                return _x;
            }

            public void SetX(double x)
            {
                // TODO something
                _x = x;
            }

            // property
            public double X
            {
                get
                {
                    return _x;
                }
                set
                {
                    // TODO something
                    _x = value;
                }
            }

            // auto property
            public double Y { get; set; }

            public override string ToString()
            {
                return "x=" + X + " Y=" + Y;
            }
        }

        class MyPointC
        {
            public static double Val = 10;

            public MyPointC()
            {

            }

            public MyPointC(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X { get; set; }
            public double Y { get; set; }

            public override string ToString()
            {
                return "x=" + X + " Y=" + Y;
            }

            public void Set0()
            {
                X = 0;
                Y = 0;
            }
            public double Distance(MyPointC p2)
            {
                return Math.Sqrt(Math.Pow(p2.X - X, 2) + Math.Pow(p2.Y - Y, 2));                
            }

        }

        static void SomeS(MyPointS p, int val)
        {
            p.X = 0;
            p.Y = 0;
            val = 0;
        }

        static void SomeC(MyPointC p, int val)
        {
            p.X = 0;
            p.Y = 0;
            val = 0;
        }

         static double Distance(MyPointC p1, MyPointC p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2 ) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private static void Tets()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                int p1 = rnd.Next(0, int.MaxValue / 2);
                int p2 = rnd.Next(0, int.MaxValue / 2);

                LargeNumbers l1 = new LargeNumbers(p1.ToString());
                LargeNumbers l2 = new LargeNumbers(p2.ToString());

                string summ = l1.Summation(l2).ShowNumber();

                if (summ != (p1 + p2).ToString())
                    throw new Exception("!!!!");

            }

        }


        static void Main(string[] args)
        {
            LargeNumbers l1 = new LargeNumbers("1");
            LargeNumbers l2 = new LargeNumbers("42");
            Console.WriteLine(l1.ShowNumber());
            Console.WriteLine(l2.ShowNumber());
            Console.WriteLine(l1.Multiplication(l2).ShowNumber());

            Console.ReadLine();

           //LargeNumbers l1 = new LargeNumbers("804149198");
           //LargeNumbers l2 = new LargeNumbers("194892921");

            //LargeNumbers l1 = new LargeNumbers("9");
            //LargeNumbers l2 = new LargeNumbers("9");
            //Trace.WriteLine("start");

            //string summ = l1.Summation(l2).ShowNumber();
            //Console.WriteLine(summ);

            Trace.WriteLine("start");
            Tets();

            LargeNumbers num1 = new LargeNumbers(Console.ReadLine());
            LargeNumbers num2 = new LargeNumbers(Console.ReadLine());
            Console.WriteLine(num1.ShowNumber());
            Console.WriteLine(num2.ShowNumber());
            Console.WriteLine(num1.Summation(num2).ShowNumber());

            Console.ReadLine();

            MyPointS p = new MyPointS();
            p.X = 10;
            p.Y = 10;
            Console.WriteLine(p);

            MyPointC.Val = 15;

            MyPointC p2 = new MyPointC(y: 0, x: 0);
            Console.WriteLine(p2);

            MyPointC p3 = new MyPointC(y: 3, x: 4);
            Console.WriteLine(p3);
            //p3.Set0();
            //Console.WriteLine("set0 " + p3);

            int val1 = 10;
            int val2 = 10;

            SomeS(p, val1);
            SomeC(p2, val2);

            Console.WriteLine(val1);
            Console.WriteLine(val2);

            Console.WriteLine(p);
            Console.WriteLine(p2);

            double dist1 = Distance(p2, p3);

            Console.WriteLine(dist1);

            Console.WriteLine(p2.Distance(p3));

            Console.WriteLine();

            MyPoint A = new MyPoint(1, 5);

            Rectangle rect1 = new Rectangle(A, 5, 3);
            Console.WriteLine(rect1.Perimetr());

            Console.ReadKey();
        }
    }
}
