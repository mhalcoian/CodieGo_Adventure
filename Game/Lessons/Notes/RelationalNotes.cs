namespace CodieGo_Adventure.Game.Lessons.Notes
{
    public class RelationalNotes
    {
        public static readonly Dictionary<int, string> Notes = new Dictionary<int, string>()
        {
            [0] = @"Can you compare the int variable and double variable together?

TRY:
Change the the int variable b to double and see the result.",

            [1] = @"What happens if you use only one equal sign (=) instead of two?",

            [2] = @"What if I only want to run code if the user's name is NOT 'Guest'?

TRY:
Declare a string variable username with a value of your first name.
If username is not equal to “Guest”, Display “Hello Guest!”

After running the code, will the message be displayed or not?",

            [3] = @"What is the difference of Greater Than (>) and Greater Than or Equal to(>=)?

TRY:
 Follow this steps:
use the int variable named a on the given task.
If a is Greater than or Equal to 8, Display a + “ is greater than or equal to 8”
else, Display a + “ is less than 8”
Execute your code
Now switch the condition in your if statement to Greater Than
Execute your code again

What do you think is the difference?",

            [4] = @"What happens if your condition is like this, (“Apple” == “apple”), will it return true or false?",
        };
    }
}
