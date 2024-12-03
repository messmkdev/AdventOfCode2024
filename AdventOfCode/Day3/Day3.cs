using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text.RegularExpressions;

public class Day3
{
    public void Work()
    {
        Console.WriteLine("Hello, World from Day 3!");

        var input = "do()"+Utils.ReadInputFileAsString(3);

        var matches = MatchesMul(input);

        var sum = SumOfPair(matches.Select(ExtractValueFromMatch));

        var doRegexp = new Regex(@"do\(\)");
        var dontRegexp = new Regex(@"don\'t\(\)");

        List<Match> validMatch = new List<Match>();

        var currentStr = input;
        //start with a do()
        while(!string.IsNullOrWhiteSpace(currentStr)){
            var match = dontRegexp.Match(currentStr); // find first don't()
            if(match.Success){
                var part = currentStr.Substring(0, match.Index); // part to analyse 
                validMatch.AddRange(MatchesMul(part)); // add valid mul in part
                currentStr = currentStr.Remove(0, match.Index + match.Length); // remove part 
                var newBegin = doRegexp.Match(currentStr); // find new begin with do()
                currentStr = currentStr.Remove(0, newBegin.Index); // remove useless content
            }
            else{
                validMatch.AddRange(MatchesMul(currentStr)); // final do()
                currentStr = ""; // end loop
            }
        }

        var count = SumOfPair(validMatch.Select(ExtractValueFromMatch));
        Console.WriteLine($"Sum of mul {count}");
        Console.ReadLine();
    }

    public IEnumerable<Match> MatchesMul(string input){
        return new Regex(@"mul\([0-9]+,[0-9]+\)").Matches(input);
    }

    public string ExtractValueFromMatch(Match match) => match.Value.Substring(4, match.Value.Count() - 5);

    public int SumOfPair(IEnumerable<string> pairs){
        return pairs.Select(p => p.Split(",").Select(s => int.Parse(s))).Sum(p => p.Aggregate((acc,val) => acc * val));
    }

}
