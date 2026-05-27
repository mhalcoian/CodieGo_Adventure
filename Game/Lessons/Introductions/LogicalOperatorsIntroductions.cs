using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class LogicalOperatorsIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Welcome back! Now, we are going to learn about logical operations.",
                    "Logical operations are used to combine or modify true and false values. Just like relational operations, the result of a logical operation is always either true or false.",
                    "In this stage, we will focus on the AND operator, which is written using two ampersands.",
                    "The AND operator checks two conditions at the same time.",
                    "Here is the important rule to remember:\r\nFor AND to return true, both values must be true.\r\nIf even one value is false, the result becomes false.",
                    "In this task, both boolean variables have the value true. Since both sides are true, the AND condition will return true.",
                    "Your goal is to check this condition and display the correct message showing the result.",
                    "Take your time and remember: AND is very strict. Both sides must be true.",
                },
                1 => new List<string>
                {
                    "Nice work! Now, let us talk about the OR operator.",
                    "The OR operator is written using two vertical lines.",
                    "OR works differently from AND. With OR, the condition becomes true as long as at least one value is true.\r\n",
                    "Here is the key idea:\r\nIf one value is true, OR returns true.\r\nOnly when both values are false will OR return false.",
                    "In this task, one variable is false and the other is true. Since at least one value is true, the OR condition will return true.",
                    "Your job is to check this condition and display the correct output.",
                    "Understanding the difference between AND and OR is very important, so make sure you clearly see how their rules are different.",
                },
                2 => new List<string>
                {
                    "Great progress! Next, we will learn about the NOT operator.",
                    "The NOT operator is written using an exclamation mark.",
                    "NOT does something very simple but very powerful. It reverses a boolean value.",
                    "If a value is true, NOT makes it false.\r\nIf a value is false, NOT makes it true.",
                    "In this stage, you are given a boolean value and asked to apply the NOT operator to it.",
                    "Then, you will check the result and display the correct message.",
                    "This operator is commonly used when you want to check the opposite of a condition.",
                    "Take a moment to think about what the value becomes after applying NOT before writing your code.",
                },
                3 => new List<string>
                {
                    "You are doing an excellent job! Now in this stage, we will combine multiple logical operators together.",
                    "When combining logical operations, parentheses are very important.",
                    "Parentheses tell the program which condition to evaluate first, just like in math.",
                    "In this task, the program will first evaluate the condition inside the parentheses, and then combine the result with another condition using OR.",
                    "This allows you to create more complex and powerful logic.",
                    "Your goal is to carefully follow the order of operations and determine whether the final result is true or false.",
                    "Once you understand this, you will be able to build more advanced conditions in future lessons.",
                },
                _ => new List<string>()
            };
        }
    }
}
