using System;

namespace MathAnalyzerSpace
{
    public static class MathAnalyzer
    {
        public static bool IsNumber(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '(': return false;
                    case ')': return false;
                    case '+': return false;
                    case '-': return false;
                    case '*': return false;
                    case '/': return false;
                    default: break;
                }
            }
            return true;
        }

        public static bool IsNumber(char ch)
        {
            switch (ch)
            {
                case '+': return false;
                case '-': return false;
                case '*': return false;
                case '/': return false;
                case '(': return false;
                case ')': return false;
                default: return true;
            }
        }

        private static string CalculateStr(string str, char ch)
        {
            string[] meta = str.Split(new char[4] { '+', '-', '*', '/' });
            switch (ch)
            {
                case '+': return Convert.ToString(Convert.ToDouble(meta[0]) + Convert.ToDouble(meta[1]));
                case '-': return Convert.ToString(Convert.ToDouble(meta[0]) - Convert.ToDouble(meta[1]));
                case '*': return Convert.ToString(Convert.ToDouble(meta[0]) * Convert.ToDouble(meta[1]));
                case '/': return Convert.ToString(Convert.ToDouble(meta[0]) / Convert.ToDouble(meta[1]));
                default: break;
            }
            return str;
        }

        public static string SolutionNum(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '+': return CalculateStr(str, '+');
                    case '-': return CalculateStr(str, '-');
                    case '*': return CalculateStr(str, '*');
                    case '/': return CalculateStr(str, '/');
                    default: break;
                }
            }
            return str;
        }

        public static string Constructor(string str, int i)
        {
            int hooks = 0;

            for (int t = i - 1; t >= 0; t--)
            {
                if (t == 0)
                {
                    str = str.Insert(0, "(");
                    break;
                }
                switch (str[t])
                {
                    case '(': hooks--; break;
                    case ')': hooks++; break;
                    default: break;
                }
                if (!IsNumber(str[t]) && hooks == 0)
                {
                    str = str.Insert(t + 1, "(");
                    break;
                }
            }

            for (int t = i + 2; t < str.Length - 1; t++)
            {
                if (t == str.Length - 1)
                {
                    str = str.Insert(str.Length - 1, ")");
                    break;
                }
                switch (str[t])
                {
                    case '(': hooks++; break;
                    case ')': hooks--; break;
                    default: break;
                }
                if (!IsNumber(str[t]) && hooks == 0)
                {
                    str = str.Insert(t, ")");
                    break;
                }
            }
            return str;
        }

        private static bool GraphTest(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '*': return true;
                    case '/': return true;
                    default: break;
                }
            }
            return false;
        }

        public static string Delimiter(string str)
        {
            int counter = 0;
            bool state = true;
            bool answerGraphTest = GraphTest(str);

            for (int i = 0; i < str.Length; i++)
            {
                if (state && !IsNumber(str[i]) && !(str[i] == '(' || str[i] == ')'))
                {
                    if (++counter == 2)
                    {
                        state = false;
                        counter = 0;
                        i = -1;
                        continue;
                    }
                }
                else
                {
                    if (state && !IsNumber(str[i]) && (str[i] == '(' || str[i] == ')'))
                    {
                        counter = 0;
                    }
                }

                if (!state && answerGraphTest && (str[i] == '*' || str[i] == '/'))
                {
                    str = Constructor(str, i);
                    state = true;
                    i = -1;
                    continue;
                }
                if (!state && !answerGraphTest && (str[i] == '+' || str[i] == '-'))
                {
                    str = Constructor(str, i);
                    state = true;
                    i = -1;
                    continue;
                }
            }
            return str;
        }

        private static string LonelinessTest(string str)
        {
            int notNumber = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (!IsNumber(str[i]))
                {
                    if (++notNumber == 2)
                    {
                        return str;
                    }
                }
            }
            return SolutionNum(str);
        }

        public static string Solution(string str)
        {
            int start = 0;
            bool stOfSo = false;
            bool completed;

            while (!IsNumber(str))
            {
                completed = false;
                str = Delimiter(str);
                str = LonelinessTest(str);

                for (int i = 0; i < str.Length; i++)
                {
                    switch (str[i])// break
                    {
                        case '(':
                            start = i;
                            stOfSo = true;
                            break;
                        case ')':
                            if (stOfSo)
                            {
                                str = str.Substring(0, start) + SolutionNum(str.Substring(start + 1, i - start - 1)) + str.Substring(i + 1);
                                stOfSo = false;
                                completed = true;
                            }
                            break;
                        default: break;
                    }
                    if (completed)
                    {
                        break;
                    }
                }
            }
            return str;
        }
    }
}
