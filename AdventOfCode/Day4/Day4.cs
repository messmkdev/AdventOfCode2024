using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Day4{

    Regex searchXmas = new Regex(@"XMAS");
    Regex searchMas = new Regex(@"MAS");

    public void Work(){
        var lines = File.ReadAllLines($"{Utils.GetPath()}/Day4/input.txt").ToList();
        PrintMatrix(lines);
        var sumHorizontal = lines.Select(l => OccurenceOfXmas(l)).Sum();
        var sumVertical = Rotate(lines).Select(l => OccurenceOfXmas(l)).Sum();
        
        var diags = Diags(lines);
        var sumDiags = diags.Select(l => OccurenceOfXmas(l)).Sum();

        var total = sumHorizontal + sumVertical + sumDiags;    

        Console.WriteLine("------------------------");
        Console.WriteLine($"Found {total} XMAS");

        Console.WriteLine("------------------------");
        
        int countXmas = 0;
        for(var i = 0 ; i < lines.Count() - 2; i ++){
            for(var j = 0 ; j < lines[0].Count() - 2; j++){
                var toSearch = new List<List<char>>
                {
                    lines[i].Substring(j, 3).ToList(),
                    lines[i + 1].Substring(j, 3).ToList(),
                    lines[i + 2].Substring(j, 3).ToList(),
                };
                if(IsXMAS(toSearch)){
                    //Console.WriteLine($"Found at {i + 1} {j + 1}");
                    //LogSearch(toSearch);
                    countXmas++;
                }
            }
        }
        Console.WriteLine($"Found {countXmas} X-MAS");
    }

    void LogSearch(List<List<char>> search){
        Console.WriteLine("------------");
        Console.WriteLine(string.Join("\n",search.Select(c => string.Join(" ",c))));
        Console.WriteLine("------------");
    }

    bool IsA(char c) => c == 'A';
    bool IsXMAS(List<List<char>> chars) {
        var relevant = (chars[0][0].ToString() + chars[2][0].ToString() + chars[0][2].ToString() + chars[2][2].ToString()).ToString();
        return relevant.Count(c => c == 'M') == 2 && relevant.Count(c => c == 'S') == 2 && IsA(chars[1][1]) && chars[0][0] != chars[2][2];
    }

    public List<Match> XMasMatch(string diag){
        return searchMas.Matches(diag).Where(m => m.Success).Concat(searchMas.Matches(string.Join("",diag.Reverse())).Where(m => m.Success)).ToList();
    }

    public int OccurenceOfXmas(string toSearch){
        var matches = searchXmas.Matches(toSearch);
        var matchesReverse = searchXmas.Matches(string.Join("",toSearch.Reverse()));
        return matches.Count + matchesReverse.Count;
    }

    void PrintMatrix(List<string> lines){
        foreach(var line in lines){
            Console.WriteLine(string.Join("",line.Select(c => c + " ")));
        }
    }

    List<string> Diags(List<string> lines){
        List<string> flatDiag = new List<string>();
        var nbRow = lines.Count();
        for(var i = 0 ; i < nbRow; i ++){
            flatDiag.Add(lines[i].PadLeft(nbRow + i).PadRight(nbRow*2));
        }

        List<string> diags = new List<string>();
        for(var i = 0; i < flatDiag.Max(l => l.Count()); i ++){
            diags.Add(string.Join(string.Empty,flatDiag.Select(r => r[i])).Trim());
        }

        List<string> flatDiag2 = new List<string>();
        for(var i = 0 ; i < nbRow; i ++){
            flatDiag2.Add(lines[i].PadRight(nbRow + i).PadLeft(nbRow*2));
        }
        for(var i = 0; i < flatDiag.Max(l => l.Count()); i ++){
            diags.Add(string.Join(string.Empty,flatDiag2.Select(r => r[i])).Trim());
        }
        
        return diags;
    }
    

    public List<string> Rotate(List<string> original){
        List<List<char>> rotated = new List<List<char>>();
        foreach(var ori in original)
            rotated.Add(new List<char>(ori.Select(o => ' ')));
        

        for(var i = 0; i < original.Count(); i ++)
            for(var j = 0 ; j < original[i].Count(); j++)
                rotated[j][i] = original[i][j];

        return rotated.Select(r => string.Join("",r)).ToList();
    }
}