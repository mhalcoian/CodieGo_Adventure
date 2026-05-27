namespace CodieGo_Adventure.Game.Lessons.Guides
{
    public class LogicalGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"bool variableName1;
    bool variableName2;
    if (variableName1 == true && variableName2 == true)
    {
        // display here: variableName1 + ""Enter your text here"" + variableName2 + ""Enter your text here""
    }",

            [1] = @"bool variableName1;
    bool variableName2;
    if (variableName1 == false || variableName2 == true)
    {
        // display here: variableName1 + ""Enter your text here"" + variableName2 + ""Enter your text here""
    }",

            [2] = @"bool variableName1;
    bool variableName2 = !variableName1;
    if (variableName2 != false)
    {
        // display here: ""NOT"" + variableName1 + ""Enter your text here""
    }",

            [3] = @"bool variableName1;
    bool variableName2;
    bool variableName3;
    bool variableName4 = (variableName1 && variableName3) || variableName2;
    if (variableName4 == true)
    {
        // display here: (variableName1 + ""Enter your text here"" + variableName3) + ""OR"" + variableName2 + ""Enter your text here""
    }",
        };
    }
}
