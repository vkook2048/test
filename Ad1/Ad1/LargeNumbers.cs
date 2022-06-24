using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad1
{
    public class LargeNumbers
    {
        List<int> Number = new List<int> ();
        public LargeNumbers(string EnteredNumber)
        {
            for (int i = 0; i < EnteredNumber.Length; i++)
            {
                Number.Add(int.Parse(EnteredNumber[i].ToString()));
            }
        }

        private LargeNumbers(List<int> number)
        {
            Number = number;
        }



        public string ShowNumber()
        {
            string numberstr = "";
            foreach (var item in Number)
            {
                numberstr += item.ToString();
            }
            return numberstr;
        }

        public LargeNumbers Summation(LargeNumbers num2)
        {

            List<int> Numb1 = new List<int>(Number);
            List<int> Numb2 = new List<int>(num2.Number);
            List<int> sum = new List<int>();
            Numb1.Reverse();
            Numb2.Reverse();
            int[] Whois = new int[] { Numb1.Count, Numb2.Count };
            int remember = 0;

            for (int i = 0; i < Whois.Max(); i++)
            {
                int part1 = 0;
                int part2 = 0;
                int sumpart;
                if (i == Whois.Max() - 1)
                {
                    if (Numb2.Count > Numb1.Count)
                    {
                        part2 = Numb2[i];
                    }
                    else if (Numb1.Count > Numb2.Count)
                    {
                        part1 = Numb1[i];
                    }
                    else
                    {
                        part1 = Numb1[i];
                        part2 = Numb2[i];
                    }
                    // ==
                    sumpart = part1 + part2 + remember;
                }
                else
                {
                    if (i >= Whois.Min())
                    {
                        if (Numb2.Count > Numb1.Count)
                        {
                            part2 = Numb2[i];
                        }
                        else if (Numb1.Count > Numb2.Count)
                        {
                            part1 = Numb1[i];
                        }
                    }
                    else if (i < Whois.Min())
                    {
                        part1 = Numb1[i];
                        part2 = Numb2[i];
                    }
                    sumpart = part1 + part2 + remember;
                    remember = 0;
                    if (sumpart > 9)
                    {
                        remember = int.Parse(sumpart.ToString()[0].ToString());
                        sumpart -= 10;
                    }

                }
                sum.Add(sumpart);

            }
            sum.Reverse();
            while (sum[0] == 0 && sum.Count != 1)
            {
                for (int i = 0; i < sum.Count - 1; i++)
                {
                    if (sum[i] == 0 && sum[i + 1] != 0)
                    {
                        sum.RemoveAt(0);
                    }
                }
            }
                

            return new LargeNumbers(sum);
        }

        public LargeNumbers Multiplication(LargeNumbers num2)
        {
            List<int> Numb1 = Number;
            List<int> Numb2 = num2.Number;
            List<string> allresults = new List<string>();
            int rem = 1;            
            int[] Whois = new int[] { Numb1.Count, Numb2.Count };
            if (Numb1.Count > Numb2.Count)
                Numb2.Reverse();
            else
                Numb1.Reverse();

            for (int i = 0; i < Whois.Min(); i++)
            {
                int result = 0;
                if (Numb1.Count > Numb2.Count)
                {
                    string numb1 = "";
                    foreach (var item in Numb1)
                    {
                        numb1 += item.ToString();
                    }
                    result = Numb2[i] * int.Parse(numb1) * rem;
                    allresults.Add(result.ToString());
                }
                else
                {
                    string numb2 = "";
                    foreach (var item in Numb2)
                    {
                        numb2 += item.ToString();
                    }
                    result = Numb1[i] * int.Parse(numb2) * rem;
                    allresults.Add(result.ToString());
                }
                rem *= 10;
            }
            List<LargeNumbers> forallresults = new List<LargeNumbers>();
            for (int i = 0; i < allresults.Count; i++)
            {
                forallresults.Add(new LargeNumbers(allresults[i]));
            }
            var mult = new List<LargeNumbers>();
            if (forallresults.Count < 2)
                forallresults.Add(new LargeNumbers("0"));
            for (int i = 0; i < forallresults.Count; i++)
            {
                if (i == 0)
                {
                    mult.Add(forallresults[i].Summation(forallresults[i + 1]));
                }
                else if (i == 1)
                    continue;
                else
                {
                    var rememb = mult[0];
                    mult.RemoveAt(0);
                    mult.Add(forallresults[i].Summation(rememb));
                }
            }

            return mult[0];
        }

        public override string ToString()
        {
            return ShowNumber();
        }

    }
}
