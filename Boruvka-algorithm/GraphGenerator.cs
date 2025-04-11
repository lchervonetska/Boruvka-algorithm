namespace Boruvka_Algorithm;

public class GraphGenerator
{
    public GraphAdjList GenerateRandomAdjList(int n, double delta)
    {
        GraphAdjList graph = new GraphAdjList(n);
        int maxEdges = n * (n - 1) / 2;
        int targetEdges = (int)(delta * maxEdges);
        HashSet<string> adjList = new HashSet<string>();
        Random random = new Random();
        while (adjList.Count < targetEdges)
        {
            int u = random.Next(0, n);
            int v = random.Next(0, n);
            if (u != v)
            {
                string edge = $"{Math.Min(u, v)} - {Math.Max(u, v)}";
                if (!adjList.Contains(edge))
                {
                    adjList.Add(edge);
                    int weight = random.Next(1, 11);
                    graph.AddEdge(u, v, weight);
                }

            }

        } 
        return graph;


    }

    public void GenerateMatrix(int n, double delta)
    {
        
    }
}
