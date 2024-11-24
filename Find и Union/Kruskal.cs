using System;
using System.Collections.Generic;

class DisjointSet
{
    private int[] parent;
    private int[] rank;

    public DisjointSet(int size)
    {
        parent = new int[size];
        rank = new int[size];
        for (int i = 0; i < size; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
    }

    public int Find(int x)
    {
        if (parent[x] != x)
            parent[x] = Find(parent[x]); // Path compression
        return parent[x];
    }

    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
            if (rank[rootX] > rank[rootY])
                parent[rootY] = rootX;
            else if (rank[rootX] < rank[rootY])
                parent[rootX] = rootY;
            else
            {
                parent[rootY] = rootX;
                rank[rootX]++;
            }
        }
    }
}

class Edge : IComparable<Edge>
{
    public int From { get; }
    public int To { get; }
    public int Weight { get; }

    public Edge(int from, int to, int weight)
    {
        From = from;
        To = to;
        Weight = weight;
    }

    public int CompareTo(Edge? other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));
        return Weight.CompareTo(other.Weight);
    }
}

class KruskalMST
{
    public static List<Edge> FindMST(int vertices, List<Edge> edges)
    {
        edges.Sort(); // Sort edges by weight
        DisjointSet ds = new DisjointSet(vertices);

        List<Edge> mst = new List<Edge>();
        foreach (var edge in edges)
        {
            if (ds.Find(edge.From) != ds.Find(edge.To))
            {
                mst.Add(edge);
                ds.Union(edge.From, edge.To);
            }
        }
        return mst;
    }
}

class Program
{
    static void Main()
    {
        int vertices = 5;
        List<Edge> edges = new List<Edge>
        {
            new Edge(0, 1, 10),
            new Edge(0, 2, 6),
            new Edge(0, 3, 5),
            new Edge(1, 3, 15),
            new Edge(2, 3, 4)
        };

        List<Edge> mst = KruskalMST.FindMST(vertices, edges);

        Console.WriteLine("Minimum Spanning Tree:");
        foreach (var edge in mst)
        {
            Console.WriteLine($"Edge: ({edge.From}, {edge.To}) - Weight: {edge.Weight}");
        }
    }
}
