if (args.Length != 1)
{
    Console.WriteLine("Usage: dotnet run -- <filename>");

    return;
}

var solver = new AoCDay2021Solver(args[0]);
var solutions = solver.Analyse();
foreach (var solution in solutions)
{
    Console.WriteLine(solution);
}
