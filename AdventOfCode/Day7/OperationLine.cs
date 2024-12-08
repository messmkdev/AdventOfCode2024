using System.Reflection.Metadata.Ecma335;

public class OperationLine
{
    private static readonly string[] baseSigns = ["+", "*", "|"];
    static IDictionary<long, List<List<Func<long, long, long>>>> TransformationsMap = new Dictionary<long, List<List<Func<long, long, long>>>>();

    public long Resultat { get; set; }

    public List<long> Terms { get; set; }

    int NbSign;

    public OperationLine(string line)
    {
        var parts = line.Split(":");
        Resultat = long.Parse(parts[0]);
        Terms = parts[1].Trim().Split(' ').Select((t) => long.Parse(t.ToString())).ToList();
        NbSign = Terms.Count - 1;
    }

    public List<List<Func<long, long, long>>> GetTransformations(){
        if(TransformationsMap.ContainsKey(NbSign))
            return TransformationsMap[NbSign];
        
        var array = baseSigns.GetCombinations(NbSign).Select( s => string.Join("",s)).ToList();
        array.ForEach(a => Console.WriteLine(a));
        var transformation = array.Select(str => Transformation(str)).ToList();
        TransformationsMap.Add(NbSign, transformation);
        return transformation;
    }

    public bool IsValid(){
        var transformations = GetTransformations();
        return transformations.Any(t => IsValid(t));
    }

    bool IsValid(List<Func<long, long, long>> transformation, bool log =false){
        long acc = 0;
        if(log)
            Console.WriteLine($"Transformation {string.Join(" ",transformation.Select(t => t.Method.Name))}" );
        for(var i = 0 ; i< transformation.Count; i++){
            var actualTerm = acc != 0 ? acc : Terms[i];
            if(log)
                Console.WriteLine($"Doin {actualTerm} {transformation[i].Method.Name} {Terms[i + 1]}");
            acc = transformation[i](actualTerm, Terms[i + 1]);
            if(log)
                Console.WriteLine($"acc : {acc}");
        }
        if(log)
            Console.WriteLine($"final acc : {acc}");
        return acc == Resultat;
    }

    public List<Func<long, long, long>> Transformation(string opeationSequence)
    {
        List<Func<long, long, long>> transformation = new List<Func<long, long, long>>();
        for (var i = 0; i < opeationSequence.Count(); i++)
        {
            if (opeationSequence[i] == '+')
            {
                transformation.Add(Add);
            }
            else if(opeationSequence[i] == '*')
            {
                transformation.Add(Mul);
            }
            else if(opeationSequence[i] == '|'){
                transformation.Add(Concat);
            }
        }
        return transformation;
    }

    static int Factoriel(int n)
    {
        return n > 1 ? n * Factoriel(n - 1) : 1;
    }

    public long Mul(long factor1, long factor2)
    {
        return factor1 * factor2;
    }

    public long Add(long factor1, long factor2)
    {
        return factor1 + factor2;
    }

    public long Concat(long factor1, long factor2)
    {
        return long.Parse(factor1.ToString() + factor2.ToString());
    }

}