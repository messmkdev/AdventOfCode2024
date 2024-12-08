using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

public class ObjectOnGrid
{

    public char Symbol { get; }
    public int R { get; }
    public int C { get; }

    public ObjectOnGrid(char symbol, int r, int c)
    {
        Symbol = symbol;
        R = r;
        C = c;
    }

    public bool SameRC(ObjectOnGrid objectOnGrid)
    {
        return objectOnGrid.C == C && objectOnGrid.R == R;
    }


}

public class ObjectOnGridEqualityComparer : IEqualityComparer<ObjectOnGrid>
{

    public bool Equals(ObjectOnGrid? x, ObjectOnGrid? y)
    {
        return x.R == y.R && x.C == y.C;
    }

    public int GetHashCode([DisallowNull] ObjectOnGrid obj)
    {
        return obj.R * 100 + obj.C * 100;
    }
}