using code2json.Interfaces;

namespace code2json.BL.TokenScanners;

public abstract class TokenScannerBase : ITokenScanner
{
    protected readonly string TypeName;

    protected TokenScannerBase(string typeName)
    {
        TypeName = typeName;
    }
    
    public abstract IToken? GetToken(string content, ref int position);
}