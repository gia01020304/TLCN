using CommentAnalysis;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MLSimilarCommentAnalysis.Instance.TrainDataSet();
            MLSimilarCommentAnalysis.Instance.GetReplyComment("Hello baby");
            Console.WriteLine("Hello World!");
        }
    }
}
