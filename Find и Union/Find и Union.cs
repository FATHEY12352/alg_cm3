using System;

class DisjointSet
{
    private int[] parent;
    private int[] rank;

    // Constructor: Initializes parent and rank arrays
    public DisjointSet(int size)
    {
        parent = new int[size];
        rank = new int[size];

        for (int i = 0; i < size; i++)
        {
            parent[i] = i; // Each element is its own parent initially
            rank[i] = 0;   // Initial rank of all trees is 0
        }
    }

    // Find operation with path compression
    public int Find(int x)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]); // Recursively update the parent
        }
        return parent[x];
    }

    // Union operation with rank optimization
    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
            if (rank[rootX] > rank[rootY])
            {
                parent[rootY] = rootX;
            }
            else if (rank[rootX] < rank[rootY])
            {
                parent[rootX] = rootY;
            }
            else
            {
                parent[rootY] = rootX;
                rank[rootX]++;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        DisjointSet ds = new DisjointSet(10); // Create 10 elements: 0–9

        // Union operations
        ds.Union(1, 2);
        ds.Union(2, 3);
        ds.Union(4, 5);
        ds.Union(6, 7);
        ds.Union(3, 7);

        // Find operations
        Console.WriteLine(ds.Find(1)); // Should output the same root for all connected elements
        Console.WriteLine(ds.Find(3)); // Example: 7
        Console.WriteLine(ds.Find(5)); // Another root, example: 5

        // Connectivity check
        Console.WriteLine(ds.Find(1) == ds.Find(7)); // True
        Console.WriteLine(ds.Find(4) == ds.Find(6)); // False
    }
}

