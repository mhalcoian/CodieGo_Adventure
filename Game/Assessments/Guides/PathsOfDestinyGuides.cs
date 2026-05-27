namespace CodieGo_Adventure.Game.Assessments.Guides
{
    public class PathsOfDestinyGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"[TASK 1/3]
RGB Colors:

Code: ‘G’
The code matches 'R', display: ""Red""
The code matches 'G', display: ""Green""
The code matches 'B', display: ""Blue""

[EXPECTED OUTPUT]
Green",

            [1] = @"[TASK 2/3]
Evaluating Numerical Score:

Score: 2
The score matches to 3, display ""Excellent""
The score matches to 2, display ""Fair""
The score matches to 1, display ""Poor""
Otherwise, display ""Invalid Score""

[EXPECTED OUTPUT]
Fair",
            
            [2] = @"[TASK 3/3]
Days of the Week:

Day: Monday
The day matches ""Saturday"", display ""Weekend""
The day matches ""Sunday"", display ""Weekend""
For all other days, display ""Workday""

[EXPECTED OUTPUT]
Workday",
        };
    }
}
