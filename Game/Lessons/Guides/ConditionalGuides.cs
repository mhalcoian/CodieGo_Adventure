namespace CodieGo_Adventure.Game.Lessons.Guides
{
    public class ConditionalGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"int variableName = 0;
    if (variableName > n)
    {
        // display here
    }",

            [1] = @"GUIDE:
    int variableName = 0;
    if (variableName > n)
    {
        // display here
    }
    else
    {
        // display here
    }",

            [2] = @"int variableName1 = 0;
    if (variableName1 == n)
    {
        // display here
    }
    else if (condition)
    {
        // display here
    }
    else if (condition)
    {
        // display here
    }",

            [3] = @"double variableName = 1 or 1.0...;
    if (variableName > n)
    {
        // display here
    }
    else if (condition)
    {
        // display here
    }
    else
    {
        // display here
    }",
        };
    }
}
