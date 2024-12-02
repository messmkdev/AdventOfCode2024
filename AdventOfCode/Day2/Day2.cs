using System.Collections.Generic;

public class Day2
{
    public void Work()
    {
        Console.WriteLine("Hello, World from Day 2!");

        Data input = Utils.ReadInputFile(2);

        int nbValid = input.Rows.ToList().Count(IsValidPart2);

        Console.WriteLine($"Nb report valid = {nbValid}");

        Console.ReadLine();
    }

    bool IsValidPart1(List<int> line)
    {
        if (!line.IsOrdered())
            return false;
        return line.NextValidator((curr,next) => Math.Abs(curr - next) is >= 1 and <= 3);
    }

    bool IsValidPart2(List<int> line)
    {
        if (IsValidPart1(line))
            return true;
        return line.VariationsWithoutOneElement().Any(IsValidPart1);
    }
}