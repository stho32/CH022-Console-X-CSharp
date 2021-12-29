using code2json.BL.TokenScanners;
using code2json.Interfaces;

namespace code2json.BL.Scanners;

public class TSqlScanner : ScannerBase
{
    public TSqlScanner() : base(
        new ITokenScanner[]
        {
            new CollectionOfValidCharsTokenScanner("whitespace", " \t\r\n"),
            new CollectionOfValidCharsButDifferentStartTokenScanner("number", "+-0123456789.", "0123456789."),
            new EnframingTokenScanner("multiline comment", "/*", "*/", "", string.Empty,true),
            new EnframingTokenScanner("single line comment", "--", "\n", "", string.Empty, true),
            new CollectionOfValidCharsButDifferentStartTokenScanner("word", "#@qwertzuiopasdfghjklyxcvbnmQWERTZUIOPASDFGHJKLYXCVBNM_1234567890[", "qwertzuiopasdfghjklyxcvbnmQWERTZUIOPASDFGHJKLYXCVBNM_1234567890[]."),
            new CollectionOfValidCharsTokenScanner("opening bracket", "("),
            new CollectionOfValidCharsTokenScanner("closing bracket", ")"),
            new CollectionOfValidCharsTokenScanner("operator", "+-*/=|&!<>"),
            new CollectionOfValidCharsTokenScanner("comma", ","),
            new EnframingTokenScanner("single quoted string", "'", "'", string.Empty, "''", true)
        })
    {
    }
}