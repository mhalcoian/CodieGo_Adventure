using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class ForLoopIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Welcome back! to our last lesson! Now, it’s time to learn about the for loop.",
                    "A for loop is another way to repeat code, just like the while loop. The big difference is that a for loop keeps everything in one place: the starting value, the condition, and how the value changes.",
                    "Let me explain the three parts you see in a for loop:\r\nFirst is initialization — this is where we set the starting value.\r\nSecond is the condition — this decides how long the loop will keep running.\r\nThird is the increment — this updates the value after each loop run.",
                    "Because everything is written together, for loops are often easier to read and are commonly used when we already know how many times the loop should run.",
                    "Your task is to use a for loop to display numbers from one to five. Watch how the value increases each time the loop runs.",
                },
                1 => new List<string>
                {
                    "Nice work! Codie is now just a few steps away for the last treasure.Anders: Nice work! Codie is now just a few steps away from another treasure.",
                    "Just like the while loop, a for loop can also count backwards.",
                    "Instead of increasing the value, we use a decrement to reduce the number each time the loop runs.",
                    "In this loop, we start from five and move down until we reach one. The loop will continue as long as the condition remains true.",
                    "Pay attention to the condition and the decrement. They must work together, or the loop may stop too early—or it will not stop at all.",
                    "Once you finish this task, Codie will be able to move forward and secure the last treasure.",
                },
                _ => new List<string>()
            };
        }
    }
}
