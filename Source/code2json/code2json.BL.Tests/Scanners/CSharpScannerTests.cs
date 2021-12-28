using System.Diagnostics;
using code2json.BL.Scanners;
using Xunit;

namespace code2json.BL.Tests.Scanners;

public class CSharpScannerTests
{
    [Fact]
    public void Can_scan_the_code_of_this_repository()
    {
        var path = System.Reflection.Assembly.GetAssembly(typeof(CSharpScannerTests))?.Location;
        Debug.Assert(path != null, nameof(path) + " != null");
        
        var indexOfSourceFolder = path.IndexOf("Source", StringComparison.Ordinal);
        var sourceFolder = path.Substring(0, indexOfSourceFolder) + "/Source";

        var codeFiles = Directory.GetFiles(sourceFolder, "*.cs", SearchOption.AllDirectories);
        var scanner = new CSharpScanner();
        
        foreach (var file in codeFiles)
        {
            var fileTokens = scanner.ScanCode(File.ReadAllText(file), file);
            Assert.NotNull(fileTokens);
            Assert.NotEmpty(fileTokens.Tokens);
        }
    }
}