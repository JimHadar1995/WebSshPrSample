using System;

namespace Library.Common.SourceGenerators.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestHelloWorld.HelloWorld.Hello();
            Console.ReadKey();
        }
    }
}
