namespace CodieGo_Adventure.Game.Lessons.Guides
{
    public class WhileGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"int variableName = 0;
    while (variableName <= n)
    {
        // display here: ""Enter your text here"" + variableName
        variableName++;
    }",

            [1] = @"int variableName = 0;
    while (variableName >= n)
    {
        // display here: variableName
        variableName--;
    }",

            [2] = @"int variableName = 0;
    while (variableName >= n)
    {
        // display here: variableName
        variableName-=2;
    }",

            [3] = @"int variableName = 0;
    do
    {
        // display here: variableName
        variableName++;
    }
    while (condition);",

            [4] = @"int variableName = 0;
    do
    {
        // display here: variableName
        variableName--;
    }
    while (condition);",
        };
    }
}
