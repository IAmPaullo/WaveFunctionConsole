namespace WFConsole.Utils;

public static class NeighborHelper
{
    public static List<(int x, int y, string direction)> GetNeighbors(int x, int y, int width, int height)
    {
        var neighbors = new List<(int x, int y, string)>();

        if (x > 0) neighbors.Add((x - 1, y, "Left"));
        if (x < width - 1) neighbors.Add((x + 1, y, "Right"));
        if (y > 0) neighbors.Add((x, y - 1, "Up"));
        if (y < height - 1) neighbors.Add((x, y + 1, "Down"));

        return neighbors;
    }
}
