namespace CodieGo_Adventure.Game.Assessments.Guides
{
    public class MindOfLogicGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"[TASK 1/3]
Gender Abbreviations:

Gender: m
If Gender is equal to 'M' or equal to 'm', display ""Male""
If Gender is equal to 'F' or equal to 'f', display ""Female""
Otherwise, display ""Invalid Gender""
",

            [1] = @"[TASK 2/3]
Temperature Classification:

Temperature: 25
If Temperature is greater than or equal to 30, display ""HOT""
If Temperature is greater than or equal to 20 and less than or equal to 29, display ""WARM""
If Temperature is greater than or equal to 1 and less than or equal to 19, display ""COLD""
",

            [2] = @"[TASK 3/3]
Automatic Sprinkler System:

Is it raining?: False
Use NOT (!) Operation in your condition.
If it is not raining, display ""Sprinklers ON""
Otherwise, display ""Sprinklers OFF""
",
        };
    }
}
