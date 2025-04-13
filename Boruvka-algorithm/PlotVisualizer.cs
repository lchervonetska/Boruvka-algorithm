using System.Globalization;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace Boruvka_Algorithm;

public class PlotVisualizer
{
    private static LineSeries CreateMatrixSeries(IEnumerable<string> lines)
    {
        var series = new LineSeries
        {
            Title = "Matrix",
            MarkerType = MarkerType.Circle,
            MarkerSize = 5,
            MarkerStroke = OxyColors.MediumVioletRed,
            MarkerFill = OxyColors.MediumVioletRed,
            Color = OxyColors.MediumVioletRed,
            StrokeThickness = 2
        };

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            int size = int.Parse(parts[0]);
            double matrix = double.Parse(parts[2], CultureInfo.InvariantCulture);
            series.Points.Add(new DataPoint(size, matrix));
        }

        return series;
    }

    private static LineSeries CreateListSeries(IEnumerable<string> lines)
    {
        var series = new LineSeries
        {
            Title = "List",
            MarkerType = MarkerType.Square,
            MarkerSize = 5,
            MarkerStroke = OxyColors.MediumPurple,
            MarkerFill = OxyColors.MediumPurple,
            Color = OxyColors.MediumPurple,
            StrokeThickness = 2
        };

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            int size = int.Parse(parts[0]);
            double list = double.Parse(parts[3], CultureInfo.InvariantCulture);
            series.Points.Add(new DataPoint(size, list));
        }

        return series;
    }

    private static PlotModel CreateModel(string title, params LineSeries[] series)
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

        var matrixModel = CreateModel("Matrix Only", CreateMatrixSeries(lines));
        var listModel = CreateModel("List Only", CreateListSeries(lines));

        var combinedModelForExport = CreateModel(
            "Boruvka Execution Time (Average of 20 runs)",
            CreateMatrixSeries(lines),
            CreateListSeries(lines)
        );

        var combinedModelForView = CreateModel(
            "Boruvka Execution Time (Average of 20 runs)",
            CreateMatrixSeries(lines),
            CreateListSeries(lines)
        );

        ExportToPng(matrixModel, Path.Combine(outputDir, "matrix_chart.png"));
        ExportToPng(listModel, Path.Combine(outputDir, "list_chart.png"));
        ExportToPng(combinedModelForExport, Path.Combine(outputDir, "combined_chart.png"));

        var plotView = new PlotView
        {
            Dock = DockStyle.Fill,
            Model = combinedModelForView
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
