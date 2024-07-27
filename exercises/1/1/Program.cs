using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string[] nm = Console.ReadLine().Split();
        int n = int.Parse(nm[0]);
        int m = int.Parse(nm[1]);

        List<Edge> edges = new List<Edge>();
        for (int i = 0; i < m; i++)
        {
            string[] input = Console.ReadLine().Split();
            int u = int.Parse(input[0]);
            int v = int.Parse(input[1]);
            int w = int.Parse(input[2]);
            edges.Add(new Edge(u, v, w));
        }

        int minCost = FindMinimumSpanningTreeCost(edges, n);
        Console.WriteLine(minCost);
    }

    static int FindMinimumSpanningTreeCost(List<Edge> edges, int n)
    {
        edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));
        int[] parent = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            parent[i] = i;
        }

        int minCost = 0;
        foreach (Edge edge in edges)
        {
            int root1 = Find(parent, edge.Source);
            int root2 = Find(parent, edge.Destination);

            if (root1 != root2)
            {
                minCost += edge.Weight;
                Union(parent, root1, root2);
            }
        }

        return minCost;
    }

    static int Find(int[] parent, int node)
    {
        if (parent[node] != node)
        {
            parent[node] = Find(parent, parent[node]);
        }
        return parent[node];
    }

    static void Union(int[] parent, int root1, int root2)
    {
        parent[root2] = root1;
    }
}

class Edge
{
    public int Source { get; }
    public int Destination { get; }
    public int Weight { get; }

    public Edge(int source, int destination, int weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }
}
