using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roman
{
    public class RomanNumberExtend : RomanNumber
    {
        public RomanNumberExtend(string num) : base(1)
        {
            romanNum = 0;
            var symb = new Dictionary<char, int>()
            {
                {'I', 1 }, {'V', 5 }, {'X', 10 }, {'L', 50 },
                {'C', 100 }, {'D', 500 }, {'M', 1000 }
            };

            checkFormat(num);

            for (int i = 0; i < num.Length - 1; i++)
            {
                if (symb[num[i]] >= symb[num[i + 1]])
                    romanNum += (ushort)symb[num[i]];
                else
                    romanNum -= (ushort)symb[num[i]];
            }
            romanNum += (ushort)symb[num[num.Length - 1]];
            Console.WriteLine(romanNum);
        }

        private void checkFormat(string rn)
        {
            int deltaPos = 0, pos = 0, prevPos = 0;
            string symb = "IVXLCDM";
            var symbDict = new Dictionary<char, int>()
            {
                {'I', 0 }, {'V', 0 }, {'X',0 }, {'L', 0 },
                {'C', 0 }, {'D', 0 }, {'M', 0 }
            };
            for (int i = 0; i < rn.Length; i++)
                if (!symbDict.ContainsKey(rn[i]))
                    throw new RomanNumberException("Неверный символ в римском числе");
            if (symb.IndexOf(rn[0]) % 2 == 0)
                symbDict[rn[0]]++;
            else
                symbDict[rn[0]] += 3;

            for (int i = 1; i < rn.Length; i++)
            {
                if (symbDict[rn[i]] == 3)
                    throw new RomanNumberException("Неверный формат");
                pos = symb.IndexOf(rn[i]);
                prevPos = symb.IndexOf(rn[i - 1]);
                deltaPos = pos - prevPos;
                if (deltaPos == 0 && pos % 2 == 0) symbDict[rn[i]]++;      //II
                else if (symbDict[rn[i - 1]] == 1 && (deltaPos == 1 || deltaPos == 2) && prevPos % 2 == 0)  //IV IX
                {
                    symbDict[rn[i - 1]] = 3;
                    symbDict[rn[i]] = 3;
                }
                else if (symbDict[rn[i - 1]] == 1 && (deltaPos == 1 || deltaPos == 2) && prevPos % 2 == 1)  //VL
                    throw new RomanNumberException("Неверный формат");
                else if (deltaPos > 2 || (symbDict[rn[i - 1]] > 1 && (deltaPos == 1 || deltaPos == 2)))     //IM VX
                    throw new RomanNumberException("Неверный формат");
                else if ((deltaPos < 0) && pos % 2 == 0) symbDict[rn[i]]++;     //VI XI
                else if ((deltaPos < 0) && pos % 2 == 1) symbDict[rn[i]] += 3;


            }
        }

    }
}
