namespace Boruvka_Algorithm;

public class BoruvkaAlgorithm
{
    public (List<(int, int)> mstEdges, int mstWeight) BoruvkaMatrix(int[,] graph)
    {
        int n = graph.GetLength(0);
        int[] parent = new int[n];
        for (int i = 0; i < n; i++) parent[i] = i;

        int Find(int u)
        {
            while (parent[u] != u)
            {
                u = parent[u];
            }
            return u;
        }

        void Union(int u, int v)
        {
            int rootU = Find(u); 
            int rootV = Find(v);
            if (rootU != rootV)
            {
                parent[rootV] = rootU;
            }
        }
        
        int components = n;
        int totalWeight = 0;
        var mstEdges = new List<(int, int)>();

        while (components > 1)
        {
            var cheapest = new (int u, int v, int weight)[n];
            for (int i = 0; i < n; i++)
            {
                cheapest[i] = (-1, -1, int.MaxValue);
            }

            for (int u = 0; u < n; u++)
            {
                for (int v = 0; v < n; v++)
                {
                    int weight = graph[u, v];
                    if (weight > 0)
                    {
                        int setU = Find(u);
                        int setV = Find(v);
                        if (setU != setV && weight < cheapest[setU].weight)
                        {
                            cheapest[setU] = (u, v, weight);
                        }
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                var (u, v, weight) = cheapest[i];
                if (u != -1 && v != -1)
                {
                    int setU = Find(u);
                    int setV = Find(v);
                    if (setU != setV)
                    {
                        Union(setU, setV);
                        mstEdges.Add((u + 1, v + 1)); 
                        totalWeight += weight;
                        components--;
                    }
                }
            }

        }

        return (mstEdges, totalWeight);
    }
}