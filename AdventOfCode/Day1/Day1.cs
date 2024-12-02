public class Day1
{
    public void Work()
    {
        Console.WriteLine("Hello, World from Day 1!");
        Input input = Utils.ReadInputFile(1);

        var cols = input.GetCols();
        var col0Ordered = cols[0].Order().ToList();
        var col1Ordered = cols[1].Order().ToList();

        var sum = 0;
        for (var i = 0; i < col0Ordered.Count; i++)
        {
            sum += Math.Abs(col1Ordered[i] - col0Ordered[i]);
        }

        Console.WriteLine($"Distance = {sum}");

        int similarity = 0;
        foreach (var number in col0Ordered)
        {
            similarity += number * col1Ordered.Count(v => v == number);
        }

        Console.WriteLine($"Similarity = {similarity}");

        Console.ReadLine();
    }
}