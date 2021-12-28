using code2json.BL.TokenScanners;
using code2json.Interfaces;
using Xunit;

namespace code2json.BL.Tests.TokenScanners;

public class CollectionOfValidCharsTokenScannerTests
{
    private ITokenScanner GetTokenScanner()
    {
        return new CollectionOfValidCharsTokenScanner("grobble", "abc");
    } 
    
    [Fact]
    public void If_the_char_occurs_in_the_valid_characters_the_token_is_gathered()
    {
        var scanner = GetTokenScanner();

        var position = 0;
        var token = scanner.GetToken("bbbccccbbabbabb", ref position);
        
        Assert.NotNull(token);
        Assert.Equal("grobble", token?.TypeName);
        Assert.Equal("bbbccccbbabbabb", token?.Content);
    }

    [Fact]
    public void The_token_is_gathered_until_the_first_occurence_of_a_character_that_is_not_valid()
    {
        var scanner = GetTokenScanner();

        var position = 0;
        var token = scanner.GetToken("bbbccccbbabbabbXXXXXDDDDDDDD", ref position);
        
        Assert.NotNull(token);
        Assert.Equal("grobble", token?.TypeName);
        Assert.Equal("bbbccccbbabbabb", token?.Content);
    }

    [Fact]
    public void If_the_char_does_not_occur_in_the_valid_characters_the_token_is_not_gathered()
    {
        var scanner = GetTokenScanner();

        var position = 0;
        var token = scanner.GetToken("XXXXXDDDDDDDD", ref position);
        
        Assert.Null(token);
    }

    [Fact]
    public void After_the_token_is_gathered_the_position_is_one_behind_the_end_of_the_token()
    {
        var scanner = GetTokenScanner();

        var position = 0;
        var unused = scanner.GetToken("bXXXXXDDDDDDDD", ref position);
        
        Assert.Equal(1, position);
    }
}