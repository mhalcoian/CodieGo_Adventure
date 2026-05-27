namespace CodieGo_Adventure.Game.Lessons.Notes
{
    public class SwitchNotes
    {
        public static readonly Dictionary<int, string> Notes = new Dictionary<int, string>()
        {
            [0] = @"What happens if you don’t add break at the end of the case?",

            [1] = @"What if you want two different cases to do the exact same thing?

TRY:
You can do this:

switch (day) 
{ 
case 1: 
case 2:
Console.WriteLine(""This is the first day!"");
   break;
 }",
        };
    }
}
