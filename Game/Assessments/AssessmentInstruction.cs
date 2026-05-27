namespace CodieGo_Adventure.Game.Assessments
{
    public class AssessmentInstruction
    {
        public static readonly Dictionary<int, string> Instructions = new()
        {
            [1] = @"Display the sentences of the given tasks using one output statement per word. Your final output must match the exact layout shown in each task.",
            [2] = @"Declare the appropriate Variables and Data Types based on the given tasks, and display the declared values. Your final output must match the exact layout shown in the expected output.",
            [3] = @"Solve the given tasks using Arithmetic Operation. For each task, make sure to use the provided numbers and the specified operators to calculate the target result.",
            [4] = @"Solve the given tasks using conditional statements. Make sure to match the required conditions for each tasks.",
            [5] = @"Solve the given tasks using switch statement.",
            [6] = @"Solve the given tasks using Relational Operations within the conditions. Make sure to match the required conditions for each tasks.",
            [7] = @"Solve the given tasks using Logical Operations within the conditions. Make sure to match the required conditions for each tasks.",
            [8] = @"Solve the given tasks using While Loop.",
            [9] = @"Solve the given tasks using For Loop.",
        };
    }
}
