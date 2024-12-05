using System.Collections.Generic;

public class Day5
{
    
    List<Tuple<int,int>> correlations;

    List<List<int>> updates;

    public void Work()
    {
        Console.WriteLine("Hello, World from Day 5!");

        var lines = Utils.ReadInputFileAsListString(5);
        var correlationRaw = lines.TakeWhile(l => l.Contains("|"));
        var updateRaw = lines.SkipWhile(l => !l.Contains(","));

        InitList(correlationRaw, updateRaw);

        Console.WriteLine($"Sum Correct = {updates.Where(IsCorrect).Sum(MiddleNumber)}");

        var incorrect = updates.Where(u => !IsCorrect(u)).ToList();

        CorrectFault(incorrect);

        Console.WriteLine($"Sum Incorrect = {incorrect.Sum(MiddleNumber)}");
        Console.ReadLine();
    }

    void CorrectFault(List<List<int>> incorrect){
        foreach(var inc in incorrect){
            var fault = FindFault(inc);
            while(fault != null){
                inc.Swap(inc.IndexOf(fault.Item1), inc.IndexOf(fault.Item2) );
                fault = FindFault(inc);
            }
        }
    }

    Tuple<int,int>? FindFault(List<int> update){
         
         Tuple<int,int> faultyRule = null;
         for(var i = 0 ; i < update.Count(); i ++){
            var rules = correlations.Where(c => c.Item1 == update[i] && update.Contains(c.Item2)).ToList();
            foreach(var rule in rules){
                if(update.IndexOf(rule.Item2) < i){
                    faultyRule = rule;
                    break;
                }
            }
            if(faultyRule != null)
                break;
        }
        return faultyRule;
    }

    bool IsCorrect(List<int> update){
        bool isCorrect = true;
         for(var i = 0 ; i < update.Count(); i ++){
            var rules = correlations.Where(c => c.Item1 == update[i] && update.Contains(c.Item2)).ToList();
            isCorrect &= rules.All(r => update.IndexOf(r.Item2) > i);
        }
        return isCorrect;
    }

    int MiddleNumber(List<int> list) => list[(int)Math.Floor((decimal)list.Count / 2)];

    void InitList(IEnumerable<string> correlationRaw, IEnumerable<string> updateRaw){
        correlations = correlationRaw.Select(s => {
            var parts = s.Split("|").Select(p => int.Parse(p)).ToList();
           return new Tuple<int,int>(parts[0], parts[1]);
        }).ToList();

        updates = updateRaw.Select(l => l.Split(",").Select(n => int.Parse(n)).ToList()).ToList();
    }

   
}