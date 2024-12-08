using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;

public class AntennaGrid : List<List<char>>
{

    public IDictionary<char, List<Antenna>> Antennas = new Dictionary<char, List<Antenna>>();

    public List<AntiNode> AntiNodes = new List<AntiNode>();

    public AntennaGrid(List<string> lines) : base(lines.Select(l => l.Select(c => c).ToList()).ToList())
    {
        SearchAntenna();
        //this.LogMatrix();
        var antinodes = CalculateAllAntiNodesPart2();
        //this.LogMatrix();
        var countDistinct = antinodes.Distinct(new ObjectOnGridEqualityComparer()).Count();
        var antennaCount = Antennas.Where(a => a.Value.Count > 1).Sum(a => a.Value.Count);
        var uniqueAntinode = this.Sum(line => line.Count(c => c == '#'));
        var countPart2 = uniqueAntinode + antennaCount;
        
        Console.WriteLine($"Part1 Distinct antinodes count {countDistinct}");
        Console.WriteLine($"Part 2antinodes count {countPart2}");
    }

    public List<AntiNode> CalculateAllAntiNodes()
    {
        List<AntiNode> AllAntiNodes = new List<AntiNode>();
        foreach (var antennasKey in Antennas.Keys)
            AllAntiNodes.AddRange(CalculateAntiNodesOfAntennaPart1(Antennas[antennasKey]));
        return AllAntiNodes;
    }

    public List<AntiNode> CalculateAllAntiNodesPart2()
    {
        List<AntiNode> AllAntiNodes = new List<AntiNode>();
        foreach (var antennasKey in Antennas.Keys)
            AllAntiNodes.AddRange(CalculateAntiNodesOfAntennaPart2(Antennas[antennasKey]));
        LogMatrix();
        return AllAntiNodes;
    }

    public List<AntiNode> CalculateAntiNodesOfAntennaPart1(List<Antenna> antennas)
    {
        List<AntiNode> antiNodes = new List<AntiNode>();
        for (var i = 0; i < antennas.Count; i++)
        {
            for (var j = i + 1; j < antennas.Count; j++)
            {
                var antenna1 = antennas[i];
                var antenna2 = antennas[j];
                var antinodes = AntiNodesFromAntennas(antenna1, antenna2);
                foreach (var antin in antinodes.Where((a) => IsEmpty(a)))
                {
                    this[antin.R][antin.C] = '#';
                }
                antiNodes.AddRange(antinodes);
            }
        }
        //this.LogMatrix();
        return antiNodes;
    }

    private List<AntiNode> AntiNodesFromAntennas(Antenna antenna1, Antenna antenna2)
    {
        var antinodes = new List<AntiNode>();
        Console.WriteLine($"Match antennas {antenna1.Symbol} {antenna1.R},{antenna1.C} with {antenna2.Symbol} {antenna2.R},{antenna2.C}");
        var rowDiff = Math.Abs(antenna1.R - antenna2.R);
        var colDiff = Math.Abs(antenna1.C - antenna2.C);

        var antiNodeP1R = antenna1.R > antenna2.R ? antenna1.R + rowDiff : antenna1.R - rowDiff;
        var antiNodeP1C = antenna1.C > antenna2.C ? antenna1.C + colDiff : antenna1.C - colDiff;
        var antiNodeP2R = antenna2.R < antenna1.R ? antenna2.R - rowDiff : antenna2.R + rowDiff;
        var antiNodeP2C = antenna2.C < antenna1.C ? antenna2.C - colDiff : antenna2.C + colDiff;
        var antiNodeP1 = new AntiNode('#', antiNodeP1R, antiNodeP1C);
        var antiNodeP2 = new AntiNode('#', antiNodeP2R, antiNodeP2C);
        if (IsInBounds(antiNodeP1))
        {
            antinodes.Add(antiNodeP1);
        }
        if (IsInBounds(antiNodeP2))
        {
            antinodes.Add(antiNodeP2);
        }

        return antinodes;
    }

    private List<AntiNode> AntiNodesFromAntennasPart2(Antenna antenna1, Antenna antenna2)
    {
        var antinodes = new List<AntiNode>();
        Console.WriteLine($"Match antennas {antenna1.Symbol} {antenna1.R},{antenna1.C} with {antenna2.Symbol} {antenna2.R},{antenna2.C}");
        var rowDiff = Math.Abs(antenna1.R - antenna2.R);
        var colDiff = Math.Abs(antenna1.C - antenna2.C);
        for (var i = 1; i < Math.Max(this.Count / rowDiff, this.First().Count / colDiff); i++)
        {
            var antiNodeP1R = antenna1.R > antenna2.R ? antenna1.R + (rowDiff *i) : antenna1.R - (rowDiff *i);
            var antiNodeP1C = antenna1.C > antenna2.C ? antenna1.C + (colDiff *i) : antenna1.C - (colDiff *i);
            var antiNodeP2R = antenna2.R < antenna1.R ? antenna2.R - (rowDiff *i) : antenna2.R + (rowDiff *i);
            var antiNodeP2C = antenna2.C < antenna1.C ? antenna2.C - (colDiff *i) : antenna2.C + (colDiff *i);
            var antiNodeP1 = new AntiNode('#', antiNodeP1R, antiNodeP1C);
            var antiNodeP2 = new AntiNode('#', antiNodeP2R, antiNodeP2C);
            if (IsInBounds(antiNodeP1))
            {
                antinodes.Add(antiNodeP1);
            }
            if (IsInBounds(antiNodeP2))
            {
                antinodes.Add(antiNodeP2);
            }
        }
        return antinodes;
    }

    public List<AntiNode> CalculateAntiNodesOfAntennaPart2(List<Antenna> antennas)
    {
        List<AntiNode> antiNodes = new List<AntiNode>();
        for (var i = 0; i < antennas.Count; i++)
        {
            for (var j = i + 1; j < antennas.Count; j++)
            {
                var antenna1 = antennas[i];
                var antenna2 = antennas[j];
                var antinodes = AntiNodesFromAntennasPart2(antenna1, antenna2);
                foreach (var antin in antinodes.Where((a) => IsEmpty(a)))
                {
                    this[antin.R][antin.C] = '#';
                }
                antiNodes.AddRange(antinodes);
            }
        }
        //this.LogMatrix();
        return antiNodes;
    }

    public void SearchAntenna()
    {
        for (var i = 0; i < this.Count; i++)
        {
            for (var j = 0; j < this[i].Count(); j++)
            {
                var currSymbol = this[i][j];
                var antenna = new Antenna(currSymbol, i, j);
                if (currSymbol != '.')
                {
                    if (Antennas.ContainsKey(currSymbol))
                    {
                        Antennas[currSymbol].Add(antenna);
                    }
                    else
                    {
                        Antennas.Add(currSymbol, new List<Antenna>() { antenna });
                    }
                }
            }
        }
    }


    public bool IsInBounds(int r, int c) => r >= 0 && r < this.Count() && c < this[r].Count() && c >= 0;

    public bool IsInBounds(ObjectOnGrid objectOnGrid) => IsInBounds(objectOnGrid.R, objectOnGrid.C);

    public bool IsEmpty(ObjectOnGrid objectOnGrid) => this[objectOnGrid.R][objectOnGrid.C] == '.';

    public void LogMatrix()
    {
        Console.WriteLine("-------------------------");
        this.Select(r => string.Join("", r.ToList())).ToList().PrintMatrix();
    }
}