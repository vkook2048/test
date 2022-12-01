using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveApp
{  
    class Hex
    {
        private Hex()
        {

        }
        private static Hex _instance;

        public static Hex GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Hex();
            }
            return _instance;
        }

        public  string ToHexString(byte[] Bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Bytes)
            {
                sb.Append(ToHexademical(item));
            }
            return sb.ToString();
        }

        public  byte[] FromHexString(string InputString)
        {
            string[] numbers = new string[InputString.Length / 2];
            int count = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] += InputString[count];
                count++;
                numbers[i] += InputString[count];
                count++;
            }
            byte[] output = new byte[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                output[i] = FromHexademical(numbers[i]);
            }
            return output;
        }

        private  string ToHexademical(byte item)
        {
            int number = Convert.ToInt32(item);
            string result = "";
            int[] array = new int[] { number / 16, number % 16 };
            for (int i = 0; i < array.Length; i++)
            {
                result += NumbToLetter(array[i]);
            }

            return result;
        }
        private  string NumbToLetter(int numb)
        {
            if (numb == 10)
            {
                return "a";
            }
            else if (numb == 11)
            {
                return "b";
            }
            else if (numb == 12)
            {
                return "c";
            }
            else if (numb == 13)
            {
                return "d";
            }
            else if (numb == 14)
            {
                return "e";
            }
            else if (numb == 15)
            {
                return "f";
            }
            else
            {
                return numb.ToString();
            }
        }
        private  byte FromHexademical(string str)
        {
            int numb = 0;
            int help = 16;
            foreach (var charr in str)
            {
                numb += ToInt(charr) * help;
                help = 1;
            }
            return Convert.ToByte(numb);
        }
        private  int ToInt(char charr)
        {
            if (charr == 'a')
            {
                return 10;
            }
            else if (charr == 'b')
            {
                return 11;
            }
            else if (charr == 'c')
            {
                return 12;
            }
            else if (charr == 'd')
            {
                return 13;
            }
            else if (charr == 'e')
            {
                return 14;
            }
            else if (charr == 'f')
            {
                return 15;
            }
            else
            {
                return int.Parse(charr.ToString());
            }
        }
    }
}
