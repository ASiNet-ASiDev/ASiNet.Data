using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ASiNet.Data.Serialization.Attributes;
using ASiNet.Data.Serialization.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ASiNet.Data.Serialization.Analyzers;

[Generator]
public class ModelSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            (x, _) => x is ClassDeclarationSyntax classSyntax && 
                classSyntax.AttributeLists.ContainsAttribute<PreGenerateAttribute>() &&
                classSyntax.BaseList.ContainsBase<ISerializeModel>(),
            (x, _) => (ClassDeclarationSyntax)x.Node)
             .Where(x => x != null);

        var p = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(p, Execute);
    }

    private void Execute(SourceProductionContext context, (Compilation Left, ImmutableArray<ClassDeclarationSyntax> Right) tuple)
    {
        var (compilation, list) = tuple;

        var values = new List<string>();

        foreach (var syntax in list)
        {
            var attr = syntax.AttributeLists.FirstOrDefault(
                x => x.Attributes.FirstOrDefault(
                    y => $"{y.Name}Attribute" == nameof(PreGenerateAttribute)) is not null);

            if (attr is not null)
            {
                values.Add(syntax.Identifier.Text);
            }
        }
        var sb = new StringBuilder();
        sb.AppendLine("/*");
        foreach (var item in values)
        {
            sb.AppendLine($"* {item}");
        }
        sb.AppendLine("*/");


        context.AddSource("test.g.cs", sb.ToString());
    }

}
