namespace CodieGo_Adventure.Game.Lessons.Notes
{
    public class ForNotes
    {
        public static readonly Dictionary<int, string> Notes = new Dictionary<int, string>()
        {
            [0] = @"What happens If you change the value of i inside the loop body AND in the header?

TRY:
Adding i+= 2 before your output syntax",

            [1] = @"What if you want to count two things at the exact same time?

TRY:
for (int i = 0, j = 10; i < 10; i++, j--)
{
Console.WriteLine(i);
Console.WriteLine(j);
}",
        };
    }
}
