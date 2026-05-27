using System.ComponentModel.DataAnnotations;

namespace CodieGo_Adventure.Models
{
    public class Modules
    {
        // Properties representing the modules
        [Key]
        public int ModuleId { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int TotalLessons { get; private set; }
        public int Order { get; private set; }
    }
}
