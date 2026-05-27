using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class RelationalOperatorsIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Welcome back! Now that you understand conditional statement, let us learn something called relational operations.",
                    "Relational operations are used to compare two values. When we compare values, the result is always either true or false. This is very important in programming because it helps the program make decisions.",
                    "In this stage, we are using the greater than operator, which is written as a greater than symbol.",
                    "When we write a > b, we are asking a question. Is the value of a greater than the value of b?",
                    "The answer to this question is either true or false, and that answer can be stored in a boolean variable.",
                    "A boolean variable can only hold two values: true or false. Nothing else.",
                    "For this task, you will compare two numbers and store the result of that comparison in a boolean variable named result. Then, you will display the value of result.",
                    "If your comparison is correct, the output should show true.",
                    "Take your time, and remember: you are not printing the numbers here, you are printing the result of the comparison.",
                },
                1 => new List<string>
                {
                    "Great Job! Now, let us take relational operations one step further and use them in a conditional statement.",
                    "In this stage, we will use the equality operator, written as two equal signs.",
                    "Be careful here. One equal sign is used for assigning a value, but two equal signs are used for comparing values.",
                    "When we write a == b, we are asking: Are the value of a and the value of b the same?",
                    "If they are equal, the condition becomes true and the code inside the if statement will run.",
                    "Your task is to check if two variables have the same value. If they do, display a message showing that the values are equal.",
                    "This is a very common pattern in programming, so understanding this part is extremely important.",
                    "Focus on the comparison first, then focus on what message should be displayed when the condition is true.",
                },
                2 => new List<string>
                {
                    "Great progress! Next, we will learn the not equal operator. This operator is written using an exclamation mark followed by an equal sign.",
                    "When we write a != b, we are asking: Are these two values different?",
                    "If the values are not the same, the condition becomes true.",
                    "In this task, the two numbers are different, so the condition should pass and the message should be displayed.",
                    "This operator is very useful when you want the program to react only when values do not match.",
                    "As you work on this task, pay attention to how the operator changes the meaning of the condition compared to the previous stage.",
                },
                3 => new List<string>
                {
                    "Excellent work so far. Now, let us learn about the greater than or equal to operator.",
                    "This operator checks two things:\r\n Is the first value greater than the second value?\r\n Or are they exactly the same?",
                    "If either of those is true, then the condition will pass.",
                    "This is useful when you want to allow a minimum value, not just values that are strictly greater.",
                    "In this stage, you will compare two numbers and display a message only if the condition is true.",
                    "Take a moment to read the condition carefully before writing your code. Understanding what the operator checks will help you avoid mistakes.",
                },
                4 => new List<string>
                {
                    "You are doing an amazing job! Now in this stage, we will use the less than or equal to operator.",
                    "This operator checks whether the first value is smaller than the second value, or exactly the same.",
                    "If either of those is true, the condition will pass.",
                    "This kind of comparison is often used when setting limits, checking requirements, or validating values.",
                    "Carefully compare the values given in the task and decide whether the condition should be true.",
                    "Once again, focus on what the comparison means before writing your code.",
                },
                _ => new List<string>()
            };
        }
    }
}
