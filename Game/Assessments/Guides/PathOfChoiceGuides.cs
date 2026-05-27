namespace CodieGo_Adventure.Game.Assessments.Guides
{
    public class PathOfChoiceGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"[TASK 1/3]
Check if the student passed or failed.

Student Grade: 73
If the student’s grade is greater than 75, display ""Passed""
If the student’s grade is less than 75, display ""Failed""

[EXPECTED OUTPUT]
Failed",

            [1] = @"[TASK 2/3]
Check if the number is Positive or Negative number.

Given Number: -3
If the given number is a positive number, display ""Positive Number""
Otherwise, display ""Negative Number""

[EXPECTED OUTPUT]
Negative Number",

            [2] = @"[TASK 3/3]
Categorize the weight of a shipping container.

Container Weight: 30
If the weight is greater than 50, display: ""Heavy""
If the weight is greater than 20, display: ""Medium""
If the weight is less than 21, display: ""Light""

[EXPECTED OUTPUT]
Medium",
        };
    }
}
