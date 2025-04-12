namespace Boruvka_Algorithm;
class Program
{
    static void Main()
    {
        Console.WriteLine("Matrix method");
        int[,] graph = new int[,]
        {
            { 0, 2, 0, 6, 0 },
            { 2, 0, 3, 8, 5 },
            { 0, 3, 0, 0, 7 },
            { 6, 8, 0, 0, 9 },
            { 0, 5, 7, 9, 0 }
        };

        BoruvkaAlgorithm boruvka = new BoruvkaAlgorithm();
        var resultMatrix = boruvka.BoruvkaMatrix(graph);
        
        Console.WriteLine("Edges MST:");
        foreach (var edge in resultMatrix.mstEdges)
        {
            Console.WriteLine($"{edge.Item1} - {edge.Item2}");
        }

        Console.WriteLine($"MST weight: {resultMatrix.mstWeight}");
        
        Console.WriteLine("List method");
        var adjList = new List<(int, int)>[]
        {
            new() { (1, 4), (2, 3) },
            new() { (0, 4), (2, 1), (3, 2) },
            new() { (0, 3), (1, 1), (3, 4) },
            new() { (1, 2), (2, 4) }
        };

        var resultList = boruvka.BoruvkaMatrix(graph);
        
        Console.WriteLine("Edges MST:");
        foreach (var edge in resultList.mstEdges)
        {
            Console.WriteLine($"{edge.Item1} - {edge.Item2}");
        }

        Console.WriteLine($"MST weight: {resultList.mstWeight}");


    }
}
