using System;

namespace Roman
{
    public class RomanNumber : ICloneable, IComparable
    {
        protected ushort romanNum;
        public object Clone()
        {
            return new RomanNumber(romanNum);
        }
        public int CompareTo(object? obj)
        {
            if (obj is RomanNumber rom) return romanNum - ((RomanNumber)obj).romanNum;
            throw new RomanNumberException("Некоректное значение параметра");
        }
        private static void checkForNull(ref RomanNumber? n1, ref RomanNumber? n2)
        {
            if (n1 == null)
                throw new RomanNumberException("Ошибка: n1 = null");
            if (n2 == null)
                throw new RomanNumberException("Ошибка: n2 = null");
        }
        public RomanNumber(ushort n)
        {
            if (n <= 0)
                throw new RomanNumberException("Ошибка: число меньше либо равно нулю");
            if (n > 3999)
                throw new RomanNumberException("Ошибка: число больше 3999");
            romanNum = n;
        }

        public static RomanNumber Add(RomanNumber? n1, RomanNumber? n2)
        {
            checkForNull(ref n1, ref n2);
            if (n1.romanNum + n2.romanNum > 3999)
                throw new RomanNumberException("Ошибка: n1 + n2 - превышает 3999");
            return new RomanNumber((ushort)(n1.romanNum + n2.romanNum));
        }
        public static RomanNumber operator +(RomanNumber? n1, RomanNumber? n2)
        {
            return Add(n1, n2);
        }

        public static RomanNumber Sub(RomanNumber? n1, RomanNumber? n2)
        {
            checkForNull(ref n1, ref n2);
            if (n1.romanNum <= n2.romanNum)
                throw new RomanNumberException("Ошибка: n1 - n2 - меньше либо равно нулю");
            return new RomanNumber((ushort)(n1.romanNum - n2.romanNum));
        }

        public static RomanNumber operator -(RomanNumber? n1, RomanNumber? n2)
        {
            return Sub(n1, n2);
        }

        public static RomanNumber Mul(RomanNumber? n1, RomanNumber? n2)
        {
            checkForNull(ref n1, ref n2);
            if (n1.romanNum * n2.romanNum > 3999)
                throw new RomanNumberException("Ошибка: n1 * n2 - превышает 3999");
            return new RomanNumber((ushort)(n1.romanNum * n2.romanNum));
        }

        public static RomanNumber operator *(RomanNumber? n1, RomanNumber? n2)
        {
            return Mul(n1, n2);
        }

        public static RomanNumber Div(RomanNumber? n1, RomanNumber? n2)
        {
            checkForNull(ref n1, ref n2);
            if (n2.romanNum <= 0)
                throw new RomanNumberException("Ошибка: n2 - меньше либо равен нулю");
            if (n1.romanNum / n2.romanNum == 0)
                throw new RomanNumberException("Ошибка: n1 / n2 - равно нулю");
            return new RomanNumber((ushort)(n1.romanNum / n2.romanNum));
        }

        public static RomanNumber operator /(RomanNumber? n1, RomanNumber? n2)
        {
            return Div(n1, n2);
        }

        public override string ToString()
        {
            string symb = "IVXLCDM";
            string answer = "";
            int digit, tmpRn = romanNum, degreesOfTen = 1000;
            for (int i = 3; i >= 0; i--)
            {
                digit = tmpRn / degreesOfTen;
                if (digit != 0)
                {
                    if (digit <= 3)
                        for (int j = 0; j < digit; j++)
                            answer += symb[i * 2];
                    else if (digit == 4)
                    {
                        answer += symb[i * 2];
                        answer += symb[i * 2 + 1];
                    }
                    else if (digit == 9)
                    {
                        answer += symb[i * 2];
                        answer += symb[i * 2 + 2];
                    }
                    else
                    {
                        answer += symb[i * 2 + 1];
                        for (int j = 0; j < digit - 5; j++)
                            answer += symb[i * 2];
                    }

                    tmpRn %= degreesOfTen;
                }
                degreesOfTen /= 10;
            }
            return answer;
        }

    }
}