namespace code2json.Interfaces;

public interface ITokenScanner
{
    IToken? GetToken(string content, ref int position);
}