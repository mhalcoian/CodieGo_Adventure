namespace CodieGo_Adventure.Models
{
    public class CustomErrorHandling
    {
        // Properties to hold error handling details
        public string Title { get; set; }
        public string Message { get; set; }
        public string YesURL { get; set; }
        public string YesText { get; set; }
        public string NoText { get; set; }

        public CustomErrorHandling(params string[] propertyNames)
        {
            Title = propertyNames[0]; // Title parameter
            Message = propertyNames[1]; // Message parameter
            YesURL = propertyNames[2]; // YesURL parameter
            YesText = propertyNames[3]; // YesText parameter
            NoText = propertyNames[4]; // NoText parameter
        }
    }
}
