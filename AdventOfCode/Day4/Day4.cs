using System.Text.RegularExpressions;

public class Day4{

    public void Work(){
        var lines = File.ReadAllLines($"{Utils.GetPath()}/Day4/input.txt").ToList();
        PrintMatrix(lines);
        var sumLines = lines.Select(l => OccurenceOfXmas(l)).Sum();
        var sumReverseLines = lines.Select(l => OccurenceOfXmas(string.Join(string.Empty,l.Reverse()))).Sum();
        var diags = AllDiags(lines);
        Console.WriteLine("------------------------");
        PrintMatrix(diags);
        Console.WriteLine("------------------------");
        
    }

    public int OccurenceOfXmas(string toSearch){
        Regex searchXmas = new Regex(@"X+M+A+S");
        var matches = searchXmas.Matches(toSearch);
        return matches.Count;
    }

    void PrintMatrix(List<string> lines){
        foreach(var line in lines){
            Console.WriteLine(string.Join("",line.Select(c => c + " ")));
        }
    }

    public List<string> AllDiags(List<string> lines){
        List<string> diags = [.. DiagsLeftTopToBottomRigth(lines), ..DiagsLeftTopToBottomRigth(Rotate(lines))];
        return diags;
    }

    List<string> DiagsLeftTopToBottomRigth(List<string> lines){
        List<string> diags = new List<string>();
        for(var j = 0 ; j < lines[0].Count(); j++){
            List<char> diag = new List<char>();
            for(var i = 0 ; i < lines.Count - j; i++){
                diag.Add(lines[i][i + j]);
            }
            diags.Add(string.Join("",diag));
        }
        return diags;
    }

    List<string> DiagsTopRigthToBottomLeft(List<string> lines){
        List<string> diags = new List<string>();
        for(var j = lines[0].Count() - 1 ; j >= 0; j--){
            List<char> diag = new List<char>();
            for(var i = 0 ; i < lines.Count - j; i++){
                diag.Add(lines[i][i + j]);
            }
            diags.Add(string.Join("",diag));
        }
        return diags;
    }

    public List<string> Rotate(List<string> original){
        List<List<char>> rotated = new List<List<char>>();
        foreach(var ori in original){
            rotated.Add(new List<char>(ori.Select(o => ' ')));
        }

        for(var i = 0; i < original.Count(); i ++){
            for(var j = 0 ; j < original[i].Count(); j++){
                rotated[j][i] = original[i][j];
            }
        }

        return rotated.Select(r => string.Join("",r)).ToList();
    }
}