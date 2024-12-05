
using System.Reflection;

public class Utils{


    public static string GetPath(){
        var location = Assembly.GetCallingAssembly().Location.Split(@"\");
        var path = Path.Combine(location.TakeWhile(s => s != "bin").ToArray());;
        return path;
    }

    public static Data ReadInputFile(int day, string colSeparator = " "){
        
        return new Data(File.ReadAllLines($"{GetPath()}/Day{day}/input.txt"), colSeparator);
    }

    internal static string ReadInputFileAsString(int day)
    {
        return File.ReadAllText($"{GetPath()}/Day{day}/input.txt");
    }

    internal static List<string> ReadInputFileAsListString(int day)
    {
        return File.ReadAllLines($"{GetPath()}/Day{day}/input.txt").ToList();
    }
}