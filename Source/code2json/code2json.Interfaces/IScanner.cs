namespace code2json.Interfaces;

public interface IScanner
{
    IToken[] GetTokens(string code);
}