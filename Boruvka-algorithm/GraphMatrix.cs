namespace Boruvka_Algorithm;

public class GraphMatrix
{
    private int[,] Matrix;
    public int Vertices { get; }

    public GraphMatrix(int vertices)
    {
        Vertices = vertices;
        Matrix = new int[vertices, vertices];
    }

    public void AddEdge(int u, int v, int weight)
    {
        Matrix[u, v] = weight;
        Matrix[v, u] = weight;
    }

    public void PrintGraphMatrix()
    {
        for (int i = 0; i < Vertices; i++)
        {
            for (int j = 0; j < Vertices; j++)
            {
                Console.Write(Matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}