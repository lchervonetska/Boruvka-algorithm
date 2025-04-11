namespace Boruvka_Algorithm;
class Program
{
    static void Main()
    {
        int[,] graph = new int[,]
        {
            { 0, 2, 0, 6, 0 },
            { 2, 0, 3, 8, 5 },
            { 0, 3, 0, 0, 7 },
            { 6, 8, 0, 0, 9 },
            { 0, 5, 7, 9, 0 }
        };

        BoruvkaAlgorithm boruvka = new BoruvkaAlgorithm();
        var result = boruvka.BoruvkaMatrix(graph);

        

        Console.WriteLine("Edges MST:");
        foreach (var edge in result.mstEdges)
        {
            Console.WriteLine($"{edge.Item1} - {edge.Item2}");
        }

        Console.WriteLine($"MST weight: {result.mstWeight}");
    }
}
