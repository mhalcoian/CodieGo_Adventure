using CodieGo_Adventure.Interfaces;

namespace CodieGo_Adventure.Game.Lessons.Introductions
{
    public class VariableIntroductions : ILessonIntroduction
    {
        public List<string> GetIntroduction(int stage)
        {
            return stage switch
            {
                0 => new List<string>
                {
                    "Hey there! Welcome to our second lesson. Before you can solve bigger challenges, you need to learn how programs store information. This is where variables come in.",
                    "Think of a variable as a box. This box has a name, and inside it, we store a value. We can use that value later whenever we want.",
                    "Now, every variable must have a data type. A data type tells the program what kind of value the box can hold. Is it text? A number? A true or false value? That’s what data types are for.",
                    "Let’s start with one of the most common data types called string.",
                    "A string is used to store text. This can be letters, words, sentences, or even symbols. In C#, text values are always wrapped inside double quotation marks.",
                    "In this stage, you will declare a string variable named text, assign a value, and then display it on the screen.",
                    "Remember: declare the variable first, give it a value, and then use Console.Write() to show it. Take your time and follow the syntax guide if you need help."
                },
                1 => new List<string>
                {
                    "Nice work! Now let’s move on to another data type called char.",
                    "The word char is short for character. This data type is used to store only one character.",
                    "A character can be a letter, a number, or a symbol, but it must be just one. Unlike strings, characters use single quotation marks, not double ones.",
                    "For example, 'A' is a character, but \"A\" is a string. That small difference is very important in C#.",
                    "In this stage, you will declare a char variable, assign a value, and display it on the screen.",
                    "Focus on using single quotes and make sure you only store one character."
                },
                2 => new List<string>
                {
                    "Great progress! Now let’s talk about numbers.",
                    "The int data type is used to store whole numbers. These are numbers without decimal points, like 1, 5, 10, or even negative numbers.",
                    "When using int, you do not need quotation marks because numbers are not text.\r\n",
                    "In this stage, you will declare an integer variable, assign a value, and display it.",
                    "This type of variable is very common in programming, especially when counting, calculating scores, or looping through values.",
                    "Once you finish this, you’ll be one step closer to real problem-solving!"
                },
                3 => new List<string>
                {
                    "Awesome! Now let’s take numbers one step further.",
                    "Sometimes, whole numbers are not enough. What if we need decimal values like prices, measurements, or averages? That’s where double comes in.",
                    "The double data type stores numbers with decimal points, such as 12.5, 3.14, or 0.75.",
                    "Just like integers, doubles don’t use quotation marks.",
                    "In this stage, you will declare a double variable named num, assign it the value 12.51, and display it.",
                    "Make sure the decimal point is included. A missing decimal can change the meaning of your value."
                },
                4 => new List<string>
                {
                    "You’re doing great! Now let’s talk about one of the most powerful data types in programming: boolean.",
                    "A bool can only have two possible values: true or false.",
                    "This data type is used when we want to check conditions, make decisions, or control the flow of a program.",
                    "For example, we can use booleans to check if a player won a game, if a password is correct, or if a condition is met.",
                    "In this stage, you will declare a boolean variable, assign it the value true, and display it.",
                    "Remember: true and false are written without quotation marks because they are not text."
                },
                _ => new List<string>()
            };
        }
    }
}
