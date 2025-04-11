using Boruvka_Algorithm;

class Program
{
    static void Main(string[] args)
    {
        GraphGenerator generator = new GraphGenerator(); 
        GraphAdjList graph = generator.GenerateRandomAdjList(5, 0.5);
        graph.PrintGraphAdjList();
        GraphMatrix graphMatrix = generator.GenerateRandomMatrix(5, 0.5);
        graphMatrix.PrintGraphMatrix();
        

    }
}