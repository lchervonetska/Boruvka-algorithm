namespace Boruvka_Algorithm;

public class GraphGenerator
{
    public GraphAdjList GenerateRandomAdjList(int n, double seed)
    {
        GraphAdjList graph = new GraphAdjList(n);
        int maxEdges = n * (n - 1) / 2;
        int targetEdges = (int)(seed * maxEdges);
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

    public GraphMatrix GenerateRandomMatrix(int n, double seed)
    {
        GraphMatrix graph = new GraphMatrix(n); 
        int maxEdges = n * (n - 1) / 2;
        int targetEdges = (int)(seed * maxEdges);
        HashSet<string> matrix = new HashSet<string>();
        Random random = new Random();
        while (matrix.Count < targetEdges)
        {
            int u = random.Next(0, n);
            int v = random.Next(0, n);
            if (u != v)
            {
                string edge = $"{Math.Min(u, v)} - {Math.Max(u, v)}";
                if (!matrix.Contains(edge))
                {
                    matrix.Add(edge);
                    int weight = random.Next(1, 11);
                    graph.AddEdge(u, v, weight);
                }
            }
        }
        return graph;

    }
}
