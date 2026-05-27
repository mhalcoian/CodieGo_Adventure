using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class SwitchStatementIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Welcome back! Thank you for you help, Codie is halfway in collecting all the treasures.",
                    "Up to now, you’ve used if and if-else statements to make decisions.\r\nNow, I’ll introduce you to another decision-making tool called the switch statement.",
                    "A switch statement is useful when you want to check one variable against many possible values.\r\nInstead of writing many if-else statements, switch lets you organize your choices more cleanly.",
                    "Here’s how it works:\r\nThe switch checks the value of a variable.\r\nEach case represents a possible value.\r\nWhen a matching case is found, its code runs.",
                    "The break keyword stops the switch so it doesn’t continue checking other cases.",
                    "In this task, your variable is choice with a value of 1.\r\nYour goal is to use a switch statement so that when the value is 1, the program displays Option 1.",
                    "Help Codie get rid of this obstacle by choosing the correct case.",
                },
                1 => new List<string>
                {
                    "Great job! Codie is now one last step for another treasure.",
                    "Ever wonder what happens if none of the cases match the value?",
                    "This is where the default case comes in.",
                    "The default case runs when no case matches the value of the variable. Think of it as the switch statement’s safety net.",
                    "In this task, the variable day has a value of 2, but only case 1 is provided.",
                    "Since there is no matching case, the program should fall back to the default case and display Invalid Day.",
                    "This helps your program handle unexpected values properly — something very important in real programs.",
                    "Once you complete this, Codie will finally secure another treasure.",
                },
                _ => new List<string>()
            };
        }
    }
}
