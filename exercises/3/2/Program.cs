using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

class Graph
{
    private int V; // Number of vertices
    private List<int>[] adj; // Adjacency list
    public Dictionary<Tuple<int , int> , int> tekrari;

    int[] nmd;
    public Graph(int v)
    {
        V = v;
        adj = new List<int>[V];
        for (int i = 0; i < V; ++i)
            adj[i] = new List<int>();

        tekrari = new Dictionary<Tuple<int, int>, int>();
    }

    public void addEdge(int v, int w)
    {
        if(adj[v] != null && adj[v].Contains(w) && !tekrari.ContainsKey(new Tuple<int, int>(v , w)))
        {
            tekrari.Add(new Tuple<int, int>(v , w) , 0);
            tekrari.Add(new Tuple<int, int>(w , v) , 0);
        }
        adj[v].Add(w);
        adj[w].Add(v);
    }

    private void bridgeUtil(int u, bool[] visited, int[] disc, int[] low, int[] parent, ref int count)
    {
        int children = 0;
        visited[u] = true;
        disc[u] = low[u] = ++time;

        foreach (int v in adj[u])
        {
            if (!visited[v])
            {
                children++;
                parent[v] = u;
                bridgeUtil(v, visited, disc, low, parent, ref count);
                low[u] = Math.Min(low[u], low[v]);

                if (low[v] > disc[u])
                {
                    if(tekrari.ContainsKey(new Tuple<int, int>(v , u)))
                    {
                        continue;
                    }
                    // Console.WriteLine("bekdk" + v + " " +u);
                    else
                    count++;
                }
            }
            else if (v != parent[u])
                low[u] = Math.Min(low[u], disc[v]);
        }
    }

    public int CountCutEdges()
    {
        bool[] visited = new bool[V];
        int[] disc = new int[V];
        int[] low = new int[V];
        int[] parent = new int[V];
        int count = 0;

        for (int i = 0; i < V; i++)
        {
            parent[i] = -1;
            visited[i] = false;
        }

        for (int i = 0; i < V; i++)
            if (!visited[i])
                bridgeUtil(i, visited, disc, low, parent, ref count);

        return count;
    }

    static int time = 0;
}


class Program
{
    static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split(' ');
        int n = int.Parse(input[0]);
        int m = int.Parse(input[1]);
        Graph g = new Graph(n + 1);

        for (int i = 0; i < m; i++)
        {
            input = Console.ReadLine().Split(' ');
            int x = int.Parse(input[0]);
            int y = int.Parse(input[1]);
            g.addEdge(x , y);
            // if(g.tekrari.ContainsKey(new Tuple<int ,int>(x , y)))
            // {
            //     if(!g.jzz.ContainsKey(new Tuple<int ,int>(x , y)))
            //         g.jzz.Add(new Tuple<int, int>(x , y) , 1);
            // }
            // else
            // {
            //     g.tekrari.Add(new Tuple<int , int>(x ,y) , 0);
            // }
        }
        

        Console.WriteLine(g.CountCutEdges());
         
    }
}
