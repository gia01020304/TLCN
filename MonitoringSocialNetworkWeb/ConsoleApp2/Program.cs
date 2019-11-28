using CommentAnalysis;
using General.Extension;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var rsSend = EmailExtension.Instance.SendEmailAsync("gia01020304@gmail.com", "Social Care: Comment Negative", "Hello").Result;
            Console.WriteLine("Hello World!");
        }
    }
}
