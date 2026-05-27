using CodieGo_Adventure.Game.Lessons.Guides;
using CodieGo_Adventure.Game.Lessons.Notes;
using CodieGo_Adventure.Game.Lessons.Task;

namespace CodieGo_Adventure.Game.Lessons
{
    public static class LessonGuides
    {
        public static string GetGuide(string lessonType, int stage)
        {
            var guides = lessonType.ToLower() switch
            {
                "output-statement" => OutputGuides.Guides.GetValueOrDefault(stage),
                "variables-and-data-types" => VariablesGuides.Guides.GetValueOrDefault(stage),
                "arithmetic-operations" => ArithmeticGuides.Guides.GetValueOrDefault(stage),
                "conditional-statement" => ConditionalGuides.Guides.GetValueOrDefault(stage),
                "switch-statement" => SwitchGuides.Guides.GetValueOrDefault(stage),
                "relational-operation" => RelationalGuides.Guides.GetValueOrDefault(stage),
                "logical-operation" => LogicalGuides.Guides.GetValueOrDefault(stage),
                "while-loop" => WhileGuides.Guides.GetValueOrDefault(stage),
                "for-loop" => ForGuides.Guides.GetValueOrDefault(stage),
                _ => throw new NotImplementedException("No guide available for this lesson type.")
            };

            return guides;
        }

        public static string GetTask(string lessonType, int stage)
        {
            var tasks = lessonType.ToLower() switch
            {
                "output-statement" => OutputTask.Tasks.GetValueOrDefault(stage),
                "variables-and-data-types" => VariableTask.Tasks.GetValueOrDefault(stage),
                "arithmetic-operations" => ArithmeticTask.Tasks.GetValueOrDefault(stage),
                "conditional-statement" => ConditionalTask.Tasks.GetValueOrDefault(stage),
                "switch-statement" => SwitchStatementTask.Tasks.GetValueOrDefault(stage),
                "relational-operation" => RelationalOperatorsTask.Tasks.GetValueOrDefault(stage),
                "logical-operation" => LogicalOperatorsTask.Tasks.GetValueOrDefault(stage),
                "while-loop" => WhileLoopTask.Tasks.GetValueOrDefault(stage),
                "for-loop" => ForLoopTask.Tasks.GetValueOrDefault(stage),
                _ => throw new NotImplementedException("No guide available for this lesson type.")
            };

            return tasks;
        }

        public static string GetNotes(string lessonType, int stage)
        {
            var notes = lessonType.ToLower() switch
            {
                "output-statement" => OutputNotes.Notes.GetValueOrDefault(stage),
                "variables-and-data-types" => VariablesNotes.Notes.GetValueOrDefault(stage),
                "arithmetic-operations" => ArithmeticNotes.Notes.GetValueOrDefault(stage),
                "conditional-statement" => ConditionalNotes.Notes.GetValueOrDefault(stage),
                "switch-statement" => SwitchNotes.Notes.GetValueOrDefault(stage),
                "relational-operation" => RelationalNotes.Notes.GetValueOrDefault(stage),
                "logical-operation" => LogicalNotes.Notes.GetValueOrDefault(stage),
                "while-loop" => WhileNotes.Notes.GetValueOrDefault(stage),
                "for-loop" => ForNotes.Notes.GetValueOrDefault(stage),
                _ => throw new NotImplementedException("No guide available for this lesson type.")
            };

            return notes;
        }
    }
}
