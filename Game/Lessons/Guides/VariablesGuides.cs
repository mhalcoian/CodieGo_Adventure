namespace CodieGo_Adventure.Game.Lessons.Guides
{
    public class VariablesGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"string variableName = ""Text"";
    Console.Write(variableName);
                OR
    Console.WriteLine(variableName);",

            [1] = @"char variableName = 'M';
    Console.Write(variableName);
                OR
    Console.WriteLine(variableName);",

            [2] = @"int variableName = 0;
    Console.Write(variableName);
                OR
    Console.WriteLine(variableName);",

            [3] = @"double variableName = 9.99;
    Console.Write(variableName);
                OR
    Console.WriteLine(variableName);",

            [4] = @"bool variableName = true;
    Console.Write(variableName);
                OR
    Console.WriteLine(variableName);",
        };
    }
}
