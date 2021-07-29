<h1 align="center">Simple Interpreter</h1>
<div align="center">

[![license](https://img.shields.io/github/license/LuisFernandoBenatto/Simple-Interpreter.svg)](https://github.com/LuisFernandoBenatto/Simple-Interpreter/blob/master/LICENSE)
<a href="https://docs.microsoft.com/pt-br/dotnet/csharp/" target="_blank"><img alt="CSharp" src="https://img.shields.io/badge/C Sharp-%2332CD32.svg?&style=flat&logo=csharp&logoColor=white"/></a>
</div>

<p>Implementation: A simple expression interpreter</p>

---

<h3 align="center"><strong> Create New Project in <img align="center" height="30" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/csharp/csharp.png"></strong></h3>
<h4> Install <a href="https://docs.microsoft.com/pt-br/dotnet/core/install/windows?tabs=net50" target="_blank">.NET</a> on Windows </h4>
Create the app:

```
  $ dotnet new console
```
Run the application:
```
  $ dotnet run
```
---

## Example

```C#
static void Main(string[] args)
{
  Lexer lexer = new Lexer("$x = 2 + 2 * 3; "
                        + "$y = 1 + 10 / 2; "
                        + "$z = $x + $y; "
                        + "print($z);");
  Parser parser = new Parser(lexer);
  parser.Prog();
}  
// Output: 14
```
