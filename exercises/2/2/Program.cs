using System;
using System.Collections.Generic;

public class GraphMatching
{
    static List<List<char>> graph = new List<List<char>>();

    public static void AddEdge(char vertex1, char vertex2)
    {
        while (graph.Count <= vertex1)
            graph.Add(new List<char>());
        while (graph.Count <= vertex2)
            graph.Add(new List<char>());
        
        graph[vertex1].Add(vertex2);
        graph[vertex2].Add(vertex1);
    }

    public static int FindMaximumMatchingEdges(string inputString)
    {
        var splittedInputString = inputString.Split();

        foreach (var part in splittedInputString)
        {
            for (int i = 0; i < part.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    AddEdge(part[i], part[i + 1]);
                }
            }
        }

        int maxMatching = 0;
        var matchR = new char[graph.Count];
        Array.Fill(matchR, '-');

        for (int vertex = 0; vertex < graph.Count; vertex++)
        {
            var visitedVertices = new bool[graph.Count];
            if (FindAugmentingPath((char)vertex, matchR, visitedVertices))
            {
                maxMatching++;
            }
        }

        return maxMatching / 2;
    }

    public static bool FindAugmentingPath(char startingVertex, char[] matchR, bool[] visitedVertices)
    {
        foreach (var neighborVertex in graph[startingVertex])
        {
            if (!visitedVertices[neighborVertex])
            {
                visitedVertices[neighborVertex] = true;
                if (matchR[neighborVertex] == '-' || FindAugmentingPath(matchR[neighborVertex], matchR, visitedVertices))
                {
                    matchR[neighborVertex] = startingVertex;
                    return true;
                }
            }
        }
        return false;
    }


    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        Console.WriteLine(FindMaximumMatchingEdges(input));
    }
}
