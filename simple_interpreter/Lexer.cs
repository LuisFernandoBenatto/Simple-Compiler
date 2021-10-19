using System;

namespace Interpretador
{
  public class Lexer
  {
    private string _spaces = " \n\t";
    public int Position { get; protected set; }
    public string Input { get; protected set; }
    public bool HasInput 
    { get
      {
        return !string.IsNullOrEmpty(Input) && Position < Input.Length;
      }
    }
    public Lexer(string input)
    {
      this.Input = input;
      this.Position = 0;
    }
    private char NextChar()
    {
      if (this.Position == this.Input.Length) 
      {
        return char.MinValue;
      }
      return this.Input[this.Position++];
    }
    public Token NextToken()
    {
      char peek;
      do
      {
        peek = NextChar();              
      } while (_spaces.Contains(peek));

      if (char.IsDigit(peek))
      {
        string v = "";
        do
        {
          v += peek;
          peek = NextChar();         
        } while (char.IsDigit(peek));
        Console.WriteLine("** Esse é o v: " + v + " **");
        if (peek != char.MinValue) 
        {
          this.Position--;
        }
        return new Token(ETokenType.NUM, double.Parse(v));
      }
      if (peek == '$') 
      {
        string v = "";
        do 
        {
          v += peek;
          peek = NextChar();
        } while (char.IsLetter(peek));
        Console.WriteLine("** Esse é o v '$': " + v + " **");
        if (peek != char.MinValue) 
        {
          this.Position--;
        }
          return new Token(ETokenType.VAR, v);
      }
      if (peek == 'p') 
      {
        var v = "";
        do 
        {
          v += peek;
          peek = NextChar();
        } while (char.IsLetter(peek));
        Console.WriteLine("** Esse é o v 'print': " + v + " **");
        if (peek != char.MinValue) 
        {
          this.Position--;
        }
        if(v.Contains("print"))
        {
          return new Token(ETokenType.PRINT);
        }
        else
        {
          return new Token(ETokenType.INVALID);
        }
      }
      Console.WriteLine("** Esse é o peek: " + peek + " **");
      if (peek == char.MinValue)
      {
        return new Token(ETokenType.EOF);
      }
      else if (peek == '+')
      {
        return new Token(ETokenType.SUM);
      }
      else if (peek == '-')
      {
        return new Token(ETokenType.SUB);
      }
      else if (peek == '/')
      {
        return new Token(ETokenType.DIV);
      }
      else if (peek == '*')
      {
        return new Token(ETokenType.MULT);
      }
      else if (peek == ';')
      {
        return new Token(ETokenType.EOL);
      }
      else if (peek == '(')
      {
        return new Token(ETokenType.OPEN);
      }
      else if (peek == ')')
      {
        return new Token(ETokenType.CLOSE);
      }
      else if (peek == '=')
      {
        return new Token(ETokenType.ATTR);
      }
      else 
      {
        return new Token(ETokenType.INVALID);
      }
    }
  }  
  public class Token
  {
    public ETokenType Type {get;set;}
    public double? Attribute {get;set;}
    public String Name {get;set;}
    public bool HasValue 
    { get
      {
        if(this.Attribute == null) 
        {
          return false;
        } 
        else
        {
          return true;
        }
      }
    }
    public Token(ETokenType type, double? value, string name)
    {
      this.Type = type;
      this.Attribute = value;
      this.Name = name;
    }
    public Token(ETokenType type, double? value)
    {
      this.Type = type;
      this.Attribute = value;
      this.Name = "";
    }
    public Token(ETokenType type, string name)
    {
      this.Type = type;
      this.Attribute = null;
      this.Name = name;
    }
    public Token(ETokenType type)
    {
      this.Type = type;
      this.Attribute = null;
      this.Name = "";
    }
  }
  public enum ETokenType
  {
    NUM = 0,
    SUM = 1,
    SUB = 2,
    MULT = 3,
    DIV = 4,
    OPEN = 5,
    CLOSE = 6,
    VAR = 7,
    ATTR = 8,
    PRINT = 9,
    EQ = 10,
    EOL = 98,
    EOF = 99,
    INVALID = -1,
  }
}