namespace CodieGo_Adventure.Game.Assessments.Guides
{
    public class MemoryAwakeningGuides
    {
        public static readonly Dictionary<int, string> Guides = new()
        {
            [0] = @"[TASK 1/3]
Declare variables to store the following data points for a student:

Their last name
Their current grade percentage
Their total number of attendances
Their Enrollment status

[EXPECTED OUTPUT]
Last Name: Delos Reyes
Grade: 94.5
Attendance: 20
Enrolled: True",

            [1] = @"[TASK 2/3]
Declare variables to store the following data:

The first name of the person
The middle initial of the person
The last name of the person

[EXPECTED OUTPUT]
First Name: Tibursio
Middle Initial: B
Last Name: Binangkal",

            [2] = @"[TASK 3/3]
Declare variables to store the following data of a product:

The name of the product
The ID number of the product
The price of the product

[EXPECTED OUTPUT]
Product Name: Coconut Soap
Product ID: 676767
Price: 59.99",
        };
    }
}
