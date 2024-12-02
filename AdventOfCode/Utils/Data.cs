public class Data : List<string>{

    string colSeparator = " ";

    public List<List<int>> Rows;
    public List<List<int>> Cols;

    public Data(ICollection<string> values, string colSeparator) : base(values){
        this.colSeparator = colSeparator;
        this.Rows = GetRows();
        this.Cols = GetCols();
    }
    
    public List<int> GetRow(int row){
        return this.Rows[row];
    }
   
    public List<int> GetCol(int col){
        return this.Cols[col];
    }

    List<List<int>> GetCols(){
        List<List<int>> cols = [.. Enumerable.Range(0, Rows.Max(r => r.Count)).Select(c => new List<int>())];
        for(var i = 0 ; i < Rows.Count; i ++){
            var row = GetRow(i);
            for(var j = 0 ; j < row.Count; j ++){
                cols[j].Add(row[j]);
            }
        }
        return cols;
    }

    List<List<int>> GetRows(){
        return this.Select(r=> r.Split(colSeparator).Select(l => int.Parse(l)).ToList()).ToList();
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, this);
    }
}