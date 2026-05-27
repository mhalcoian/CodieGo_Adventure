using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class ArithmeticIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Welcome back! Now that you know how to store values using variables, it’s time to actually do something with them.",
                    "This lesson is all about Arithmetic Operations. These are basic math operations that allow your program to calculate values, just like a calculator.",
                    "Let’s start with addition.",
                    "In C#, addition uses the plus sign (+), the same symbol you use in math class.",
                    "Here’s what’s happening in this stage:\r\n You already have two integer variables, a and b, with predefined values. Your task is to create another variable called sum that will store the result of adding a and b.",
                    "Instead of calculating the answer yourself, you let the program do it by writing a + b.",
                    "Finally, you display the result using Console.Write() so the user can see the output.",
                    "This is an important concept. Programs don’t just store values, they process them. Take your time and follow each step carefully."
                },
                1 => new List<string>
                {
                    "Great job finishing addition! Let’s move on to another arithmetic operation: subtraction.",
                    "Subtraction uses the minus sign (-). It allows your program to find the difference between two values.",
                    "In this stage, you are given two integer variables, a and b. Your job is to subtract b from a and store the result in a new variable named diff.",
                    "Just like before, we don’t directly print the calculation. We store the result first, then display it.",
                    "This teaches you a very important habit in programming:\r\ncalculate first, display later.",
                    "Once you see the difference between the predefined values on the screen, you’ll know you did it right."
                },
                2 => new List<string>
                {
                    "Nice progress! Now let’s talk about multiplication.",
                    "In C#, multiplication uses the asterisk symbol (*). This might look a bit different from the × symbol you see in math books, but it works the same way.",
                    "You are given two variables again, a and b. This time, your task is to multiply them and store the result in a variable named prod, which stands for product.",
                    "Multiplication is often used in games and programs for things like damage calculation, scoring systems, or scaling values.",
                    "Once the multiplication is done, display the result using Console.Write().",
                    "If your output shows the product of the predefined values, you’re on the right track!"
                },
                3 => new List<string>
                {
                    "Excellent work so far! Now we’re moving on to division.",
                    "Division uses the forward slash (/) in C#.",
                    "You might notice something different in this stage. The variables are now of type double, not int. Why? Because division can produce decimal values, and integers can’t store decimals properly.",
                    "Here, you will divide a by b and store the result in a variable called quot, which stands for quotient.",
                    "Using double ensures that the answer is accurate and not cut off.",
                    "When you display the result, you should see the quotient of the predefined values.",
                    "This is a great example of choosing the right data type for the job."
                },
                4 => new List<string>
                {
                    "Great job reaching the final stage! Now let’s learn about a special arithmetic operation called modulus.",
                    "Modulus uses the percent sign (%). Instead of giving you the result of division, it gives you the remainder.",
                    "For example, when you divide 11 by 2, the remainder is 1. That’s exactly what modulus calculates.",
                    "This operation is very useful in programming. It’s often used to check if a number is even or odd, control patterns, or limit values.",
                    "In this stage, you will calculate the predefined values and store the result in a variable named mod.",
                    "After displaying the result, you should see it as 0.",
                    "Once you understand modulus, you’ve officially mastered all the basic arithmetic operations!"
                },
                _ => new List<string>()
            };
        }
    }
}
