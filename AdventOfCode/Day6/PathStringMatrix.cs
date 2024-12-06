public class PathStringMatrix : StringMatrix{

    public Tuple<int,int> GuardPos {get;set;}

    public PathStringMatrix(List<string> lines) : base(lines){
        GuardPos = GetPos(IsGuard);
        Console.WriteLine(string.Join("",GuardLoS()));
    }

    int GuardRow() => GuardPos.Item1;

    int GuardCol() => GuardPos.Item2;



    char[] guardChars = new []{'<','>','^','v'};

    public bool IsGuard(int r, int c){
        return guardChars.Contains(GetAt(r,c));
    }

    public bool IsGuard(char c){
        return guardChars.Contains(c);
    }

    public bool IsWall(int r, int c) => GetAt(r,c) == '#';

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

    List<char> GuardLoS(){
        var direction = GuardDirection();
        switch(direction){
            case Direction.UP: return this.Select( rows => rows[GuardCol()]).TakeWhile(c => !IsGuard(c)).Reverse().ToList();
            case Direction.DOWN: return this.Select( rows => rows[GuardCol()]).SkipWhile(c => !IsGuard(c)).ToList()[1..];
            case Direction.LEFT: return this[GuardRow()].TakeWhile(c => !IsGuard(c)).ToList();
            case Direction.RIGHT: return this[GuardRow()].SkipWhile(c => !IsGuard(c)).ToList()[1..];
        }
        return new List<char>();
    }

    public Direction GuardDirection(){
        return CharToDirection(GetAt(GuardRow(),GuardCol()));
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