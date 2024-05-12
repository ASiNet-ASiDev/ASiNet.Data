using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ASiNet.Data.Serialization.Analyzers;
public static class Helper
{

    public static bool ContainsAttribute<T>(this SyntaxList<AttributeListSyntax> attributeList) where T : Attribute =>
        attributeList.FirstOrDefault(x => x.Attributes.ContainsAttribute<T>()) is not null;
    

    public static bool ContainsAttribute<T>(this SeparatedSyntaxList<AttributeSyntax> attributeList) where T : Attribute =>
        attributeList.FirstOrDefault(x => x.ContainsAttribute<T>()) is not null;

    public static bool ContainsAttribute<T>(this AttributeSyntax attribute) where T : Attribute =>
        $"{attribute.Name}" == typeof(T).Name;

    public static bool ContainsBase<T>(this BaseListSyntax baseList) where T :  class =>
        baseList.Types.FirstOrDefault(x => x.ChildNodes().FindIdentifier()?.Identifier.Text == typeof(T).Name) is not null;
    

    public static IdentifierNameSyntax FindIdentifier(this IEnumerable<SyntaxNode> nodes) =>
        nodes.FirstOrDefault(x => x is IdentifierNameSyntax) as IdentifierNameSyntax;
}
