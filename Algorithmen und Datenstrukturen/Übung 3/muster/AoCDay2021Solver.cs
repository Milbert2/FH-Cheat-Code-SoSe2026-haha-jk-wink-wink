
public class AoCDay2021Solver
{
    private List<string> _lines;

    public AoCDay2021Solver(string inputFile)
    {
        var lines = File.ReadLines(inputFile);
        _lines = new List<string>(lines);

    }
    public List<LineStatus> Analyse()
    {
        var statuses = new List<LineStatus>();

        foreach (var line in _lines)
        {
            statuses.Add(AnalyseLine(line));
        }

        return statuses;
    }

    private LineStatus AnalyseLine(string line)
    {
        var stack = new FHS.CT.AlgoDat.DataStructures.Stack<char>();

        foreach (char symbol in line)
        {
            if (IsOpeningBracket(symbol))
            {
                stack.Push(symbol);
            }
            else
            {
                // symbol must be a closing bracket
                char lastOpeningBracket = stack.Pop();
                if (symbol != GetClosingBracket(lastOpeningBracket))
                {
                    return LineStatus.Invalid;
                }
            }
        }

        if (stack.Count > 0)
        {
            return LineStatus.Incomplete;
        }

        return LineStatus.Valid;
    }

    private bool IsOpeningBracket(char symbol)
    {
        if (symbol == '('
            || symbol == '['
            || symbol == '{'
            || symbol == '<')
        {
            return true;
        }

        return false;
    }

    private char GetClosingBracket(char openingBracket)
    {
        char closingBracket;

        switch (openingBracket)
        {
            case '(':
                closingBracket = ')';
                break;
            case '[':
                closingBracket = ']';
                break;
            case '{':
                closingBracket = '}';
                break;
            case '<':
                closingBracket = '>';
                break;

            default:
                throw new InvalidOperationException($"Cannot find closing bracket for {openingBracket}");
        }

        return closingBracket;
    }
}
public enum LineStatus
{
    Valid, Incomplete, Invalid
}