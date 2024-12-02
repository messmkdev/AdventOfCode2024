public class Utils{
    public static Input ReadInputFile(int day, string colSeparator = " "){
        return new Input(File.ReadAllLines($"C:/Dev/advent of code/AdventOfCode/Day{day}/input.txt"), colSeparator);
    }    
}