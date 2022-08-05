using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechSupportHelpSystem.Models
{
    public class OHIPClinicGroupNumber
    {
        [Key]
        public int ID_Clinic { get; set; }
        [Key]
        [Column(TypeName = "char(4)")]
        public string GroupNumber { get; set; }
        [Column(TypeName = "char(4)")]
        public string MasterNumber { get; set; }
        [Column(TypeName = "char(1)")]
        public string ID_MOHOffice { get; set; }
        [Column(TypeName = "char(10)")]
        public string EDSUserMUID { get; set; }
        [Column(TypeName = "char(4)")]
        public string SLI { get; set; }

    }
}
