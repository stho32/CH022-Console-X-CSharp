using code2json.BL.TokenScanners;
using code2json.Interfaces;
using Xunit;

namespace code2json.BL.Tests.TokenScanners;

public class VerbatimStringTokenScannerTests
{
    private ITokenScanner GetTokenScanner()
    {
        return new EnframingTokenScanner(
            "verbatim double quoted string",
            "@\"", 
            "\"", 
            string.Empty,
            "\"\"",
            true);
    }

    [Fact]
    public void Can_scan_a_normal_verbatim_string_without_escapes()
    {
        var content = "@\"Hello world\"";
        var position = 0;

        var scanner = GetTokenScanner();
        var token = scanner.GetToken(content, ref position);
        
        Assert.Equal(content, token?.Content);
    }  
    
    [Fact]
    public void Can_scan_a_verbatim_string_with_an_doublequote_quote()
    {
        var content = "@\"Hello \"\"world\"\"\"";
        var position = 0;

        var scanner = GetTokenScanner();
        var token = scanner.GetToken(content, ref position);
        
        Assert.Equal(content, token?.Content);
    } 
}