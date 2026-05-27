namespace CodieGo_Adventure.Game.Assessments.Guides
{
    public class JudgementProtocolGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"[TASK 1/3]
Determine if the person is minor or adult.

Person’s Age: 17
If the age is 18 or higher, display: ""Adult""
If the age is higher than 0 and is 18 lower, display: ""Minor""
Otherwise, display: ""Invalid Age""
",

            [1] = @"[TASK 2/3]
Manage stock levels for a shop.

Item Stock: 8
If stock is less than or equal to 5, display ""Very Low Stock""
If stock is 10 or less, display: ""Low Stock""
Otherwise, display: ""Stock Sufficient""
",

            [2] = @"[TASK 3/3]
Determine if the number is Odd or Even.

Given Number: 3
If the given number is even, display ""Even Number""
Otherwise, display ""Odd Number""
",
        };
    }
}
