namespace CodieGo_Adventure.Game.Lessons.Task
{
    public class LogicalOperatorsTask
    {
        public static readonly Dictionary<int, string> Tasks = new()
        {
            [0] = @"[TASKS 1/4]
Declare bool variable named a with a value of true.
Declare bool variable named b with a value of true.
If a is true AND b is true, Display: a + AND + b + is true",

            [1] = @"[TASKS 2/4]
Declare bool variable a = false
Declare bool variable b = true
If a is false or b is true, Display: a + OR + b + is true",

            [2] = @"[TASKS 3/4]
Declare bool variable named a with a value of true.
Declare bool variable result = !a.
If a is NOT false, Display: NOT + a  + is false",

            [3] = @"[TASKS 4/4]
Declare bool variable a = true
Declare bool variable b = false
Declare bool variable c = true
Declare bool variable result = (a && c) || b
If variable result is true, Display: ( + a + AND + c + ) OR + b + is true",
        };
    }
}
