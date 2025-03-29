using Boruvka_Algorithm;

class Program
{
    static void Main(string[] args)
    {
        GraphMatrix graph1 = new GraphMatrix(5);
        graph1.AddEdge(2,3,4);
        graph1.AddEdge(0,1,6);
        graph1.PrintGraphMatrix();
        
        Console.WriteLine();
        
        GraphAdjList graph2 = new GraphAdjList(5);
        graph2.AddEdge(2, 3, 3);
        graph2.PrintGraphAdjList();
        
    }
}