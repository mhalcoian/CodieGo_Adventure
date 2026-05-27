using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class WhileLoopIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Welcome back! Now, let me introduce you to the while loop.",
                    "A while loop repeats a block of code as long as a condition remains true. Before each loop runs, the program checks the condition first. If the condition is true, the loop runs. If it becomes false, the loop stops.",
                    "In this stage, we start with a number and slowly increase it using an increment. This is what you called an iterator.",
                    "Increment means adding one to a value.",
                    "Inside the loop, the program will display the current assigned value, then increase it by one. This is very important. If we forget to increment, the loop will never stop.",
                    "Your task is to make the loop run while the number is less than or equal to three and display the message correctly each time.",
                    "Watch closely how the value changes as the loop runs.",
                },
                1 => new List<string>
                {
                    "Anders: Great job! Now, let us look at decrementing in a while loop.",
                    "Anders: Decrement means reducing a value by one.",
                    "Anders: This time, instead of counting up, we will count down. The loop will continue running as long as the number is greater than or equal to one.",
                    "Anders: Each time the loop runs, the current value will be displayed, then the value will decrease by one.",
                    "Anders: This type of loop is useful when you want to count backwards or reduce values step by step.",
                    "Anders: Be careful with the condition and the decrement. If they do not match, the loop may never stop or may not run at all.",
                },
                2 => new List<string>
                {
                    "Excellent work so far! Now in this stage, I want to show you that incrementing and decrementing does not always have to be by one.",
                    "You can assign a specific value to increase or decrease by.",
                    "In this task, the number will decrease by two each time the loop runs. This means the value will jump instead of moving one step at a time.",
                    "The loop will continue as long as the number remains greater than zero.",
                    "Pay close attention to how the value changes with each loop cycle.",
                    "Once you understand this, you will have more control over how your loops behave.",
                },
                3 => new List<string>
                {
                    "In the previous lesson, you learned about the while loop, which repeats a set of instructions while a condition is true.",
                    "Now, we will learn another type of loop called the do while loop.",
                    "The do while loop is very similar to the while loop, but there is one important difference. A do while loop runs the code first before checking the condition.",
                    "Because of this, the code inside the loop will always execute at least once, even if the condition becomes false later.\r\n",
                    "This type of loop is called a post-test loop, because the condition is tested after the code runs.",
                    "In this stage, you will declare a variable named num with a value of 1.",
                    "Your task is to use a do while loop to display the text \"Post-Test\" together with the value of num.",
                    "After displaying it, make sure that num increments, and the loop will continue while num is less than 5.",
                    "Follow the syntax guide if you need help, and let's continue helping Codie move forward on his adventure!",
                    "You can also try the optional task in TRY IT YOURSELF to understand the loop further",
                },
                4 => new List<string>
                {
                    "Great job! Codie just got another step closer to the treasure.",
                    "Now let us compare the while loop and the do while loop.",
                    "The while loop is called a pre-test loop. This means the condition is checked before the loop runs. If the condition is false at the beginning, the loop will not execute at all.",
                    "On the other hand, the do while loop is called a post-test loop.",
                    "This means the instructions inside the loop are executed first, and the condition is checked afterward.",
                    "Because of this, the do while loop will always run at least one time, even if the condition is false.",
                    "To see this difference clearly, try the optional task in TRY IT YOURSELF. This will help you understand how a post-test loop behaves differently from a pre-test loop.",
                    "Let’s continue the task and see what happens when the loop condition starts as false.",
                },
                _ => new List<string>()
            };
        }
    }
}
