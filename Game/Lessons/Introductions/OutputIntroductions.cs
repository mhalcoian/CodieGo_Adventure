using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class OutputIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Greetings! Welcome to Codie Go Adventure. In this game, we are going to help Codie overcome the obstacles that block his way on his adventure by learning C# programming and collecting treasures.",
                    "Before anything else, let us start with the basics. First, we need to learn how to show text on the screen. This is called output. Output simply means displaying something on the screen.",
                    "In C#, we use the Console class to display text. The word console refers to the window where your program shows its result.",
                    "One of the most common commands you will use is Console.WriteLine.",
                    "WriteLine means two things: First, it prints the text you put inside it. Second, after printing, it automatically moves to the next line. Think of it like pressing the Enter key after typing a sentence.",
                    "Inside the parentheses, you place the text you want to display. The text must be enclosed in quotation marks so the program knows it is text and not code. Without quotation marks, the program will think the text is part of the code and will produce an error.",
                    "For this task, your goal is simple. You just need to display the words Hello World exactly as shown.\r\n",
                    "This is usually the first program that every beginner writes. Once you can do this, you are officially starting your programming journey",
                    "You can refer to the syntax guide for your guidance. Enjoy!",
                    "If you’re open to doing some side tasks, you can refer to the ‘Try It Yourself’ section to see what would happen in those scenarios. It will answer some of the ‘what if’ questions and give you a better idea."
                },

                1 => new List<string>
                {
                    "Great job! You have successfully completed the first stage.",
                    "Now that you already know how to use Console.WriteLine, let us take a look at another command that is very similar to it. This time, we are going to learn about Console.Write.",
                    "Console.Write also displays text on the screen, just like Console.WriteLine. However, there is one important difference between the two.",
                    "While Console.WriteLine prints the text and then automatically moves the cursor to the next line, Console.Write prints the text but it does not move to the next line afterward. This means if you use Console.Write multiple times, the text will continue on the same line.",
                    "Understanding the difference between these two commands helps you control how your output is arranged on the screen.",
                    "For this task, your goal is to display the message by using Console.Write for each word as shown on your task.",
                    "Make sure the text is enclosed in quotation marks so the program knows it is a string. Don’t forget to observe proper spacing as well.",
                    "Do not forget the semicolon at the end of your statement. Every instruction in C# must end with a semicolon.",
                    "You can refer to the syntax guide for your guidance if you need help.",
                    "Once you complete this task, you are one step closer to helping Codie collect the treasure.",
                    "Keep going — Codie is counting on you!"
                },

                2 => new List<string>
                {
                    "Great Job! Codie is one step ahead to collect the first treasure!",
                    "In this stage, you need to display the message shown in your task using Write or WriteLine for each word.",
                    "You can combine Write and WriteLine to control how your output looks. This is very useful when formatting text or building longer outputs.",
                    "Take your time to experiment. Observe how each command affects the output. Understanding this difference will help you a lot in future lessons.",
                    "Once you complete this task,  Codie can finally collect the treasure."
                },

                _ => new List<string>()
            };
        }
    }
}
