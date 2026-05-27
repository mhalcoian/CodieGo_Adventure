namespace CodieGo_Adventure.Game.Lessons.Notes
{
    public class OutputNotes
    {
        public static readonly Dictionary<int, string> Notes = new()
        {
            [0] = @"What happens if the syntax is missing a semi colon (;)?

TRY: 
Using the Console.WriteLine without the semi colon in the end.",

            [1] = @"What happens if you only leave the parentheses empty?

TRY: 
Using Console.Write and Console.WriteLine leaving the inside of parentheses empty.

You can also try adding quotation marks (“”) inside the parentheses and see the difference.",

            [2] = @"What if I want to print a double quotation marks ("") inside my message?
Since C# uses quotes to start and end a string, writing Console.WriteLine(""He said ""Hello""""); will cause an error.

TRY:
Using backlash symbol (\) before the quotation marks to tell C# that it is part of the text.

Using backlash has other usage, such as “\t” for a tab and “\n” for new line."
        };
    }
}
