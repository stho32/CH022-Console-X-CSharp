using code2json.Interfaces;

namespace code2json.BL.Scanners;

public abstract class ScannerBase : IScanner
{
    protected readonly ITokenScanner[] _tokenScanners;
    private readonly string _fileFilter;

    protected ScannerBase(ITokenScanner[] tokenScanners, string fileFilter)
    {
        _tokenScanners = tokenScanners;
        _fileFilter = fileFilter;
    }

    private IToken[] GetTokens(string code, string filePath)
    {
        var result = new List<IToken>();

        var position = 0;
        while (position < code.Length)
        {
            var lastPosition = position;
            
            foreach (var tokenScanner in _tokenScanners)
            {
                var token = tokenScanner.GetToken(code, ref position);
                
                if (token != null)
                {
                    result.Add(token);
                    break;
                }
            }

            if (position < code.Length && 
                lastPosition == position)
            {
                var startPosition = position - 20;
                if (startPosition < 0)
                    startPosition = 0;
                var aBitMoreCode = code.Substring(startPosition, 20);

                var row = code.Substring(0, position).Count(x => x == '\n') + 1;
                
                throw new Exception(
                    $"I could not get past position {position} in file {filePath}. I am probably missing a token scanner around here. The character is '{code[position]}', in row {row}. Look here '{aBitMoreCode}'");
            }
        }
        
        return result.ToArray();
    }

    public IFileTokens ScanCode(string code, string filePath = "")
    {
        var tokens = GetTokens(code, filePath);

        return new FileTokens(
            filePath,
            tokens
        );
    }

    public IFileTokens[] ScanDirectory(string path)
    {
        var files = Directory.GetFiles(path, _fileFilter, SearchOption.AllDirectories);
        var result = new List<IFileTokens>();
        
        foreach (var file in files)
        {
            result.Add(ScanCode(File.ReadAllText(file), file));
        }

        return result.ToArray();
    }
}