/*
 * code2json - parses code files and creates a json file that contains everything as scanned tokens
 *             thus enabling code quality checks using powershell scripting
 */

using System.Diagnostics;
using System.Text.Json;
using code2json.BL.Scanners;
using code2json.Interfaces;

if (args.Length is 0 or < 3 or > 3)
{
 Console.WriteLine("code2json");
 Console.WriteLine("parses code files and creates a json file that contains everything as scanned tokens");
 Console.WriteLine("thus enabling easier code quality checks");
 Console.WriteLine("");
 Console.WriteLine("usage: ");
 Console.WriteLine("code2json <language> <inputfolder> outputfile.json");
 Console.WriteLine("");
 Console.WriteLine("language: c# or tsql");
 return;
}

var language = args[0];
var inputfolder = args[1];
var outputfile = args[2];

IScanner? scanner = null;
switch (language.ToLower())
{
 case "c#":
  scanner = new CSharpScanner();
  break;
 case "tsql":
  scanner = new TSqlScanner();
  break;
 default:
  Console.WriteLine($"I do not know the language {language}");
  return;
}

var results = scanner.ScanDirectory(inputfolder);
var asJson = JsonSerializer.SerializeToUtf8Bytes(results);
File.WriteAllBytes(outputfile, asJson);
