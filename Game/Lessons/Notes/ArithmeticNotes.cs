namespace CodieGo_Adventure.Game.Lessons.Notes
{
    public class ArithmeticNotes
    {
        public static readonly Dictionary<int, string> Notes = new Dictionary<int, string>()
        {
            [0] = @"DID YOU KNOW? You can add as many numbers as you like. Let’s try getting the sum of three int variables.

TRY:
Declare three int variables with stored values.
Use addition (+) to get the sum of three int variables.
Display the result.",

            [1] = @"What happens if you write this code?

Console.Write(5 - 5);",

            [2] = @"What happens if the problem goes like this, int calc = 5 + 2 * 10, will the result be 70 or 25?",

            [3] = @"What happens if you divide an int value, will it display the result or will it cause an error?

TRY:
dividing 9 to 4 but using the int variable.",

            [4] = @"DID YOU KNOW? C# follows the same math rules you learned in school, known as PEMDAS. You can use parentheses to set which operations are performed first.

TRY:
Declaring int variable with a value of (5 + 2) * 10
Display the result",
        };
    }
}
