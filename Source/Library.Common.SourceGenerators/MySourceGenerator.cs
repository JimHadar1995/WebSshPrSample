using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Library.Common.SourceGenerators
{
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = new StringBuilder(@"
using System;
namespace TestHelloWorld {
    public static class HelloWorld {
        public static void Hello() {
            Console.WriteLine(""Hello, world!"");
");
            foreach (SyntaxTree tree in context.Compilation.SyntaxTrees)
            {
                sourceBuilder.AppendLine($@"Console.WriteLine(@"" - { tree.FilePath }""); ");
            }
            sourceBuilder.Append(@"
    }
}
}");

            context.AddSource("TestHelloWorld", sourceBuilder.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            Debugger.Launch();
#endif
        }

    }
}
