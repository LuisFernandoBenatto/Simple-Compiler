using System;
namespace Interpretador
{
    class Interpretador
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer("$x = 2 + 2 * 3; "
                                  + "$y = 1 + 10 / 2; "
                                  + "$z = $x + $y; "
                                  + "print($z);");
            Parser parser = new Parser(lexer);
            parser.Prog();
        }
    }
}
