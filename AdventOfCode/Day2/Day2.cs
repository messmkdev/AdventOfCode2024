using System.Collections.Generic;

public class Day2
{
    IEnumerable<int> validRange = Enumerable.Range(1,3);

    public void Work()
    {
        Console.WriteLine("Hello, World from Day 2!");

        Input input = Utils.ReadInputFile(2);

        int nbValid = input.GetRows().Count(IsValidPart2);

        Console.WriteLine($"Nb report valid = {nbValid}");

        Console.ReadLine();
    }

    bool IsValidPart1(List<int> line)
    {
        bool ordered = line.Order().SequenceEqual(line) || line.Order().Reverse().SequenceEqual(line);
        if (!ordered)
            return false;
        return line.NextValidator((curr,next) => validRange.Contains(Math.Abs(curr - next)));
    }

    bool IsValidPart2(List<int> line)
    {
        if (IsValidPart1(line))
            return true;
        for (var i = 0; i < line.Count; i++)
        {
            var newLine = new List<int>(line);
            newLine.RemoveAt(i);
            if (IsValidPart1(newLine))
                return true;
        }
        return false;
    }
}