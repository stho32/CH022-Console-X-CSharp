using code2json.BL.TokenScanners;
using code2json.Interfaces;
using Xunit;

namespace code2json.BL.Tests.TokenScanners;

public class EnframingTokenScannerTests 
{
    private ITokenScanner GetTokenScanner(bool putStartAndEndIntoTheResult = false)
    {
        return new EnframingTokenScanner(
            "Comment", 
            "/*", 
            "*/", 
            string.Empty,
            string.Empty,
            putStartAndEndIntoTheResult);
    }
    
    [Fact]
    public void If_the_start_is_found_a_token_is_extracted()
    {
        var scanner = GetTokenScanner();

        var content = "/* Hello world */";
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.NotNull(token);
    }

    [Fact]
    public void The_token_is_normally_collected_without_start_and_end()
    {
        var scanner = GetTokenScanner();

        var content = "/* Hello world */";
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.Equal(" Hello world ", token?.Content);        
    }

    [Fact]
    public void The_token_can_be_extracted_with_start_and_end()
    {
        var scanner = GetTokenScanner(true);

        var content = "/* Hello world */";
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.Equal("/* Hello world */", token?.Content);        
    }

    [Fact]
    public void The_position_is_moved_to_the_start_of_the_next_possible_token()
    {
        var scanner = GetTokenScanner(true);

        var content = "/* Hello world */End";
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.Equal(17, position);        
    }

    [Fact]
    public void When_the_start_sequence_cannot_be_found_there_is_no_token_extracted()
    {
        var scanner = GetTokenScanner(true);

        var content = "XXX/* Hello world */";
        var position = 0;

        var token = scanner.GetToken(content, ref position);
        
        Assert.Null(token);        
    }
}