namespace Boruvka_Algorithm;

public class ExperimentRunner
{
    private BoruvkaAlgorithm algorithm = new();
    private GraphGenerator generator = new();

    public double RunExperimentMatrix(int n, double delta)
    {
        var graph = generator.GenerateRandomMatrix(n, delta);
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        algorithm.BoruvkaMatrix(graph.Matrix);
        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds;
    }

    public double RunExperimentList(int n, double delta)
    {
        var graph = generator.GenerateRandomAdjList(n, delta);
        var adjList = new List<(int, int)>[n];
        for (int i = 0; i < n; i++) adjList[i] = graph.AdjList[i];
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        algorithm.BoruvkaList(adjList);
        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}
