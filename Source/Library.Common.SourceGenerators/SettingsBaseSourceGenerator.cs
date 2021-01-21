using System;
using Microsoft.CodeAnalysis;

namespace Library.Common.SourceGenerators
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.CodeAnalysis.ISourceGenerator" />
    [Generator]
    public class SettingsBaseSourceGenerator : ISourceGenerator
    {
        /// <inheritdoc />
        public void Initialize(GeneratorInitializationContext context)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Execute(GeneratorExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
