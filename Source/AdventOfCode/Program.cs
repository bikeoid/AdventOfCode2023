
using AdventOfCode;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Reflection;
using static System.Runtime.CompilerServices.RuntimeHelpers;


const int year = 2023;
const int day = 2;  // Also change day number in Run line below
var runPuzzleDelegate = new RunPuzzleDelegate(Day02.Run);


Console.WriteLine($"Advent of code, day {day}");
Console.WriteLine();

var puzzleInputLink = $"https://adventofcode.com/{year}/day/{day}/input";

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var secretProvider = config.Providers.First();
string? session = null;
if (!secretProvider.TryGet("AOC:session", out session)) {
    Console.WriteLine("Session not specified - obtain from logged in page on AdventOfCode, developer info / network / request / cookies");
    Console.WriteLine("Then Manage User secrets, for example:");
    Console.WriteLine("{");
    Console.WriteLine("  \"AOC\": {");
    Console.WriteLine("    \"session\": \"xxxxxx...\"");
    Console.WriteLine("  }");
    Console.WriteLine("}");
    return;
}

HttpClient httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("Cookie", $"session={session}");


try
{
    var appPath = Assembly.GetExecutingAssembly().Location;
    var puzzleFilename = Path.Combine(Path.GetDirectoryName(appPath), $"Day{day}.txt");

    if (!File.Exists(puzzleFilename))
    {
        var httpResult = await httpClient.GetAsync(puzzleInputLink);
        using var resultStream = await httpResult.Content.ReadAsStreamAsync();
        using var fileStream = File.Create(puzzleFilename);
        resultStream.CopyTo(fileStream);
    }


    //string puzzleInput = await httpClient.GetStringAsync(puzzleInputLink);

    //Console.WriteLine(responseBody);

    runPuzzleDelegate(puzzleFilename);
}
catch (HttpRequestException e)
{
    Console.WriteLine($"\nUnable to download puzzle input from {puzzleInputLink}:");
    Console.WriteLine("Message :{0} ", e.Message);
}


delegate void RunPuzzleDelegate(string s);
