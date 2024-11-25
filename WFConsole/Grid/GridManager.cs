using WFConsole.Rules;
using WFConsole.Utils;

namespace WFConsole.Grid;

public class GridManager
{
    private readonly int _width;
    private readonly int _height;
    private readonly Cell[,] _grid;
    private readonly DirectionalRules _rules;
    private Dictionary<(int x, int y), List<(int x, int y, string direction)>> _neighborCache = new();

    private readonly Dictionary<char, int> _stateWeights = new()
    {
        {'C', 2 },
        {'S', 2 },
        {'L', 6 },
    };


    public GridManager(int width, int height)
    {
        _width = width;
        _height = height;
        _grid = new Cell[width, height];
        _rules = new DirectionalRules();
        InitializeNeighbourCache();
    }

    private void InitializeNeighbourCache()
    {
        _neighborCache = new();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _neighborCache[(x, y)] = NeighborHelper.GetNeighbors(x, y, _width, _height);
            }
        }
    }

    public void InitializeGrid()
    {
        char[] states = { 'L', 'S', 'C' };
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _grid[x, y] = new Cell(states);
            }
        }
    }


    public bool IsGridResolved()
    {
        return _grid.Cast<Cell>().All(cell => cell.Entropy == 1);
    }
    public void ResolveNextStep()
    {
        var cellToCollapse = FindCellWithLowestEntropy();
        if (cellToCollapse != null)
        {
            CollapseCell(cellToCollapse.Value.x, cellToCollapse.Value.y);
            PropagateConstraints();
        }
    }
    public void PrintGrid()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var cell = _grid[x, y];
                var currentState = cell.Entropy == 1 ? $"[{cell.PossibleStates[0]}] " : "[...] ";

                if (cell.Entropy == 1)
                {
                    Console.ForegroundColor = cell.PossibleStates[0] switch
                    {
                        'L' => ConsoleColor.Green,
                        'C' => ConsoleColor.DarkGray,
                        'S' => ConsoleColor.Blue,
                        _ => ConsoleColor.White,
                    };
                }
                Console.Write(currentState);
            }
            Console.WriteLine();
        }
        Console.ForegroundColor = ConsoleColor.White;
    }
    private (int x, int y)? FindCellWithLowestEntropy()
    {
        int minEntropy = int.MaxValue;
        (int x, int y)? result = null;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int entropy = _grid[x, y].Entropy;
                if (entropy > 1 && entropy < minEntropy)
                {
                    minEntropy = entropy;
                    result = (x, y);
                }
            }
        }

        return result;
    }

    private void CollapseCell(int x, int y)
    {
        var cell = _grid[x, y];

        var possibleStates = cell.PossibleStates;
        List<char> weightedStates = new();

        foreach (var states in possibleStates)
        {
            weightedStates.AddRange(Enumerable.Repeat(states, _stateWeights[states]));
        }
        var chosenState = weightedStates[new Random().Next(weightedStates.Count)];

        cell.Collapse(chosenState);

        //char chosenState = cell.PossibleStates[new Random().Next(cell.PossibleStates.Count)];
        //cell.Collapse(chosenState);
    }

    private void PropagateConstraints()
    {
        var queue = new Queue<(int x, int y)>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_grid[x, y].Entropy == 1)
                {
                    queue.Enqueue((x, y));
                }
            }
        }

        while (queue.Count > 0)
        {
            var (currentX, currentY) = queue.Dequeue();
            var currentState = _grid[currentX, currentY].PossibleStates[0];
            var neighbors = NeighborHelper.GetNeighbors(currentX, currentY, _width, _height);

            foreach (var (nx, ny, direction) in neighbors)
            {
                var neighbor = _grid[nx, ny];
                if (neighbor.Entropy == 1) continue;

                var validStates = _rules.GetValidStates(currentState, direction);
                neighbor.PossibleStates = neighbor.PossibleStates
                    .Intersect(validStates)
                    .ToList();
                if (neighbor.Entropy == 1)
                {
                    queue.Enqueue((nx, ny));
                }

            }
        }
    }
}