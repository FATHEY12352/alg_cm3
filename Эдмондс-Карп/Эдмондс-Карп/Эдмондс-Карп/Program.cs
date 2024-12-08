using System;
using System.Collections.Generic;

class EdmondsKarp
{
    private int[,] capacity;
    private int[,] flow;
    private int[] parent;
    private int nodeCount;

    public EdmondsKarp(int nodeCount)
    {
        this.nodeCount = nodeCount;
        capacity = new int[nodeCount, nodeCount];
        flow = new int[nodeCount, nodeCount];
        parent = new int[nodeCount];
    }

    public void AddEdge(int from, int to, int capacityValue)
    {
        capacity[from, to] = capacityValue;
    }

    private bool BFS(int source, int sink)
    {
        Array.Fill(parent, -1);
        var visited = new bool[nodeCount];
        var queue = new Queue<int>();
        queue.Enqueue(source);
        visited[source] = true;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            for (int next = 0; next < nodeCount; next++)
            {
                if (!visited[next] && capacity[current, next] - flow[current, next] > 0)
                {
                    parent[next] = current;
                    visited[next] = true;
                    queue.Enqueue(next);
                    if (next == sink) return true;
                }
            }
        }
        return false;
    }

    public int MaxFlow(int source, int sink)
    {
        int maxFlow = 0;
        while (BFS(source, sink))
        {
            int pathFlow = int.MaxValue;
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, capacity[u, v] - flow[u, v]);
            }
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                flow[u, v] += pathFlow;
                flow[v, u] -= pathFlow;
            }
            maxFlow += pathFlow;
        }
        return maxFlow;
    }
}

class Program
{
    static void Main()
    {
        var ek = new EdmondsKarp(6);
        ek.AddEdge(0, 1, 16);
        ek.AddEdge(0, 2, 13);
        ek.AddEdge(1, 2, 10);
        ek.AddEdge(1, 3, 12);
        ek.AddEdge(2, 4, 14);
        ek.AddEdge(3, 2, 9);
        ek.AddEdge(3, 5, 20);
        ek.AddEdge(4, 3, 7);
        ek.AddEdge(4, 5, 4);
        Console.WriteLine("Maximum Flow: " + ek.MaxFlow(0, 5));
    }
}
