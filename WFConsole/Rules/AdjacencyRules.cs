namespace WFConsole.Rules;

public class AdjacencyRules
{
    private readonly Dictionary<char, List<char>> _rules = new()
    {
        { 'L', new List<char> { 'C' } },
        { 'S', new List<char> { 'C' } },
        { 'C', new List<char> { 'L', 'S' } }
    };

    public List<char> GetValidStates(char currentState)
    {
        return _rules.ContainsKey(currentState) ? _rules[currentState] : new List<char>();
    }
}
public class DirectionalRules
{
    private readonly Dictionary<char, Dictionary<string, List<char>>> _rules = new()
    {
        {
            'S', new Dictionary<string, List<char>>
            {
                { "Up", new List<char> {'S','C' } },
                { "Down", new List<char> { 'S', 'C' } },
                { "Left", new List<char> { 'C' } },
                { "Right", new List<char> { 'S' } }
            }
        },
        {
            'L', new Dictionary<string, List<char>>
            {
                { "Up", new List<char> { 'L', 'C' } },
                { "Down", new List<char> { 'L', 'C' } },
                { "Left", new List<char> { 'L' } },
                { "Right", new List<char> { 'L', 'C'} }
            }
        },
        {
            'C', new Dictionary<string, List<char>>
            {
                { "Up", new List<char> { 'L', 'C','S' } },
                { "Down", new List<char> { 'L', 'C', 'S' } },
                { "Left", new List<char> { 'L'} },
                { "Right", new List<char> {'S'} }
            }
        }
    };

    public List<char> GetValidStates(char currentState, string direction)
    {
        if (_rules.ContainsKey(currentState) && _rules[currentState].ContainsKey(direction))
        {
            return _rules[currentState][direction];
        }
        return [];
    }
}

