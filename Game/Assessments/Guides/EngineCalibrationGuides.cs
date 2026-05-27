namespace CodieGo_Adventure.Game.Assessments.Guides
{
    public class EngineCalibrationGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"[TASK 1/3]

Given Numbers: 7, 8, and 9
Operators: Addition(+) and Multiplication(*)
Target result: 65

[EXPECTED OUTPUT]
65",

            [1] = @"[TASK 2/3]

Given Numbers: 5, 15, and 60.
Operators: Subtraction(-) and Division(/)
Target result: 9

[EXPECTED OUTPUT]
9",

            [2] = @"[TASK 3/3]

Given Numbers:  5, 10, and 55.
Operators: Multiplication(*) and Modulo(%)
Target result: 25

[EXPECTED OUTPUT]
25",
        };
    }
}
