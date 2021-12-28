using code2json.BL.TokenScanners;
using code2json.Interfaces;
using Xunit;

namespace code2json.BL.Tests.TokenScanners;

public class CollectionOfValidCharsButDifferentStartTokenScannerTests
{
    private ITokenScanner GetTokenScanner()
    {
        return new CollectionOfValidCharsButDifferentStartTokenScanner(
            "number", "0123456789+-", "0123456789.");
    }
    
    [Fact]
    public void When_the_first_character_is_in_the_allowed_list_the_token_is_extracted()
    {
        var scanner = GetTokenScanner();

        var content = "+1.75";
        var position = 0;

        var token = scanner.GetToken(content, ref position);

        Assert.NotNull(token);
        Assert.Equal("+1.75", token?.Content);
    }

    [Fact]
    public void When_the_first_character_not_part_of_the_allowed_chars_for_the_first_char_the_token_is_not_extracted()
    {
        var scanner = GetTokenScanner();

        var content = "binary+1.75";
        var position = 0;

        var token = scanner.GetToken(content, ref position);

        Assert.Null(token);
    }
}