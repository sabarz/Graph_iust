using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        List<int>[] tree = new List<int>[n + 1];
        
        for (int i = 0; i <= n; i++)
        {
            tree[i] = new List<int>();
        }

        for (int i = 0; i < n - 1; i++)
        {
            string[] input = Console.ReadLine().Split();
            int u = int.Parse(input[0]);
            int v = int.Parse(input[1]);
            tree[u].Add(v);
            tree[v].Add(u);
        }

        int center = FindTreeCenter(tree, n);
        Console.WriteLine(center);
    }

    static int FindTreeCenter(List<int>[] tree, int n)
    {
        int[] degree = new int[n + 1];
        Queue<int> leaves = new Queue<int>();

        for (int i = 1; i <= n; i++)
        {
            degree[i] = tree[i].Count;
            if (degree[i] == 1)
            {
                leaves.Enqueue(i);
            }
        }

        int remainingNodes = n;
        
        while (remainingNodes > 2)
        {
            int leavesCount = leaves.Count;
            remainingNodes -= leavesCount;

            for (int i = 0; i < leavesCount; i++)
            {
                int leaf = leaves.Dequeue();
                foreach (int neighbor in tree[leaf])
                {
                    degree[neighbor]--;
                    if (degree[neighbor] == 1)
                    {
                        leaves.Enqueue(neighbor);
                    }
                }
            }
        }

        
        List<int> centers = leaves.ToList();
        return centers.Max();  
    }
}
