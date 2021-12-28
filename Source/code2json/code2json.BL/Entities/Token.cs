using code2json.Interfaces;

namespace code2json.BL;

public class Token : IToken
{
    public string TypeName { get; }
    public string Content { get; }

    public Token(string typeName, string content)
    {
        TypeName = typeName;
        Content = content;
    }

    public override string ToString()
    {
        return $"{nameof(TypeName)}: {TypeName}, {nameof(Content)}: {Content}";
    }
}