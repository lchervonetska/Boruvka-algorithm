using System.Globalization;

namespace Boruvka_Algorithm;

public class ExperimentBatch
{
    public static void RunAll()
    {
        var runner = new ExperimentRunner();
        int[] sizes = { 20, 40, 60, 80, 100, 120, 140, 160, 180, 200 };
        double[] densities = { 0.1, 0.3, 0.5, 0.7, 0.9 };

        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string resultDir = Path.Combine(baseDir, "Result");

        if (!Directory.Exists(resultDir))
            Directory.CreateDirectory(resultDir);

        string resultPath = Path.Combine(resultDir, "results_comparison.csv");


        var results = new List<string>();
        results.Add("Size,Density,MatrixTime(ms),ListTime(ms)");

        var tasks = new List<(int size, double density)>();

        foreach (var size in sizes)
        {
            foreach (var density in densities)
            {
                tasks.Add((size, density));
            }
        }

        Console.WriteLine("Running experiments...");

        foreach (var pair in tasks)
        {
            int size = pair.size;
            double density = pair.density;

            double totalMatrix = 0;
            for (int i = 0; i < 20; i++)
                totalMatrix += runner.RunExperimentMatrix(size, density);
            double avgMatrix = totalMatrix / 10.0;

            double totalList = 0;
            for (int i = 0; i < 20; i++)
                totalList += runner.RunExperimentList(size, density);
            double avgList = totalList / 10.0;

            string line = $"{size},{density.ToString(CultureInfo.InvariantCulture)},{avgMatrix.ToString("F2", CultureInfo.InvariantCulture)},{avgList.ToString("F2", CultureInfo.InvariantCulture)}";
            results.Add(line);

            Console.WriteLine($"Size: {size}, Density: {density} => Matrix: {avgMatrix:F2} ms, List: {avgList:F2} ms");
        }

        Console.WriteLine("Writing results to file...");
        File.WriteAllLines(resultPath, results);
        Console.WriteLine("Experiment completed. Results saved to results_comparison.csv");
    }
}