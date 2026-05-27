using CodieGo_Adventure.DTO;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodieGo_Adventure.Game.Challenges
{
    public static class ChallengePool
    {
        public static List<ChallengeDefinition> All = new()
        {
            // arithmetic
            new ChallengeDefinition
            {
                Id = 1,
                Title = "EASY",
                ExpectedOutput =
                    @"30, 6
                    36
                    24
                    180
                    5
                    0",
                ScorePerDifficulty = 250,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Given the number 30 as the first number and 6 as the second number:
    1.) Display both numbers on the first line, separated by a comma and a single space.
    2.) Calculate the sum, difference, product, quotient, and remainder(modulo) using the numbers provided.
    3.) Display each result on its own separate line, following the order listed in Step 2.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<BinaryExpressionSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 2,
                Title = "NORMAL",
                ExpectedOutput =
                    @"8, 10
                    18",
                ScorePerDifficulty = 500,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Given the numbers 8 and 10:
    1.) Display both numbers on the first line, separated by a
    comma and a single space.
    2.) Identify which arithmetic operation results in 18 when
    applied to the numbers provided.
    3.) Display the result on a new line.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<BinaryExpressionSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 3,
                Title = "EXPERT",
                ExpectedOutput =
                    @"13, 21, 9
                    202",
                ScorePerDifficulty = 1000,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Given the numbers 13, 21, and 9:
    1.) Display the provided numbers on the first line, separated by a comma and a single space.
    2.) Identify which arithmetic operations results in 202 when
    applied to the numbers provided.
    3.) Display the result on a new line.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<BinaryExpressionSyntax>().Any()
            },

            // conditional
            new ChallengeDefinition
            {
                Id = 4,
                Title = "EASY",
                ExpectedOutput =
                    @"50
                    The number is greater than 25",
                ScorePerDifficulty = 250,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Given the number 50:
    1.) Display the provided number on the first line
    2.) Use Conditional Statement to check if the provided number is greater than 25.
    3.) Display “The number is greater than 25.” if the condition is true, Otherwise, Display ""The number is less than or equal to 25"".",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<IfStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 5,
                Title = "EXPERT",
                ExpectedOutput =
                    @"Andrew met the required grade
                    Andrew did not meet the required attendance
                    Andrew FAILED in his subject",
                ScorePerDifficulty = 1000,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Andrew has a grade of 83 and a total attendance of 25 in his subject:
    1.) He needs 80 or higher to meet the grade requirement.
    2.) He needs at least 30 to meet the attendance requirement.
    3.) He needs to meet both requirements to pass.
    4.) On the first line, Display ""Andrew met the required grade"" if he meets the required grade. Otherwise, Display ""Andrew did not meet the required grade"".
    5.) On the second line, Display ""Andrew met the required attendance"" if he meets the required attendance. Otherwise, Display ""Andrew did not meet the required attendance"".
    6.) On the third line, Display ""Andrew PASSED in his subject"" if he meets both requirements. Otherwise, Display ""Andrew FAILED in his subject"".",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<IfStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 6,
                Title = "NORMAL",
                ExpectedOutput =
                    @"15
                    No condition is matching",
                ScorePerDifficulty = 500,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Given the number 15:
    1.) Display the provided number on the first line.
    2.) Use Conditional Statement to check if the provided number matches this conditions:
    First Condition: Is the number greater than 20?
    Second Condition: Is the number greater than 30?
    3.) Display ""The number is greater than 20"" if the first condition is true. Display ""The number is greater than 30"" if the second condition is true. Otherwise, Display ""No condition is matching"".",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<IfStatementSyntax>().Any()
            },

            // while or for
            new ChallengeDefinition
            {
                Id = 7,
                Title = "NORMAL",
                ExpectedOutput = "10 8 6 4 2",
                ScorePerDifficulty = 500,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Using Loop, Display all the even numbers from 2 up to 10 in descending order. The numbers must be displayed in one line, separated with a single spacing.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 8,
                Title = "EXPERT",
                ExpectedOutput =
                    @"12345
                    54321",
                ScorePerDifficulty = 1000,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Given the the number 12345 as a value, display the given number and the reversed given number to making  it 54321.

    Given the number 12345:
    1.) Display the provided number on the first line.
    2.) Use loop, division, multiplication and modulo to reverse the digits of the provided number.
    3.) Display the reversed number.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 9,
                Title = "EXPERT",
                ExpectedOutput =
                    @"456789
                    6",
                ScorePerDifficulty = 1000,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Given the number 456789:
    1.) Display the provided number on the first line.
    2.) Using loop and division, determine the total digits of the provided number.
    3.) Display the total digits of the provided number.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 10,
                Title = "EASY",
                ExpectedOutput =
                    @"12
                    11
                    10
                    9
                    8
                    7
                    6
                    5
                    4",
                ScorePerDifficulty = 250,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Using loop, Display all the numbers from 4 to 12 in descending order. Each numbers should be displayed on its own separate line.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            // for
            new ChallengeDefinition
            {
                Id = 11,
                Title = "NORMAL",
                ExpectedOutput = "76",
                ScorePerDifficulty = 500,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Using loop, from numbers 6 to 13, calculate the sum of all numbers within the given range then display the result.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 12,
                Title = "NORMAL",
                ExpectedOutput = "2 4 6 8 10",
                ScorePerDifficulty = 500,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Using loop, Display all the the even numbers from 2 up to 10 in ascending order. The numbers must be displayed in one line, separated with a single spacing.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 13,
                Title = "NORMAL",
                ExpectedOutput = "1 3 5 7 9 11",
                ScorePerDifficulty = 500,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Using loop, display all the odd numbers from 1 up to 11 in ascending order. The numbers must be displayed in one line, separated with a single spacing.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 14,
                Title = "EXPERT",
                ExpectedOutput =
                    @"0
                    1
                    1
                    2
                    3
                    5
                    8
                    13
                    21
                    34",
                ScorePerDifficulty = 1000,
                Badge = new Badge
                {
                    badgeId = 14,
                    badgeName = "Genius Mind",
                    badgeDescription = "Awarded for successfully solving the hardest challenge.",
                },

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Using loop, generate and display the first 10th terms of the Fibonacci sequence. Each term must be displayed on its own separate line.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            new ChallengeDefinition
            {
                Id = 15,
                Title = "EASY",
                ExpectedOutput = "16 15 14 13 12 11 10 9 8",
                ScorePerDifficulty = 250,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Friday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Using loop, Display all the numbers from 8 to 16 in descending order. The numbers must be displayed in one line, separated with a single spacing.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<WhileStatementSyntax>().Any() ||
                    root.DescendantNodes().OfType<ForStatementSyntax>().Any()
            },

            // switch
            new ChallengeDefinition
            {
                Id = 16,
                Title = "EASY",
                ExpectedOutput = "Saturday",
                ScorePerDifficulty = 250,

                AvailableDays = new HashSet<DayOfWeek> {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                },

                Guide =
                    @"TASK:
    Write a switch case that handles the days of the weekend, and the default case displays ""Invalid weekend day"". Display ""Saturday"" using the case value.",

                RequiredLogic = root =>
                    root.DescendantNodes().OfType<SwitchStatementSyntax>().Any()
            }
        };
    }
}
