using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad1
{
    class MyPoint
    {
        public static double Val = 10;

        public MyPoint()
        {

        }

        public MyPoint(double x, double y)
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
        public double Distance(MyPoint p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - X, 2) + Math.Pow(p2.Y - Y, 2));
        }
    }
    class Quadrilateral
    {
        public const int PointsCount = 4;

        public MyPoint[] Points { get; set; } = new MyPoint[PointsCount] { new MyPoint(), new MyPoint(), new MyPoint(), new MyPoint() };

        public Quadrilateral(MyPoint[] Vertices)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i].X = Vertices[i].X;
                Points[i].Y = Vertices[i].Y;
            }
        }
        public Quadrilateral(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            Points[0].X = x1;
            Points[0].Y = y1;
            Points[1].X = x2;
            Points[1].Y = y2;
            Points[2].X = x3;
            Points[2].Y = y3;
            Points[3].X = x4;
            Points[3].Y = y4;
        }
        public Quadrilateral()
        {

        }

        public double Perimetr()
        {
            double per = 0;
            for (int i = 0; i < Points.Length; i++)
            {
                if (i < Points.Length - 1)
                {
                    per += Points[i].Distance(Points[i + 1]);
                }
                else
                    per += Points[i].Distance(Points[0]);

            }
            return per;
        }

    }
    class Rectangle : Quadrilateral
    {
        public Rectangle(MyPoint start, double width, double heigth)
           : base(start.X, start.Y, start.X + width, start.Y, start.X + width, start.Y - heigth, start.X, start.Y - heigth)
        {

        }
    }

   /* class Square : Rectangle
    {

    }*/
}
