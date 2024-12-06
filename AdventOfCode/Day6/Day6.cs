using System.Diagnostics;

public class Day6
{

    public void Work()
    {
        var lines = Utils.ReadInputFileAsListString(6);
        //lines.PrintMatrix();


        PathStringMatrix path = new PathStringMatrix(lines);
        WalkPath(path);

        var positionVisited = path.PositionVisited();
        positionVisited.RemoveAt(0);

        List<PathStringMatrix> possiblesVariations = new List<PathStringMatrix>();

        foreach (var posVisited in positionVisited)
        {
            var copy = new PathStringMatrix(lines);
            copy[posVisited.Item1][posVisited.Item2] = '#';
            possiblesVariations.Add(copy);
        }

        int cpt = 0;
        Console.WriteLine("----------------");       
        Stopwatch s = Stopwatch.StartNew(); 
        Parallel.For(0, possiblesVariations.Count(), (i) =>
        {
            if (IsLoop(possiblesVariations[i]))
            {
                if(i % 50 == 0){
                    Console.WriteLine($"Already found {cpt}");            
                }
                Interlocked.Increment(ref cpt);
            }
        });
        s.Stop();
        Console.WriteLine($"Found {cpt} variations with infinite loop in {s.Elapsed.Duration().ToString()}" );

        Console.WriteLine("----------------");
        Console.WriteLine($"total guard path length {path.visited.Count()}");

    }

    public void WalkPath(PathStringMatrix paths, bool log = false)
    {
        while (paths.GuardLoS().Contains('#'))
        {
            if(log)
                paths.LogLos();
            paths.WalkLos();
            if(log)
                paths.LogMatrix();
        }
        if(log)
            paths.LogLos();
        paths.WalkLos();
        if(log)
            paths.LogMatrix();
    }

    public bool IsLoop(PathStringMatrix paths)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        int iterationMax = 400;
        int iteration = 0;
        while (paths.GuardLoS().Contains('#') && iteration <= iterationMax)
        {
            paths.WalkLos();
            iteration++;
        }
        stopwatch.Stop();

        if (iteration == iterationMax + 1)
        {
            return true;
        }
        return false;
    }
}