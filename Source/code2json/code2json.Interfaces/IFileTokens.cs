namespace code2json.Interfaces;

public interface IFileTokens
{
    string FilePath { get; }
    IToken[] Tokens { get; }
}