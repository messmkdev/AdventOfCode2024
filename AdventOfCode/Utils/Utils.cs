
using System.Reflection;

public class Utils{


    public static string GetPath(){
        var location = Assembly.GetCallingAssembly().Location.Split(@"\");
        var path = Path.Combine(location.TakeWhile(s => s != "bin").ToArray());;
        return path;
    }

    public static Data ReadInputFile(int day, string colSeparator = " ", bool sample = false){
        var file = sample ? "sample.txt" : "input.txt";
        return new Data(File.ReadAllLines($"{GetPath()}/Day{day}/input.txt"), colSeparator);
    }

    internal static string ReadInputFileAsString(int day, bool sample = false)
    {
        var file = sample ? "sample.txt" : "input.txt";
        return File.ReadAllText($"{GetPath()}/Day{day}/input.txt");
    }

    internal static List<string> ReadInputFileAsListString(int day, bool sample = false)
    {  
        var file = sample ? "sample.txt" : "input.txt";
        return File.ReadAllLines($"{GetPath()}/Day{day}/{file}").ToList();
    }
}