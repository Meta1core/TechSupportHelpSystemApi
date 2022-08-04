using System.ComponentModel.DataAnnotations.Schema;

namespace TechSupportHelpSystem.Models
{
    public class Configuration
    {
        [Column(TypeName = "int(11)")]
        public int? ID_Configuration { get; set; }
        [Column(TypeName = "int(11)")]
        public int? ID_Clinic { get; set; }
        [Column(TypeName = "varchar(80)")]
        public string ParameterName { get; set; }
        [Column(TypeName = "varchar(1024)")]
        public string ParameterValue { get; set; }
    }
}
