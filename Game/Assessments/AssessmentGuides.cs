using CodieGo_Adventure.Game.Assessments.Guides;
using CodieGo_Adventure.Game.Lessons.Guides;

namespace CodieGo_Adventure.Game.Assessments
{
    public class AssessmentGuides
    {
        public static string GetGuide(string lessonType, int stage)
        {
            var guides = lessonType.ToLower() switch
            {
                "first-step" => FirstStepGuides.Guides.GetValueOrDefault(stage),
                "memory-awakening" => MemoryAwakeningGuides.Guides.GetValueOrDefault(stage),
                "engine-calibration" => EngineCalibrationGuides.Guides.GetValueOrDefault(stage),
                "path-of-choice" => PathOfChoiceGuides.Guides.GetValueOrDefault(stage),
                "paths-of-destiny" => PathsOfDestinyGuides.Guides.GetValueOrDefault(stage),
                "judgement-protocol" => JudgementProtocolGuides.Guides.GetValueOrDefault(stage),
                "mind-of-logic" => MindOfLogicGuides.Guides.GetValueOrDefault(stage),
                "endless-cycle" => EndlessCycleGuides.Guides.GetValueOrDefault(stage),
                "march-of-steps" => MarchOfStepsGuides.Guides.GetValueOrDefault(stage),
                _ => throw new NotImplementedException("No guide available for this lesson type.")
            };

            return guides;
        }

        public static string GetInstructions(int order) =>
            AssessmentInstruction.Instructions.GetValueOrDefault(order);
    }
}
