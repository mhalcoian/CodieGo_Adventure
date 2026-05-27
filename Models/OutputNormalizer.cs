namespace CodieGo_Adventure.Models
{
    public class OutputNormalizer
    {
        public static string Normalize(string text)
        {
            return string.Join(" ",
                text.Split(new[] { ' ', '\n', '\r', '\t' },
                StringSplitOptions.RemoveEmptyEntries)
            );
        }
    }
}
