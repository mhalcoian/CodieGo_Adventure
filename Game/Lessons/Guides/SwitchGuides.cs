namespace CodieGo_Adventure.Game.Lessons.Guides
{
    public class SwitchGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"int variableName = value;
    switch (variableName)
    {
        case value:
            // display here: Option 1
        break;
        default:
            // display here: Invalid Option
        break;
    }",

            [1] = @"int variableName = 0;
    switch (variableName)
    {
        case 1:
            // display here: This is First Day
        break;
        default:
            // display here: Invalid Day
        break;
    }",
        };
    }
}
