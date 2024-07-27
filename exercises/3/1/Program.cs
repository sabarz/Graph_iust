using System;
using System.Collections.Generic;

public class GraphSolver
{
    private int verticesCount;
    private List<int>[] adjacencyList;

    public GraphSolver(int verticesCount)
    {
        this.verticesCount = verticesCount;
        adjacencyList = new List<int>[verticesCount];
        for (int i = 0; i < verticesCount; ++i)
            adjacencyList[i] = new List<int>();
    }

    public void AddEdge(int v, int w)
    {
        adjacencyList[v].Add(w);
        adjacencyList[w].Add(v);
    }

    private bool IsConnected()
    {
        bool[] visited = new bool[verticesCount];
        Stack<int> stack = new Stack<int>();
        int i;
        for (i = 0; i < verticesCount; i++)
            visited[i] = false;

        for (i = 0; i < verticesCount; i++)
            if (adjacencyList[i].Count != 0)
                break;

        if (i == verticesCount)
            return true;

        stack.Push(i);
        while (stack.Count > 0)
        {
            int current = stack.Pop();
            if (!visited[current])
            {
                visited[current] = true;
                foreach (int neighbor in adjacencyList[current])
                {
                    if (!visited[neighbor])
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }

        for (i = 0; i < verticesCount; i++)
            if (visited[i] == false && adjacencyList[i].Count > 0)
                return false;

        return true;
    }

    private int GetEulerianStatus()
    {
        if (IsConnected() == false)
            return 0;

        int oddDegreeVertices = 0;
        for (int i = 0; i < verticesCount; i++)
            if (adjacencyList[i].Count % 2 != 0)
                oddDegreeVertices++;

        if (oddDegreeVertices > 2)
            return 0;

        return (oddDegreeVertices == 2) ? 1 : 2;
    }

    public void TestEulerian()
    {
        int result = GetEulerianStatus();
        if (result == 0)
            Console.WriteLine("NO");
        else
            Console.WriteLine("YES");
    }

    public static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split(' ');
        int vertices = int.Parse(input[0]);
        int edges = int.Parse(input[1]);
        GraphSolver solver = new GraphSolver(vertices + 1);
        for (int i = 0; i < edges; i++)
        {
            input = Console.ReadLine().Split(' ');
            int x = int.Parse(input[0]);
            int y = int.Parse(input[1]);
            solver.AddEdge(x, y);
        }

        solver.TestEulerian();
    }
}
