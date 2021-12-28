using code2json.Interfaces;

namespace code2json.BL;

public class FileTokens : IFileTokens
{
    public string FilePath { get; }
    public IToken[] Tokens { get; }

    public FileTokens(string filePath, IToken[] tokens)
    {
        FilePath = filePath;
        Tokens = tokens;
    }
}