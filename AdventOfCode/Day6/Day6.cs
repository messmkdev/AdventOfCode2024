public class Day6{

    public void Work(){
            var lines = Utils.ReadInputFileAsListString(6);
            //lines.PrintMatrix();

            
            PathStringMatrix path = new PathStringMatrix(lines);
            WalkPath(path);

            var positionVisited = path.PositionVisited();
            positionVisited.RemoveAt(0);

            List<PathStringMatrix> possiblesVariations = new List<PathStringMatrix>();

            foreach(var posVisited in positionVisited){
                var copy = new PathStringMatrix(lines);
                copy[posVisited.Item1][posVisited.Item2] = '#';
                possiblesVariations.Add(copy);
            }
            var nbVariations = possiblesVariations.Count();
            var count = possiblesVariations.Count(p => IsLoop(p));

            Console.WriteLine("----------------");
            Console.WriteLine($"total guard path length {path.visited.Count()}");

    }

    public void WalkPath(PathStringMatrix paths){
        while(paths.GuardLoS().Contains('#')){
            //paths.LogLos();
            paths.WalkLos();
            //paths.LogMatrix();
        }
        //paths.LogLos();
        paths.WalkLos();
        //paths.LogMatrix();
    }

    public bool IsLoop(PathStringMatrix paths){
        int iterationMax = 10000;
        int iteration = 0;
        while(paths.GuardLoS().Contains('#') && iteration <= iterationMax){
            //paths.LogLos();
            paths.WalkLos();
            //paths.LogMatrix();
            iteration++;
        }
        if(iteration == iterationMax + 1)
            return true;
        return false;
    }
}