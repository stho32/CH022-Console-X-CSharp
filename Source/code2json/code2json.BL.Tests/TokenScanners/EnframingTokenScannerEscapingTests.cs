using code2json.BL.TokenScanners;
using code2json.Interfaces;
using Xunit;

namespace code2json.BL.Tests.TokenScanners;

public class EnframingTokenScannerEscapingTests {

    protected ITokenScanner GetTokenScanner(bool putStartAndEndIntoTheResult)
    {
        return new EnframingTokenScanner(
            "double-quoted string",
            "\"",
            "\"",
            "\\",
            "\\\\", // escaped backslash
            putStartAndEndIntoTheResult
        );
    }
    
    [Fact]
    public void Can_extract_a_sequence_with_no_escaping_char_mentioned()
    {
        var scanner = GetTokenScanner(true);
        var content = @"""Hello world""";
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.NotNull(token);
        Assert.Equal("\"Hello world\"", token?.Content);
    }

    [Fact]
    public void Double_Quotes_can_be_escaped()
    {
        var scanner = GetTokenScanner(true);
        var content = @"""Hello \""world\""""";
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.NotNull(token);
        Assert.Equal("\"Hello \\\"world\\\"\"", token?.Content);
    }

    [Fact]
    public void When_start_and_end_are_not_put_into_the_result_the_result_does_remove_the_escaping_sequences()
    {
        var scanner = GetTokenScanner(false);
        var content = @"""Hello \""world\""""";
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.NotNull(token);
        Assert.Equal("Hello \"world\"", token?.Content);        
    }

    [Fact]
    public void Correctly_recognizes_escaped_double_quote()
    {
        var scanner = GetTokenScanner(true);
        var content = '"'.ToString() + '\\' + '"' + '"';
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.NotNull(token);
        Assert.Equal(content, token?.Content);        
    }
    
    [Fact]
    public void Correctly_recognizes_escaped_backslash()
    {
        var scanner = GetTokenScanner(true);
        var content = '"'.ToString() + '\\' + '\\' + '"';
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.NotNull(token);
        Assert.Equal(content, token?.Content);        
    }
}