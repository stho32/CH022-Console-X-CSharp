namespace code2json.Interfaces;

public interface IScanner
{
    IFileTokens ScanCode(string code, string filePath = "");
}