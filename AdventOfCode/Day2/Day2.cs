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
        var ordered = line.Order().SequenceEqual(line) || line.Order().Reverse().SequenceEqual(line);
        if (!ordered)
            return false;
        for (var i = 0; i < line.Count - 1; i++)
        {
            var diff = Math.Abs(line[i] - line[i + 1]);
            if (diff < 1 || diff > 3)
                return false;
        }
        return true;
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