public class Utils{
    public static Data ReadInputFile(int day, string colSeparator = " "){
        return new Data(File.ReadAllLines($"C:/Dev/advent of code/AdventOfCode/Day{day}/input.txt"), colSeparator);
    }    
}