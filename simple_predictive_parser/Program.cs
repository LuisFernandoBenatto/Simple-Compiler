using System;

namespace simple_predictive_parser
{
    class Program
    {
        public static string[] vars = {'E', 'X', 'T', 'Y', 'F'};
        public enum Token
        {
            vars0 = 'E',
            vars1 = 'X',
            vars2 = 'T',
            vars3 = 'Y',
            vars4 = 'F',
            ID = 'i',
            LP = '(',
            RP = ')',
            SUM = '+',
            SUB = '-',
            MUL = '*',
            DIV = '/',
            EOF = '$'
        }
        public char nextToken(string input, int pos)
        {
            if (input.Length > pos)
            {
                return input[pos++];
            }
            else
            {
                return ((char)Token.EOF);
            }
        }
        public string predTable(char VR, char tk)
        {
            if (VR == ((char)Token.vars0))
            {
                if (tk == ((char)Token.ID) || tk == ((char)Token.LP))
                {
                    return "TX";
                }
            }
            if (VR == ((char)Token.vars1))
            {
                if (tk == ((char)Token.SUM))
                {
                    return "+TX";
                }
                if (tk == ((char)Token.RP) || tk == ((char)Token.EOF))
                {
                    return "";
                }
            }
            if (VR == ((char)Token.vars2))
            {
                if (tk == ((char)Token.ID) || tk == ((char)Token.LP))
                {
                    return "FY";
                }
            }
            if (VR == ((char)Token.vars3))
            {
                if (tk == ((char)Token.MUL))
                {
                    return "*FY";
                }
                if (tk == ((char)Token.RP) || tk == ((char)Token.SUM) || tk == ((char)Token.EOF))
                {
                    return "";
                }
            }
            if (VR == ((char)Token.vars4))
            {
                if (tk == ((char)Token.ID))
                {
                    return "i";
                }
                if (tk == ((char)Token.LP))
                {
                    return "(E)";
                }
            }
            return "";
        }
        public string parse(string input, int pos)
        {
            char[] stack = {'$'};
            char a = nextToken(input, pos);
            char X = ((char)Token.vars0);
            while (X != ((char)Token.EOF))
            {
                Console.WriteLine("X = " + X);
                Console.WriteLine("A = " + a);
                if (X == a)
                {
                    Console.WriteLine("match(" + a + ")");
                    a = nextToken(input, pos);
                }
                else if (vars.includes(X))
                {
                    string m = predTable(X, a);
                    Console.WriteLine("M = " + m);
                    if (m == undefined) 
                    {
                        return "error";
                    }
                    m.split("").reverse().forEach(s => stack.push(s));
                }
                X = stack.pop();
            }
            return "success";
        }
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            string input = "i+i*i";
            int pos = 0;

            Console.WriteLine(parse(input, pos));
        }
    }
}