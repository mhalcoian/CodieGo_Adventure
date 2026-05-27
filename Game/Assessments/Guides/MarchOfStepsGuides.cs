namespace CodieGo_Adventure.Game.Assessments.Guides
{
    public class MarchOfStepsGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"[TASK 1/3]
Start at 5 and end at 25.
The loop must jump at 5 for each iterations.
Display the numbers in one line, separated with single line.

[EXPECTED OUTPUT]
5 10 15 20 25",

            [1] = @"[TASK 2/3]
Run a loop 5 times.
Instead of displaying numbers, display the character ""*"" in one line.

[EXPECTED OUTCOME]
*****",

            [2] = @"[TASK 3/3]
Iterate numbers from 1 to 10.
Use an if statement to check if the number is even.
Display the number if it meets the condition.
Otherwise, leave it blank.
The numbers must be displayed on it’s own separate line.

[EXPECTED OUTCOME]

2

4

6

8

10",
        };
    }
}
