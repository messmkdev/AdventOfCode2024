public class Day6{

    public void Work(){
            var lines = Utils.ReadInputFileAsListString(6);
            lines.PrintMatrix();

            PathStringMatrix paths = new PathStringMatrix(lines);
            while(paths.GuardLoS().Contains('#')){
                paths.LogLos();
                paths.WalkLos();
                paths.LogMatrix();
            }
            paths.LogLos();
            paths.WalkLos();
            paths.LogMatrix();

            Console.WriteLine("----------------");
            Console.WriteLine($"total guard path length {paths.visited.Count()}");
           

            /*LoS los;
            while((los = paths.GuardLoS()).Contains('#')){
                if(los.Direction == Direction.UP){
                    los.ForEach((c)=> c = c != '#' ? 'X' : '.');
                }
            }*/

    }

    


}