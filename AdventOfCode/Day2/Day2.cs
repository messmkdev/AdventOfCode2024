using System.Collections.Generic;

public class Day2
{
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
        if (!line.IsOrdered())
            return false;
        return line.NextValidator((curr,next) => Enumerable.Range(1,3).Contains(Math.Abs(curr - next)));
    }

    bool IsValidPart2(List<int> line)
    {
        if (IsValidPart1(line))
            return true;
        return line.VariationsWithoutOneElement().Any(IsValidPart1);
    }
}