public class StringMatrix : List<List<char>>
{
    public StringMatrix(List<string> input) : base(input.Select(i => i.ToList()).ToList()){

    }

    public char GetAt(int r, int c){
        return this[r][c];
    }
}