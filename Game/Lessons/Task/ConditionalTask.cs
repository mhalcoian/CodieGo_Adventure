namespace CodieGo_Adventure.Game.Lessons.Task
{
    public class ConditionalTask
    {
        public static readonly Dictionary<int, string> Tasks = new()
        {
            [0] = @"[TASKS 1/4] 
Declare int variable named num with value of 10.
If num is greater than 5, Display: Greater than 5",

            [1] = @"[TASKS 2/4]
Declare int variable named num with value of 3.
If num is greater than 5, Display: Passed
Else, Display: Failed",

            [2] = @"[TASKS 3/4]
Declare int variable named num with value of 2.
If num is Equal to 3, Display: The number is 3
If num is Equal to 2, Display: The number is 2
If num is Equal to 1, Display: The number is 1",

            [3] = @"[TASKS 4/4]
Declare a double variable named Num with value of any number.
If the value of Num is Less Than 10, Display: The number is less than 10
If the value of Num is Less Than 20, Display: The number is less than 20
Else, Display: The number is not less than 10 or 20",
        };
    }
}
