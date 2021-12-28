using code2json.Interfaces;

namespace code2json.BL.TokenScanners;

public class EnframingTokenScanner : TokenScannerBase
{
    private readonly string _startsWith;
    private readonly string _endsWith;
    private readonly bool _putStartAndEndIntoTheResult;

    public EnframingTokenScanner(
        string typeName, 
        string startsWith, 
        string endsWith,
        bool putStartAndEndIntoTheResult) : base(typeName)
    {
        _startsWith = startsWith;
        _endsWith = endsWith;
        _putStartAndEndIntoTheResult = putStartAndEndIntoTheResult;
    }

    public override IToken? GetToken(string content, ref int position)
    {
        if (PeekingFromEquals(content, position, _startsWith))
        {
            if (_putStartAndEndIntoTheResult)
            {
                return new Token(
                    TypeName,
                    _startsWith + Collect(content, ref position) + _endsWith
                );
            }

            return new Token(
                TypeName,
                Collect(content, ref position)
            );
        }

        return null;
    }

    private string Collect(string content, ref int position)
    {
        position += _startsWith.Length;

        var result = "";
        
        while (position < content.Length && !PeekingFromEquals(content, position, _endsWith))
        {
            result += content[position];
            position += 1;
        }

        position += _endsWith.Length;
        
        return result;
    }
}