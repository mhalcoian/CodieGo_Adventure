namespace CodieGo_Adventure.Game.Lessons.Guides
{
    public class RelationalGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"int variableName1;
    int variableName2;
    bool variableName3 = variableName1 > variableName2;
    Console.Write(variableName3);
                    OR
    Console.WriteLine(variableName3);",

            [1] = @"int variableName1;
    int variableName2;
    if (variableName1 == variableName2)
    {
        // display here: variableName1 + ""Enter your text here"" + variableName2
    }",

            [2] = @"int variableName1;
    int variableName2;
    if (variableName1 != variableName2)
    {
        // display here: variableName1 + ""Enter your text here"" + variableName2
    }",

            [3] = @"int variableName1;
    int variableName2;
    if (variableName1 >= variableName2)
    {
        // display here: variableName1 + ""Enter your text here"" + variableName2
    }",

            [4] = @"int variableName1;
    int variableName2;
    if (variableName1 <= variableName2)
    {
        // display here: variableName1 + ""Enter your text here"" + variableName2
    }",
        };
    }
}
