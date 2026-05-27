namespace CodieGo_Adventure.Game.Lessons.Task
{
    public class WhileLoopTask
    {
        public static readonly Dictionary<int, string> Tasks = new()
        {
            [0] = @"[TASKS 1/5]
Declare int variable named num with a value of 1.
While num is less than or equal to 3,
Display: Loop + num
num increments

[EXPECTED OUTCOME]
Loop 1
Loop 2
Loop 3",

            [1] = @"[TASKS 2/5]
Declare int variable named count with a value of 5.
While count is greater than or equal to 1, 
Display: count
count decrements

[EXPECTED OUTCOME]
5
4
3
2
1",

            [2] = @"[TASKS 3/5]
Declare int variable named num with a value of 8.
While num is greater than 0,
Display: num
num decrements to 2

[EXPECTED OUTCOME]
8
6
4
2",

            [3] = @"[TASKS 4/5]
* Declare int variable named num with a value of 1.
* Using Do While Loop, display ""Post-Test""
‎and num increments, While num
‎is less than 5.

[EXPECTED OUTCOME]
Post-Test
Post-Test
Post-Test
Post-Test",

            [4] = @"[TASKS 5/5]
* Declare int variable named num with a value of 5.
* Using Do While Loop, display num 
and num decrements, While num is greater than 1.

[EXPECTED OUTCOME]
5 
4 
3 
2",
        };
    }
}
