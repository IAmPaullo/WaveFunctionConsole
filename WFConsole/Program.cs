using System.Diagnostics;
using WFConsole.Grid;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        var gridManager = new GridManager(10, 10);
        gridManager.InitializeGrid();

        while (!gridManager.IsGridResolved())
        {
            gridManager.ResolveNextStep();
        }
        gridManager.PrintGrid();
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed.ToString());
    }
}
