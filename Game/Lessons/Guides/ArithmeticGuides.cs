namespace CodieGo_Adventure.Game.Lessons.Guides
{
    public class ArithmeticGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"int variableName1 = 0;
    int variableName2 = 0;
    int variableName = variableName1 + variableName2;
    Console.Write(""Enter your text here"" + variableName);
                                OR
    Console.WriteLine(""Enter your text here"" + variableName);",

            [1] = @"int variableName1 = 0;
    int variableName2 = 0;
    int variableName = variableName1 - variableName2;
    Console.Write(""Enter your text here"" + variableName);
                                OR
    Console.WriteLine(""Enter your text here"" + variableName);",

            [2] = @"int variableName1 = 0;
    int variableName2 = 0;
    int variableName = variableName1 * variableName2;
    Console.Write(""Enter your text here"" + variableName);
                                OR
    Console.WriteLine(""Enter your text here"" + variableName);",

            [3] = @"double variableName1 = 0;
    double variableName2 = 0;
    int variableName = variableName1 / variableName2;
    Console.Write(""Enter your text here"" + variableName);
                                OR
    Console.WriteLine(""Enter your text here"" + variableName);",

            [4] = @"int variableName1 = 0;
    int variableName2 = 0;
    int variableName = variableName1 % variableName2;
    Console.Write(""Enter your text here"" + variableName);
                                OR
    Console.WriteLine(""Enter your text here"" + variableName);",
        };
    }
}
