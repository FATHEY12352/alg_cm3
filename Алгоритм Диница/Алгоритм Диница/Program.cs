using System;
using System.Collections.Generic;

class DinicAlgorithm
{
    private readonly int[,] capacity;
    private readonly List<int>[] adjList;
    private readonly int[] level;
    private readonly int nodeCount;

    public DinicAlgorithm(int nodeCount)
    {
        this.nodeCount = nodeCount;
        capacity = new int[nodeCount, nodeCount];
        adjList = new List<int>[nodeCount];
        level = new int[nodeCount];

        for (int i = 0; i < nodeCount; i++)
            adjList[i] = new List<int>();
    }

    public void AddEdge(int from, int to, int cap)
    {
        adjList[from].Add(to);
        adjList[to].Add(from);
        capacity[from, to] += cap;
    }

    private bool BuildLevelGraph(int source, int sink)
    {
        Array.Fill(level, -1);
        level[source] = 0;
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            foreach (int next in adjList[current])
            {
                if (level[next] < 0 && capacity[current, next] > 0)
                {
                    level[next] = level[current] + 1;
                    queue.Enqueue(next);
                }
            }
        }

        return level[sink] >= 0;
    }

    private int SendFlow(int node, int flow, int sink, int[] start)
    {
        if (node == sink)
            return flow;

        for (; start[node] < adjList[node].Count; start[node]++)
        {
            int next = adjList[node][start[node]];

            if (level[next] == level[node] + 1 && capacity[node, next] > 0)
            {
                int currentFlow = Math.Min(flow, capacity[node, next]);
                int tempFlow = SendFlow(next, currentFlow, sink, start);

                if (tempFlow > 0)
                {
                    capacity[node, next] -= tempFlow;
                    capacity[next, node] += tempFlow;
                    return tempFlow;
                }
            }
        }

        return 0;
    }

    public int MaxFlow(int source, int sink)
    {
        int totalFlow = 0;

        while (BuildLevelGraph(source, sink))
        {
            int[] start = new int[nodeCount];
            while (true)
            {
                int flow = SendFlow(source, int.MaxValue, sink, start);
                if (flow == 0)
                    break;
                totalFlow += flow;
            }
        }

        return totalFlow;
    }
}

class Program
{
    static void Main()
    {
        DinicAlgorithm dinic = new DinicAlgorithm(6);
        dinic.AddEdge(0, 1, 10);
        dinic.AddEdge(0, 2, 10);
        dinic.AddEdge(1, 2, 2);
        dinic.AddEdge(1, 3, 4);
        dinic.AddEdge(1, 4, 8);
        dinic.AddEdge(2, 4, 9);
        dinic.AddEdge(3, 5, 10);
        dinic.AddEdge(4, 3, 6);
        dinic.AddEdge(4, 5, 10);

        Console.WriteLine("Maximum Flow: " + dinic.MaxFlow(0, 5));
    }
}
