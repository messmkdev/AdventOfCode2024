public class Day7{

    public void Work(){
        var lines = Utils.ReadInputFileAsListString(7);
        var operationLines = lines.Select((l) => new OperationLine(l)).ToList();
        var sum = operationLines.Where(l => l.IsValid()).Sum( o => o.Resultat);
        Console.WriteLine($"Sum of valid test values {sum}");
    }
}