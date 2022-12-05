using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public abstract class Expression
    {
        private class Context
        {
            public Dictionary<string, Expression> dict = new Dictionary<string, Expression>();
        }

        public static Expression Parse(string strexp)
        {
            return Parse(strexp, new Context());
        }

        private static Expression Parse(string strexp, Context context)
        {
            strexp = strexp.Replace(" ", "");

            if (strexp.IndexOf("(") >= 0 || strexp.IndexOf(")") >= 0)
            {
                int indx = 0;
                int endindx = 0;
                for (int i = 0; i < strexp.Length; i++)
                {
                    if (strexp[i] == '(')
                    {
                        indx++;
                    }
                    else if (strexp[i] == ')')
                    {
                        indx--;
                        if (indx == 0)
                        {
                            endindx = i;
                            break;
                        }
                        if (indx < 0)
                        {
                            throw new Exception("Скобки расставлены неправильно");
                        }
                    }
                }
                if (indx > 0)
                {
                    throw new Exception("Количество открытых скобок превышает количество закрытых");
                }
                else if (indx < 0)
                {
                    throw new Exception("Количество закрытых скобок превышает количество открытых");
                }

                string strIndex = "exp" + (context.dict.Count + 1);
                int startindx = strexp.IndexOf('(') + 1;
                int lenght = endindx - startindx;
                string str = strexp.Substring(startindx, lenght);
                string repl = '(' + str + ')';
                string temp = strexp.Replace(repl, strIndex);
                context.dict.Add(strIndex, Expression.Parse(str));
                return Parse(temp, context);
            }

            if (strexp.IndexOf("+") >= 0 || strexp.IndexOf("-") >= 0)
            {
                
                var strs = strexp.Split('+','-');
                Expression exp = new SumExpression();
                int signIndex = -1;
                for (int i = 0; i < strs.Length; i++)
                {
                    if (strs[i].Length == 0)
                    {
                        exp.Expressions.Add(new NumberExpression());
                    }
                    else
                    {
                        var child = Expression.Parse(strs[i], context);
                        if (signIndex >= 0)
                            child.Sign = strexp[signIndex];
                        exp.Expressions.Add(child);
                    }
                    signIndex += strs[i].Length + 1;             
                }               
                return exp;
            }

            if (strexp.IndexOf("*") >= 0 || strexp.IndexOf("/") >= 0)
            {
                var strs = strexp.Split('*', '/');
                Expression exp = new MultyExpression();
                int signIndex = -1;
                for (int i = 0; i < strs.Length; i++)
                {
                    var child = Expression.Parse(strs[i], context);
                    if (signIndex >= 0)
                        child.Sign = strexp[signIndex];
                    exp.Expressions.Add(child);
                    signIndex += strs[i].Length + 1;
                }
                return exp;
            }

            if (strexp.IndexOf("^") >= 0)
            {
                var strs = strexp.Split('^');
                Expression exp = new PowerExpression();
                int signIndex = -1;
                for (int i = 0; i < strs.Length; i++)
                {
                    var child = Expression.Parse(strs[i], context);
                    if (signIndex >= 0)
                        child.Sign = strexp[signIndex];
                    exp.Expressions.Add(child);
                    signIndex += strs[i].Length + 1;
                }
                return exp;
            }

            if (context.dict.ContainsKey(strexp))
                return context.dict[strexp];
            if (strexp.Length == 0)
            {
                throw new Exception("Пустое значение после/перед знаком");
            }

            double d = 0;
            if (!double.TryParse(strexp, out d))
            {
                throw new Exception("Введены непонятные данные: " + strexp);
            }
            else
            {
                return new NumberExpression() { value = d };
            }
        }

        public abstract double Calculate();
        public char Sign;
        public List<Expression> Expressions { get; set; } = new List<Expression>();
    }

    public class NumberExpression: Expression
    {
        public double value;
        public override double Calculate()
        {
            return value;
        }
    }

    public class SumExpression : Expression
    {
        public override double Calculate()
        {
            double sum = 0;
            foreach (var item in Expressions)
            {
                if (item.Sign == '-')
                {
                    sum -= item.Calculate();
                }
                else
                {
                    sum += item.Calculate();
                }
            }
            return sum;
        }
    }

    public class MultyExpression : Expression
    {
        public override double Calculate()
        {
            double mult = 1;
            foreach (var item in Expressions)
            {
                if (item.Sign == '/')
                {
                    mult /= item.Calculate();
                }
                else
                {
                    mult *= item.Calculate();
                }
            }
            return mult;
        }
    }

    public class PowerExpression : Expression
    {
        public override double Calculate()
        {
            double pow = 1;
            foreach (var item in Expressions)
            {
                if (item.Sign == '^')
                {
                    pow = Math.Pow(pow, item.Calculate());
                }
                else
                {
                    pow *= item.Calculate();
                }
            }
            return pow;
        }
    }


    /*public class WrapperExpression : Expression
    {
        public override double Calculate()
        {
            return Expressions[0].Calculate();
        }
    }*/

}
