using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cal_cmd
{
    class Number
    {
        private long numerator, denominator;
        public long getNumerator
        {
            get { return numerator; }
        }
        public long getDenomintaor
        {
            get { return denominator; }
        }

        public Number()
        {
            numerator = 0;
            denominator = 1;
        }

        public Number(long numerator)
        {
            this.numerator = numerator;
            denominator = 1;
        }

        public Number(long numerator, long denominator)
        {
            long gcd = findGCD(numerator, denominator);
            this.numerator = numerator / gcd;
            this.denominator = denominator / gcd;
        }

        public Number(Number num)
        {
            long gcd = findGCD(num.getNumerator, num.getDenomintaor);
            this.numerator = num.getNumerator / gcd;
            this.denominator = num.getDenomintaor / gcd;
        }

        private static long findGCD(long numOne, long numTwo)
        {
            if (numOne == 0 && numTwo == 0)
                return 1;

            long gcd = numOne % numTwo;
            while (gcd > 0)
            {
                numOne = numTwo;
                numTwo = gcd;
                gcd = numOne % numTwo;
            }
            return numTwo;
        }

        public static Number operator + (Number numOne, Number numTwo)
        {
            long newNumerator, newDenominator;
            newNumerator = numOne.getNumerator * numTwo.getDenomintaor + numTwo.getNumerator * numOne.getDenomintaor;
            newDenominator = numOne.getDenomintaor * numTwo.getDenomintaor;
            long gcd = findGCD(newNumerator, newDenominator);
            newNumerator /= gcd;
            newDenominator /= gcd;
            Number res = new Number(newNumerator, newDenominator);
            return res;
        }

        public static Number operator - (Number numOne, Number numTwo)
        {
            long newNumerator, newDenominator;
            newNumerator = numOne.getNumerator * numTwo.getDenomintaor - numTwo.getNumerator * numOne.getDenomintaor;
            newDenominator = numOne.getDenomintaor * numTwo.getDenomintaor;
            long gcd = findGCD(newNumerator, newDenominator);
            newNumerator /= gcd;
            newDenominator /= gcd;
            Number res = new Number(newNumerator, newDenominator);
            return res;
        }

        public static Number operator * (Number numOne, Number numTwo)
        {
            long newNumerator, newDenominator;
            newNumerator = numOne.getNumerator * numTwo.getNumerator;
            newDenominator = numOne.getDenomintaor * numTwo.getDenomintaor;
            long gcd = findGCD(newNumerator, newDenominator);
            newNumerator /= gcd;
            newDenominator /= gcd;
            Number res = new Number(newNumerator, newDenominator);
            return res;
        }

        public static Number operator  / (Number numOne, Number numTwo)
        {
            long newNumerator, newDenominator;
            newNumerator = numOne.getNumerator * numTwo.getDenomintaor;
            newDenominator = numOne.getDenomintaor * numTwo.getNumerator;
            long gcd = findGCD(newNumerator, newDenominator);
            newNumerator /= gcd;
            newDenominator /= gcd;
            Number res = new Number(newNumerator, newDenominator);
            return res;
        }

        public String toString()
        {
            if (numerator == 0)
                return Convert.ToString(0);
            else if (denominator != 1)
                return numerator + "/" + denominator;
            else
                return Convert.ToString(numerator);
        }

        public String toString(bool hasDemical)
        {
            double res = Convert.ToDouble(numerator) / Convert.ToDouble(denominator);
            return Convert.ToString(res);
        }

        public String toString(int precision)
        {
            double res = Convert.ToDouble(numerator) / Convert.ToDouble(denominator);
            res = Math.Round(res, precision);
            String tmpRes = Convert.ToString(res);
            int pos = tmpRes.LastIndexOf('.');
            if (pos != -1)
                tmpRes = tmpRes.PadRight(100, '0');
            else
            {
                tmpRes += ".";
                tmpRes = tmpRes.PadRight(100, '0');
            }
            pos = tmpRes.LastIndexOf('.');
            return tmpRes.Substring(0, pos + precision + 1);
        }
    }
}
