using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First
{
    public class Calculator
    {
        public double CulcTriangleSquare(double ab, double bc, double ac)
        {
            double p = 0.5 * (ab + bc + ac);

            return Math.Sqrt(p * (p - ab) * (p - bc) * (p - ac));
        }
        public double CulcTriangleSquare(double c, double h)
        {
            return 0.5 * c * h;
        }
        public double CulcTriangleSquare(double a, double b, int alpha)
        {
            double rads = alpha * Math.PI / 180;
            return 0.5 * a * b * Math.Sin(rads);
            
        }




    }
}
