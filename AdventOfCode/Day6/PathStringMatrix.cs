public class PathStringMatrix : StringMatrix{

    public Tuple<int,int> GuardPos {get;set;}

    public PathStringMatrix(List<string> lines) : base(lines){
        GuardPos = GetPos(IsGuard);
    }

    public int GuardRow => GuardPos.Item1;

    public int GuardCol => GuardPos.Item2;

    char[] guardChars = new []{'<','>','^','v'};

    public bool IsGuard(int r, int c){
        return guardChars.Contains(GetAt(r,c));
    }

    public bool IsGuard(char c){
        return guardChars.Contains(c);
    }

    public bool IsWall(int r, int c) => IsWall(GetAt(r,c));

    public bool IsWall(char c) => c == '#';

    public void Walk(int r, int c) => this[r][c] = 'X';

    public Tuple<int,int>? GetPos(Func<int,int,bool> loop){
        for(var i = 0 ; i < this.Count; i++){
            for(var j = 0 ; j < this[i].Count; j++){
                if(loop(i,j)){
                    return new Tuple<int,int>(i,j);
                }
            }
        }
        return null;
    }

    public LoS GuardLoS(){
        var direction = GuardDirection();
        switch(direction){
            case Direction.UP: return new LoS(direction, this.Select( rows => rows[GuardCol]).TakeWhile(c => !IsGuard(c)).Reverse().ToList());
            case Direction.DOWN: return new LoS(direction, this.Select( rows => rows[GuardCol]).SkipWhile(c => !IsGuard(c)).ToList()[1..]);
            case Direction.LEFT: return new LoS(direction, this[GuardRow].TakeWhile(c => !IsGuard(c)).Reverse().ToList());
            case Direction.RIGHT: return new LoS(direction, this[GuardRow].SkipWhile(c => !IsGuard(c)).ToList()[1..]);
        }
        return new LoS(Direction.UP, new List<char>());
    }

    public HashSet<string> visited = new HashSet<string>();

    public void WalkLos(){
        var los = GuardLoS();
        bool hasWall = los.Contains('#');
        if(los.Direction == Direction.UP){
            var verticalDistanceToWall = Math.Abs(GuardRow - los.TakeWhile(c => !IsWall(c)).Count());
            for(var i = GuardRow ; i > verticalDistanceToWall; i--){
                this[i][GuardCol] = 'X';
                visited.Add((i).ToString()+"|"+GuardCol.ToString());
            }
            this[verticalDistanceToWall][GuardCol] = NextDirection(los);
            
        }
        if(los.Direction == Direction.DOWN){
            var verticalDistanceToWall = Math.Abs(GuardRow + los.TakeWhile(c => !IsWall(c)).Count());
            for(var i = GuardRow ; i < verticalDistanceToWall; i++){
                this[i][GuardCol] = 'X';
                visited.Add((i).ToString()+"|"+GuardCol.ToString());
            }
            this[verticalDistanceToWall][GuardCol] = NextDirection(los);
        }
        if(los.Direction == Direction.LEFT){
            var horizontalDistanceToWall = Math.Abs(GuardCol - los.TakeWhile(c => !IsWall(c)).Count());
            for(var i = GuardCol ; i > horizontalDistanceToWall ; i--){
                this[GuardRow][i] = 'X';
                visited.Add(GuardRow.ToString()+"|"+i.ToString());
            }
            this[GuardRow][horizontalDistanceToWall] = NextDirection(los);
        }
        if(los.Direction == Direction.RIGHT){
            var horizontalDistanceToWall = Math.Abs(GuardCol + los.TakeWhile(c => !IsWall(c)).Count());
            for(var i = GuardCol ; i < horizontalDistanceToWall ; i++){
                this[GuardRow][i] = 'X';
                visited.Add(GuardRow.ToString()+"|"+i.ToString());
            }
            this[GuardRow][horizontalDistanceToWall] = NextDirection(los);
        }
        
        GuardPos = GetPos(IsGuard);
        if(!hasWall){
            this[GuardRow][GuardCol] = 'X';
            visited.Add("final");
        }
    }

    public char NextDirection(LoS los){
        if(los.Direction == Direction.UP) return '>';
        if(los.Direction == Direction.RIGHT) return 'v';
        if(los.Direction == Direction.DOWN) return '<';
        if(los.Direction == Direction.LEFT) return '^';
        return ' ';
    }

    public void LogLos(){
        Console.WriteLine("-------------------------");
        Console.WriteLine("Line of sigth");
        Console.WriteLine(string.Join("",GuardLoS()));
    }

    public void LogMatrix(){
        Console.WriteLine("-------------------------");
        this.Select(r => string.Join("",r.ToList())).ToList().PrintMatrix();
    }

    public Direction GuardDirection(){
        return CharToDirection(GetAt(GuardRow,GuardCol));
    }

    public Direction CharToDirection(char c){
        switch(c){
            case '^': return Direction.UP;
            case 'v': return Direction.DOWN;
            case '>': return Direction.RIGHT;
            case '<': return Direction.LEFT;
        }
        return Direction.UP;
    }

}

public enum Direction{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class LoS : List<char>{
    public Direction Direction{get;set;}
     
    public LoS(Direction direction, List<char> los) : base(los){
        Direction = direction;
    }
}