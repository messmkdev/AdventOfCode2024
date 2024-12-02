public class Input : List<string>{

    string colSeparator = " ";

    public Input(ICollection<string> values, string colSeparator) : base(values){
        this.colSeparator = colSeparator;
    }
    public List<int> GetRow(int row){
        return this[row].Split(colSeparator).Select(i => int.Parse(i.ToString())).ToList<int>();
    }

    public List<int> GetCol(int col){
        return this.Select(i => int.Parse(i.Split(colSeparator)[col])).ToList<int>();
    }

    public List<List<int>> GetCols(){
        List<List<int>> cols = new List<List<int>>();
        var nbCol = this[0].Split(colSeparator).Count();
        for(var i = 0; i < nbCol; i++){
            cols.Add(GetCol(i));
        }
        return cols;
    }

    public List<List<int>> GetRows(){
        List<List<int>> rows = new List<List<int>>();
        foreach(var line in this.Select(r=> r.Split(colSeparator))){
            rows.Add(line.Select(l => int.Parse(l)).ToList());
        }
        return rows;
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, this);
    }
}