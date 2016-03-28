using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Calculate
    {
        private static bool isOpt(char x)
        {
            return (x == '+' || x == '-' || x == '*' || x == '/' || x == '(' || x == ')');
        }

        private static bool isNum(char x)
        {
            return (x >= '0' && x <= '9');
        }

        private static int priority(char x)
        {
            if (x == '-' || x == '+')
                return 1;
            else if (x == '*' || x == '/')
                return 2;
            else if (x == '(')
                return 0;
            else
                return -1;
        }

        private static String infixToPost(String infixString, out bool hasDemical, out bool error)
        {
            infixString = infixString.Replace(" ", "");
            Stack<char> s = new Stack<char>();
            bool hasNum = false;
            bool hasDemicalDuringScan = false;
            bool minusFlag = false;
            long demicalDigitsNum = 1;
            long tmpNum = 0;

            String res = "";
            hasDemical = false;
            error = false;

            for (int i = 0; i < infixString.Length; i++)
            {
                if (i == 0)
                {
                    if (infixString[i] == '-')
                    {
                        minusFlag = true;
                        i++;
                    }
                    else if (infixString[i] == '(') ;
                    else if (isOpt(infixString[i]))
                    {
                        error = true;
                        return "ERROR-Expression begin with wrong character!";
                    }
                }
                if (isNum(infixString[i]))
                {
                    hasNum = true;
                    tmpNum = tmpNum * 10 + (infixString[i] - '0');
                    if (hasDemicalDuringScan)
                        demicalDigitsNum *= 10;
                }
                else if (isOpt(infixString[i]))
                {
                    if (i > 0)
                    {
                        if (infixString[i] == '-' && infixString[i - 1] == '(') ;
                        else if (infixString[i] == '(' && isOpt(infixString[i - 1]) && infixString[i - 1] != ')') ;
                        else if (infixString[i - 1] == ')') ;
                        else if (isNum(infixString[i - 1])) ;
                        else
                        {
                            error = true;
                            return "ERROR-Two operators next to each other illegally!";
                        }
                    }
                    if (hasNum)
                    {
                        hasNum = false;
                        //Console.WriteLine("minusFlag = {0}, tmpNum = {1}, hasDemicalDuringScan = {2}, {3}", minusFlag, tmpNum, hasDemicalDuringScan, tmpNum+'0');
                        if (minusFlag)
                        {
                            tmpNum = -tmpNum;
                            minusFlag = false;
                        }
                        res += tmpNum;
                        res += ' ';
                        tmpNum = 0;
                        if (hasDemicalDuringScan)
                        {
                            if (demicalDigitsNum == 1)
                            {
                                error = true;
                                return "ERROR-Digits absent after radix point!-1";
                            }
                            hasDemical = true;
                            res += demicalDigitsNum;
                            res += ' ';
                            demicalDigitsNum = 1;
                            hasDemicalDuringScan = false;
                            res += "/ ";
                        }
                    }
                    if (infixString[i] == '(')
                    {
                        s.Push(infixString[i]);
                        if (i + 1 < infixString.Length && infixString[i + 1] == '-')
                        {
                            minusFlag = true;
                            i++;
                        }
                    }
                    else if (infixString[i] == ')')
                    {
                        if (s.Count == 0)
                        {
                            error = true;
                            return "ERROR-Parentheses unmatched!-1";
                        }
                        char tmpOperator;
                        while (s.Count > 0 && (tmpOperator = s.Pop()) != '(')
                        {
                            res += tmpOperator;
                            res += ' ';
                            if (s.Count == 0)
                            {
                                error = true;
                                return "ERROR-Parentheses unmatched!-2";
                            }
                        }
                    }
                    else
                    {
                        char tmpTop = ' ';
                        bool flag = false;
                        while (true)
                        {
                            if (s.Count == 0)
                                break;
                            tmpTop = s.Pop();
                            if (priority(infixString[i]) > priority(tmpTop))
                            {
                                flag = true;
                                break;
                            }
                            res += tmpTop;
                            res += ' ';
                        }
                        if (flag)
                            s.Push(tmpTop);
                        s.Push(infixString[i]);
                    }
                }
                else if (infixString[i] == '.')
                {
                    if (hasDemicalDuringScan || !hasNum)
                    {
                        error = true;
                        return "ERROR-Too more radix point or has no digits before radix point!";
                    }
                    else
                        hasDemicalDuringScan = true;
                }
                else
                {
                    error = true;
                    return "ERROR-Illegal character!";
                }
            }
            if (hasNum)
            {
                hasNum = false;
                if (minusFlag)
                {
                    tmpNum = -tmpNum;
                    minusFlag = false;
                }
                res += tmpNum;
                res += ' ';
                tmpNum = 0;
                if (hasDemicalDuringScan)
                {
                    if (demicalDigitsNum == 1)
                    {
                        error = true;
                        return "ERROR-Digits absent after radix point!-1";
                    }
                    hasDemical = true;
                    res += demicalDigitsNum;
                    res += ' ';
                    demicalDigitsNum = 1;
                    hasDemicalDuringScan = false;
                    res += "/ ";
                }
            }
            while (s.Count > 0)
            {
                char tmpPop = s.Pop();
                if (tmpPop == '(')
                {
                    error = true;
                    return "ERROR-Parentheses unmatched!-3";
                }
                res += tmpPop;
                res += ' ';
            }
            return res.Substring(0, res.Length - 1);
        }

        private static String postCalculate(String postString, bool hasDemical, bool error, bool hasPrecision, int precision)
        {
            Stack<Number> s = new Stack<Number>();
            if (postString.Length >= 5 && postString.Substring(0, 5).Equals("ERROR"))
                return postString;
            if (error)
                return "ERROR";

            String[] sArray = postString.Split(' ');
            foreach (String i in sArray)
            {
                if (i.Length == 1 && isOpt(i[0]))
                {
                    Number b = new Number(s.Pop());
                    Number a = new Number(s.Pop());
                    switch (i[0])
                    {
                        case '+': s.Push(new Number(a + b)); break;
                        case '-': s.Push(new Number(a - b)); break;
                        case '*': s.Push(new Number(a * b)); break;
                        case '/': if (b.getNumerator == 0) return "ERROR-Divide error!"; s.Push(new Number(a / b)); break;
                        default: return "ERROR-Illegal character";
                    }
                }
                else
                    s.Push(new Number(Convert.ToInt64(i)));
            }
            if (s.Count > 1)
                return "ERROR";
            if (hasPrecision)
                return s.Pop().toString(precision);
            else if (hasDemical)
                return s.Pop().toString(hasDemical);
            else
                return s.Pop().toString();
        }
        /*
         * 传入两个参数分别为中缀表达式以及小数位保留的位数precision
         * precisi<=0意味着用户未强制保留小数位，程序由计算结果自动输出（分数或小数）
         */
        public static String startCalculate(String infixString, int precision)
        {
            bool hasDemical, error, hasPrecision;
            if (precision <= 0)
                hasPrecision = false;
            else
                hasPrecision = true;
            return postCalculate(infixToPost(infixString, out hasDemical, out error), hasDemical, error, hasPrecision, precision);
        }
    }
}
