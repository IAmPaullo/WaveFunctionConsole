
namespace WFConsole.Grid;

public class Cell
{
    public List<char> PossibleStates { get; set; }
    public int Entropy => PossibleStates.Count;

    public Cell(IEnumerable<char> initialStates)
    {
        PossibleStates = new List<char>(initialStates);
    }

    public void Collapse(char state)
    {
        PossibleStates = new List<char> { state };
    }

}
