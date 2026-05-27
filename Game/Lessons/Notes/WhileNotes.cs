namespace CodieGo_Adventure.Game.Lessons.Notes
{
    public class WhileNotes
    {
        public static readonly Dictionary<int, string> Notes = new Dictionary<int, string>()
        {
            [0] = @"What happens if you don’t assign a value on an iterator?

TRY:

int i;

while (i > 5)
{
Console.WriteLine(“Loop”);
}",

            [1] = @"Is it possible to have a while loop inside a while loop?

TRY:
int i = 0;

while (i < 5)
{
    Console.Write(i);
    int j = 0;

    while (j < 5)
    {
        Console.Write(j);
        j++;
    }

    i++;
}",

            [2] = @"Did you know? You can use multiplication (*) and other arithmetic operations in your iterator.",
            
            [3] = @"What happens in a do while loop if the condition is already false?

TRY:
int num = 1;

do
{ 
Console.WriteLine(""Any Text"");
}
while (num == 2);

Wll it display an output or not?",
            
            [4] = @"What happens if we use while loop instead do while loop?

TRY:
int num = 1;

while (num == 2)
{ 
Console.WriteLine(""Any Text"");
}

Wll it display an output or not?",
        };
    }
}
