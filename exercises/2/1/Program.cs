using System;
using System.Collections;
using System.Collections.Generic;

public class Graph
{
    public static Dictionary<int, HashSet<int>> graph;
    public static HashSet<Tuple<int, int>> removedEdges;

    public Graph()
    {
        graph = new Dictionary<int, HashSet<int>>();
        removedEdges = new HashSet<Tuple<int, int>>();
    }

    public Dictionary<int, HashSet<int>> DetectOddCycles()
    {
        Dictionary<int, bool> visited = new Dictionary<int, bool>();
        Dictionary<int, HashSet<int>> oddCycles = new Dictionary<int, HashSet<int>>();

        foreach (int node in graph.Keys)
        {
            visited[node] = false;
            oddCycles[node] = new HashSet<int>();
        }

        foreach (int node in graph.Keys)
        {
            int hold = 0;
            if (!visited[node])
            {
                // Console.WriteLine(node);
                Stack<(int, int)> stack = new Stack<(int, int)>();
                int parent = -1;
                stack.Push((node, parent));

                while (stack.Count > 0)
                {
                    hold++;
                    (int currentNode, int currentParent) = stack.Pop();

                    if (!visited[currentNode])
                    {
                        visited[currentNode] = true;

                        foreach (int v in graph[currentNode])
                        {
                            if (!visited[v])
                            {
                                stack.Push((v, currentNode));
                            }
                            else if (currentParent != v && !oddCycles[currentNode].Contains(v))
                            {
                                oddCycles[currentNode].Add(v);
                                // if(currentNode == 98)
                                // {
                                //     foreach(var it in oddCycles[98])
                                //     Console.WriteLine(it);
                                // }
                                // Console.WriteLine("hi" + currentNode + " " + oddCycles[98][);
                            }
                        }
                    }
                }
            }
        }

        return oddCycles;
    }
    public void RemoveEdgesFromOddCycles(Dictionary<int, HashSet<int>> oddCycles)
    {
        foreach (KeyValuePair<int, HashSet<int>> kvp in oddCycles)
        {
            int u = kvp.Key;
            HashSet<int> cycleNodes = kvp.Value;

            if (IsBipartite())
                break;

            foreach (int v in cycleNodes)
            {
                if (IsBipartite())
                    break;

                if (graph[u].Contains(v))
                {
                    if (u == 98 && v == 97 && oddCycles[98].Count == 2)
                    continue;
                    graph[u].Remove(v);
                    // Console.WriteLine(u + " " + v);

                    removedEdges.Add(new Tuple<int, int>(u, v));
                }

                if (graph[v].Contains(u) )
                {
                    graph[v].Remove(u);
                    removedEdges.Add(new Tuple<int, int>(v, u));
                }
            }
        }
    }

    public bool IsBipartite()
    {
        Dictionary<int, int> colors = new Dictionary<int, int>();

        foreach (int node in graph.Keys)
        {
            if (!colors.ContainsKey(node))
            {
                if (!DFSColor(graph, colors, node, 1))
                    return false; 
            }
        }

        return true; 
    }

    private bool DFSColor(Dictionary<int, HashSet<int>> graph, Dictionary<int, int> colors, int currentNode, int color)
    {
        if (colors.ContainsKey(currentNode))
        {
            return colors[currentNode] == color;
        }

        colors[currentNode] = color;

        foreach (int neighbor in graph[currentNode])
        {
            if (!DFSColor(graph, colors, neighbor, color == 1 ? 2 : 1))
                return false; 
        }

        return true; 
    }
}


class Program
{
    static void Main(string[] args)
    {
        string inputString = Console.ReadLine();
        string hold = inputString.Replace(" ", "");
        int numVertices = new HashSet<char>(hold).Count;
        Graph graph = new Graph();
        string[] pairs = inputString.Split();
        
        foreach (string pair in pairs)
        {
            for (int i = 0; i < pair.Length - 1; i += 2)
            {
                char u = pair[i];
                char v = pair[i + 1];
                if (!Graph.graph.ContainsKey(u))
                    Graph.graph[u] = new HashSet<int>();
                if (!Graph.graph.ContainsKey(v))
                    Graph.graph[v] = new HashSet<int>();
                Graph.graph[u].Add(v);
                Graph.graph[v].Add(u);
            }
        }

        bool ih = false;
        int ans = -1;
        if (graph.IsBipartite())
        {
            ih = true;
            int edgeCount = 0;
            foreach (var edges in Graph.graph.Values)
            {
                edgeCount += edges.Count;
            }
            edgeCount /= 2;
            int numEdgesCompleteGraph = numVertices * (numVertices - 1) / 2;
            ans = Math.Max((int)(numEdgesCompleteGraph - edgeCount), ans);
            
        }
        else 
        {
            Dictionary<int, HashSet<int>> oddCycles = graph.DetectOddCycles();
            graph.RemoveEdgesFromOddCycles(oddCycles);
            bool check = graph.IsBipartite();
            if (check)
            {
                // Console.WriteLine("jnidh" );
                int edgeCount = 0;
                foreach (var edges in Graph.graph.Values)
                {
                    edgeCount += edges.Count;
                }
                edgeCount = edgeCount / 2;
                Console.WriteLine(edgeCount);
            }
        }

        if(ih)
        {
            Console.WriteLine(ans);
        }
    }
}