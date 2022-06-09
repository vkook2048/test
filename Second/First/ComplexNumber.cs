using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First
{
    class ComplexNumber
    {
        public double Real { get; private set; }
        public double Imaginary { get; private set; }

        public ComplexNumber(double a, double b)
        {
            Real = a;
            Imaginary = b;
        }

        private ComplexNumber()
        {
        }

        public ComplexNumber Multy(ComplexNumber other)
        {
            var complex = new ComplexNumber();
            complex.Real = other.Real * Real - other.Imaginary * Imaginary;
            complex.Imaginary = other.Real * Imaginary + other.Imaginary * Real;
            return complex;
        }

        public ComplexNumber Plus(ComplexNumber other)
        {
            var complex = new ComplexNumber();
            complex.Real = other.Real + Real;
            complex.Imaginary = other.Imaginary + Imaginary;
            return complex;
            
            //return new ComplexNumber(other.real + real, other.imaginary + imaginary);
        }

        public ComplexNumber Minus(ComplexNumber other)
        {
            var complex = new ComplexNumber();
            complex.Real = Real - other.Real;
            complex.Imaginary = Imaginary - other.Imaginary;
            return complex;
        }


        public ComplexNumber Plus2(ComplexNumber other)
        {
            Real += other.Real;
            Imaginary += other.Imaginary;
            return this;
        }

        public ComplexNumber Minus2(ComplexNumber other)
        {
            Real -= other.Real;
            Imaginary -= other.Imaginary;
            return this;
        }
        
    }
}
