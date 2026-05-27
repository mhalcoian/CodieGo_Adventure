namespace CodieGo_Adventure.Game.Lessons.Guides
{
    public class OutputGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"Console.WriteLine(""Enter your text here"");",

            [1] = @"Console.Write(""Enter your text here"");",

            [2] = @"Console.WriteLine(""Enter your text here"");
Console.Write(""Enter your text here"");"
        };
    }
}
