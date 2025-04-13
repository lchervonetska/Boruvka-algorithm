using System.Globalization;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace Boruvka_Algorithm;

public class PlotVisualizer
{
    private static List<LineSeries> CreateAllSeries(IEnumerable<string> lines, Func<string, bool> filter)
    {
        var matrixGroups = new Dictionary<string, LineSeries>();
        var listGroups = new Dictionary<string, LineSeries>();

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var size = int.Parse(parts[0]);
            var density = parts[1];
            var matrix = double.Parse(parts[2], CultureInfo.InvariantCulture);
            var list = double.Parse(parts[3], CultureInfo.InvariantCulture);

            if (!filter(density))
                continue;

            if (!matrixGroups.ContainsKey(density))
            {
                matrixGroups[density] = new LineSeries
                {
                    Title = $"Matrix ({density})",
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 4,
                    MarkerStroke = OxyColors.MediumVioletRed,
                    MarkerFill = OxyColors.MediumVioletRed,
                    Color = OxyColors.MediumVioletRed,
                    StrokeThickness = 2
                };
            }

            if (!listGroups.ContainsKey(density))
            {
                listGroups[density] = new LineSeries
                {
                    Title = $"List ({density})",
                    MarkerType = MarkerType.Square,
                    MarkerSize = 4,
                    MarkerStroke = OxyColors.MediumPurple,
                    MarkerFill = OxyColors.MediumPurple,
                    Color = OxyColors.MediumPurple,
                    StrokeThickness = 2
                };
            }

            matrixGroups[density].Points.Add(new DataPoint(size, matrix));
            listGroups[density].Points.Add(new DataPoint(size, list));
        }

        return matrixGroups.Values.Concat(listGroups.Values).ToList();
    }

    private static PlotModel CreateModel(string title, IEnumerable<LineSeries> series)
    {
        var model = new PlotModel
        {
            Title = title,
            TitleFontSize = 20,
            Background = OxyColors.White,
            PlotAreaBorderColor = OxyColors.Transparent
        };

        foreach (var s in series)
            model.Series.Add(s);

        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Bottom,
            Title = "Number of Vertices",
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            MajorGridlineColor = OxyColors.LightGray,
            MinorGridlineColor = OxyColors.LightGray
        });

        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = "Execution Time (ms)",
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            MajorGridlineColor = OxyColors.LightGray,
            MinorGridlineColor = OxyColors.LightGray
        });

        return model;
    }

    private static void ExportToPng(PlotModel model, string filePath, int width = 1000, int height = 700)
    {
        using var plotView = new PlotView
        {
            Model = model,
            Width = width,
            Height = height
        };

        using var bitmap = new System.Drawing.Bitmap(width, height);
        plotView.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, width, height));
        bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
    }

    public static void ShowChart()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var outputDir = Path.Combine(baseDir, "Result");
        var path = Path.Combine(outputDir, "results_comparison.csv");

        var lines = File.ReadAllLines(path).Skip(1).ToList();

        var exportSeries = CreateAllSeries(lines, _ => true);
        var modelForExport = CreateModel("Boruvka Execution Time (Average of 20 runs)", exportSeries);
        ExportToPng(modelForExport, Path.Combine(outputDir, "combined_chart.png"));

        var densities = lines.Select(line => line.Split(',')[1]).Distinct();

        foreach (var density in densities)
        {
            var series = CreateAllSeries(lines, d => d == density);
            var model = CreateModel($"Boruvka Execution Time (Density = {density})", series);
            var fileName = $"chart_density_{density.Replace(".", "_")}.png";
            ExportToPng(model, Path.Combine(outputDir, fileName));
        }

        var viewSeries = CreateAllSeries(lines, _ => true);
        var modelForView = CreateModel("Boruvka Execution Time (Average of 20 runs)", viewSeries);

        var plotView = new PlotView
        {
            Dock = DockStyle.Fill,
            Model = modelForView
        };

        var form = new Form
        {
            Text = "Boruvka Benchmark Results",
            Width = 1000,
            Height = 700
        };

        form.Controls.Add(plotView);
        Application.Run(form);
    }

}

