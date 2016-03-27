using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cal_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Number n1 = new Number(1, 2), n2 = new Number(1, 4);
            Number n3 = new Number(n1 + n2);
            Number n4 = new Number(n1 - n2);
            Number n5 = new Number(n1 * n2);
            Number n6 = new Number(n1 / n2);
            Console.WriteLine("n1: {0} / {1}", n1.getNumerator, n1.getDenomintaor);
            Console.WriteLine("n2: {0} / {1}", n2.getNumerator, n2.getDenomintaor);
            Console.WriteLine("n3: {0} / {1}", n3.getNumerator, n3.getDenomintaor);
            Console.WriteLine("n4: {0} / {1}", n4.getNumerator, n4.getDenomintaor);
            Console.WriteLine("n5: {0} / {1}", n5.getNumerator, n5.getDenomintaor);
            Console.WriteLine("n6: {0} / {1}", n6.getNumerator, n6.getDenomintaor);
            Number n7 = new Number(2, 3);
            Console.WriteLine("n7: {0}, {1}, {2}", n7.toString(), n7.toString(true), n7.toString(5));
            String str = "-5";
            Console.WriteLine("{0}", Convert.ToDouble(str));
            Console.WriteLine("now enter: ");
            */
            while(true)
            {
                String tmp = Console.ReadLine();
                bool a, b;
                Console.WriteLine("   {0}", Calculator.nifixToPost(tmp, out a, out b));
                Console.WriteLine("   {0}", Calculator.postCalculate(Calculator.nifixToPost(tmp, out a, out b), a, b, true, 8));
                Console.WriteLine("   {0}", Calculator.postCalculate(Calculator.nifixToPost(tmp, out a, out b), a, b, false, 0));
            }
        }
    }
}
