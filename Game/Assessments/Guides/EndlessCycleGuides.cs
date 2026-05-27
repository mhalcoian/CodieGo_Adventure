namespace CodieGo_Adventure.Game.Assessments.Guides
{
    public class EndlessCycleGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"[TASK 1/3]
Count down from 10 to 0.
The numbers must be decreased by 2 each time the loop runs.
The numbers must be displayed from each line.

[EXPECTED OUTPUT]
10
8
6
4
2
0",

            [1] = @"[TASK 2/3]
Start at 1 and stops when it exceeds 30.
The number must be multiplied by 3 each time the loop runs.
 The numbers must be displayed in one line, separated with single spacing.

[EXPECTED OUTPUT]
3 9 27",

            [2] = @"[TASK 3/3]
Display the word ""Looping"" 5 times in a separate lines.
On the final line, display the total number of times the word was printed.

[EXPECTED OUTPUT]
Looping
Looping
Looping
Looping
Looping
Total Loops: 5",
        };
    }
}
