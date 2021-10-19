using System;
using System.Collections.Generic;

namespace Interpretador
{
  public class Parser
  {
    Dictionary<string, double> symbolTable = new Dictionary<string, double>();
    public string Output { get; private set; }
    private Lexer _lexer;
    public Token lookahead;
    public Parser(Lexer lexer)
    {
      this._lexer = lexer;
      this.lookahead = this._lexer.NextToken();
    }
    public void Match(Token token)
    {
      // Console.WriteLine("Entrou no Match");
      if (this.lookahead.Type == token.Type && this.lookahead.Attribute == token.Attribute)
      {
        this.lookahead = this._lexer.NextToken();
      } 
      else 
      {
         throw new System.Exception("\n*** Syntax Error! Values do not match. *** \n");
      }
    }
    public double Term()
    { //term ::= OPEN expr CLOSE | NUM | VAR
      // Console.WriteLine("Entrou no Term");
      // Console.WriteLine(this.lookahead.Type);

      if (this.lookahead.Type == ETokenType.OPEN) 
      {
        return this.Expr();
      }
      if (this.lookahead.Type == ETokenType.NUM)
      {
        double? _value = this.lookahead.Attribute;
        this.Match(this.lookahead);
        return (double)_value;
      }
      if (this.lookahead.Type == ETokenType.VAR)
      {
        string _key = this.lookahead.Name;
        this.Match(this.lookahead);
        return symbolTable[_key];
      }
      throw new System.Exception("\n*** Syntax Error! '" + this.lookahead.Attribute + "' it's not a number. ***\n");
    
    }
    public double Fact() 
    {// fact ::= term MULT fact | term DIV fact | term
      // Console.WriteLine("Entrou no Fact");
      double _term = this.Term();  
      if (this.lookahead.Type == ETokenType.MULT) 
      {
        this.Match(this.lookahead);
        double _fact1 = this.Fact();
        return _term * _fact1;
      } 
      else if (this.lookahead.Type == ETokenType.DIV) 
      {
        this.Match(this.lookahead);
        double _fact1 = this.Fact();
        return _term / _fact1;
      } 
      else 
      {
        return _term;
      }
    } 
    public double Expr() 
    {//expr ::= fact SUM expr | fact SUB expr | fact 
      // Console.WriteLine("Entrou no Expr");
      double _fact = this.Fact();  
      if (this.lookahead.Type == ETokenType.SUM) 
      {
        this.Match(this.lookahead);
        double _expr1 = this.Expr();
        return _fact + _expr1;
      } 
      else if (this.lookahead.Type == ETokenType.SUB) 
      {
          this.Match(this.lookahead);
          double _expr1 = this.Expr();
          return _fact - _expr1;
      } 
      else 
      {
        return _fact;
      }
    }
    public void Print() 
    {// imp  ::= PRINT OPEN VAR CLOSE
      // Console.WriteLine("Entrou no Print");
      if(lookahead.Type == ETokenType.PRINT)
      {
        this.Match(lookahead);
      }
      if(lookahead.Type == ETokenType.OPEN)
      {
        this.Match(lookahead);
      }
      double? _value;  
      if(this.lookahead.Type == ETokenType.NUM)
      {
        _value = this.lookahead.Attribute; 
        this.Match(lookahead);
      }
      else
      {
        _value = symbolTable[this.lookahead.Name];
        this.Match(lookahead);
      }   
      if(lookahead.Type == ETokenType.CLOSE)
      {
        this.Match(lookahead);
      }
      Console.WriteLine("Saída: " + _value);
    }
    public void Attr() 
    { // atr  ::= VAR EQ expr
      // Console.WriteLine("Entrou no Attr");
      string _value = this.lookahead.Name;
      this.Match(this.lookahead);
      this.Match(this.lookahead);
      double _expr = this.Expr();
      symbolTable.Add(_value, _expr);
    }
    public void Stmt()
    { //  stmt ::= atr | imp
      // Console.WriteLine("Entrou no Stmt");
      if (this.lookahead.Type == ETokenType.VAR)
      {
        this.Attr();
      }
      else if (this.lookahead.Type == ETokenType.PRINT)
      {
        this.Print();
      }
      else 
      {
        if(this.lookahead.Type != ETokenType.EOF) 
        {
          throw new System.Exception("\n*** Syntax error!!! Parser *** \n");
        }
        throw new System.Exception("\n*** esperava VAR ou PRINT *** \n");
      }
    }
    public void Lines() 
    { //lines::= prog | ε
      // Console.WriteLine("Entrou no Lines");
      if (this.lookahead.Type != ETokenType.EOF)
      {
        this.Prog();
      }
    }
    public void Prog() 
    {  //prog ::= stmt EOL lines
      // Console.WriteLine("Entrou no Prog");
      this.Stmt();
      this.Match(this.lookahead);
      this.Lines();
    }
  }
}