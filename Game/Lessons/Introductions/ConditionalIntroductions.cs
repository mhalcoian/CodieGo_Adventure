using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class ConditionalIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Welcome back! Now that you can work with numbers and calculations, it’s time to teach you how to make decisions.",
                    "In programming, decisions are made using something called conditional statements.",
                    "A conditional statement allows your program to check a condition and decide what to do next based on whether that condition is true or false.",
                    "The most basic conditional statement is the if statement.",
                    "An if statement works like this:\r\n “If a condition is true, then do this.”",
                    "In this stage, you have a variable named num with a value of 10.",
                    "Your task is to check if num is greater than 5.\r\n If that condition is true, the program should display the message “Greater than 5”.",
                    "The code inside the curly braces { } only runs when the condition is true.",
                    "Since 10 is greater than 5, the condition should pass and the message will be displayed.",
                    "Follow the structure carefully, and place your output inside the if block.",
                },

                1 => new List<string>
                {
                    "Nice work! Now let’s make your program a little smarter.",
                    "Sometimes, you want your program to do one thing if a condition is true, and another thing if it’s false.",
                    "This is where the if–else statement comes in.",
                    "Think of it this way:\r\n “If this happens, do this. Otherwise, do something else.”",
                    "In this stage, the value of num is 3. Your program needs to check if num is greater than 5.\r\n If it is, display “Passed”.\r\n If it is not, display “Failed”.",
                    "Since 3 is not greater than 5, the condition is false, and the code inside the else block will run.\r\n",
                    "Make sure your messages are placed in the correct blocks so the logic works properly.",
                },

                2 => new List<string>
                {
                    "Great job again! You’re getting really good at making decisions in your programs.",
                    "So far, you’ve learned how to use if to check one condition. You also learned how to use if–else to choose between two possible outcomes.",
                    "But what if you have more than two possible conditions? What if your program needs to check multiple specific values? That’s where the else if statement comes in.",
                    "The else if statement allows your program to check another condition if the first one is false.",
                    "Think of it like this:\r\n “If this condition is true, do this.\r\n If not, check another condition.\r\n If that one is true, do something else.”",
                    "The program checks conditions one by one from top to bottom. As soon as it finds a condition that is true, it runs that block of code and skips the rest.",
                    "In this stage, you need to declare an integer variable named num and assign it a value of 2. Your program should first check if num is equal to 3. If that condition is true, display: “The number is 3.”",
                    "If that condition is false, the program should check another condition using else if.  Now check if num is equal to 2. If that condition is true, display: “The number is 2.”",
                    "If that is also false, check one more condition. Use another else if to check if num is equal to 1.  If it is true, display: “The number is 1.”",
                    "Remember, since num is 2, the program will first check if it equals 3. That’s false. Then it will move to the next condition and check if it equals 2. That one is true!",
                    "So the program will display: “The number is 2.” and stop checking the remaining conditions.",
                    "Make sure your conditions are written in the correct order, and don’t forget to use the equality operator == when comparing values.",
                    "Let’s see if you can make your program choose the correct output!",
                },

                3 => new List<string>
                {
                    "Excellent work so far! Now it’s time for you to try one on your own.",
                    "You will declare a double variable named Num and give it any value you want.",
                    "Your task is to check whether the value of Num is less than 10.",
                    "This challenge helps you practice writing conditions independently and choosing the correct comparison.",
                    "There’s no single correct number here, as long as your logic works.",
                },

                _ => new List<string>()
            };
        }
    }
}
