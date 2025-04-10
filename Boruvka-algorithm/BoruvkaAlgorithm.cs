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
            int[] cheapestEdge = new int[n];
            int[] cheapestEdgeWeight = new int[n];

            for (int i = 0; i < n; i++)
            {
                cheapestEdge[i] = -1;
                cheapestEdgeWeight[i] = int.MaxValue;
            }

            for (int u = 0; u < n; u++)
            {
                for (int v = u + 1; v < n; v++)
                {
                    int weight = graph[u, v];
                    if (u != v && weight > 0)
                    {
                        int rootU = Find(u);
                        int rootV = Find(v);
                        if (rootU != rootV && weight < cheapestEdgeWeight[rootU])
                        {
                            cheapestEdge[rootU] = v;
                            cheapestEdgeWeight[rootU] = weight;
                        }
                    }
                }
            }

            for (int u = 0; u < n; u++)
            {
                int rootU = Find(u);
                int v = cheapestEdge[rootU];
                if (v != 0)
                {
                    int rootV = Find(v);
                    if (rootU != rootV)
                    {
                        Union(rootU, rootV);
                        mstEdges.Add((u,v));
                        totalWeight += graph[u, v];
                        components--;
                    }
                }
            }
        }

        return (mstEdges, totalWeight);
    }
}