using code2json.BL.TokenScanners;
using code2json.Interfaces;

namespace code2json.BL.Scanners;

public class CSharpScanner : ScannerBase
{
    public CSharpScanner() : base(
        new ITokenScanner[]
        {
            new EnframingTokenScanner("multiline comment", "/*", "*/", "", string.Empty,true),
            new EnframingTokenScanner("single line comment", "//", "\n", "", string.Empty, true),
            new EnframingTokenScanner("double quoted string", "\"", "\"", "\\", "\\\\", true),
            new EnframingTokenScanner("character string", "'", "'", "\\", "\\\\", true),
            new EnframingTokenScanner("verbatim double quoted string", "@\"", "\"", string.Empty, "\"\"",true),
            new EnframingTokenScanner("interpolated double quoted string", "$\"", "\"", "\\", "\"\"",true),
            new CollectionOfValidCharsTokenScanner("whitespace", " \t\r\n"),
            new CollectionOfValidCharsButDifferentStartTokenScanner("word", "qwertzuiopasdfghjklyxcvbnm_QWERTZUIOPASDFGHJKLYXCVBNM", "qwertzuiopasdfghjklyxcvbnm_QWERTZUIOPASDFGHJKLYXCVBNM.?[]1234567890"),
            new CollectionOfValidCharsTokenScanner("operator", "+-*/=|&!<>"),
            new CollectionOfValidCharsTokenScanner("semicolon", ";"),
            new CollectionOfValidCharsTokenScanner("comma", ","),
            new CollectionOfValidCharsTokenScanner("opening curly bracket", "{"),
            new CollectionOfValidCharsTokenScanner("closing curly bracket", "}"),
            new CollectionOfValidCharsTokenScanner("opening bracket", "("),
            new CollectionOfValidCharsTokenScanner("closing bracket", ")"),
            new CollectionOfValidCharsTokenScanner("opening square bracket", "["),
            new CollectionOfValidCharsTokenScanner("closing square bracket", "]"),
            new CollectionOfValidCharsTokenScanner("question mark", "?"),
            new CollectionOfValidCharsTokenScanner("double point", ":"),
            new CollectionOfValidCharsButDifferentStartTokenScanner("number", "+-0123456789.", "0123456789.")
        },
        "*.cs")
    {
    }
}