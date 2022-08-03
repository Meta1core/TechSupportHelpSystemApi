using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models
{
    public class TeachingCollection
    {
        [Key]
        [DefaultValue(null)]
        public int? ID_TeachingCollection { get; set; }
        public string Name { get; set; }
    }
}
