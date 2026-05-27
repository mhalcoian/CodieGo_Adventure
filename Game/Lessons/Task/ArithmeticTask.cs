namespace CodieGo_Adventure.Game.Lessons.Task
{
    public class ArithmeticTask
    {
        public static readonly Dictionary<int, string> Tasks = new()
        {
            [0] = @"[TASKS 1/5]
Declare int variable named a with a value of 6.
Declare int variable named b  with a value of 7.
Declare int variable named sum.
Assign a value of variable sum: a + b
Display: Sum: + sum",

            [1] = @"[TASKS 2/5]
Declare int variable named a with a value of 9.
Declare int variable named b with a value of 3.
Declare int variable named diff.
Assign a value of variable diff: a - b
Display: Difference: + diff",

            [2] = @"[TASKS 3/5]
Declare int variable named a with a value of 3.
Declare int variable named b with a value of 2.
Declare  int variable named prod.
Assign a value of variable prod: a * b
Display: Product: + prod",

            [3] = @"[TASKS 4/5]
Declare double variable named a with a value of 9.
Declare double variable named b with a value of 4.
Declare double variable quot.
Assign a value of variable quot: a / b
Display: Quotient: + quot",

            [4] = @"[TASKS 5/5]
Declareint variable named a with a value of 10.
Declare int variable named b with a value of 2.
Declare int variable mod.
Assign a value of variable mod: 10 % 2
Display: Remainder: + mod",
        };
    }
}
