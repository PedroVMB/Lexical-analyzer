using System;
using System.Collections.Generic;
using System.IO;
using Analisador_lexico; // Certifique-se de que o namespace está correto

public class Program
{
    public static void Main(string[] args)
    {
        string filePath = @"C:\Users\pedro\source\repos\Analisador lexico\Analisador lexico\Teste.Java"; // Caminho para o arquivo Java
        string code = File.ReadAllText(filePath);

        Lexer lexer = new Lexer();
        List<Token> tokens = lexer.Analyze(code);

        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }
}
