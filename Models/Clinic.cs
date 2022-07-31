using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechSupportHelpSystem.Models
{
    public class Clinic
    {
        [Key]
        public int ID_Clinic { get; set; }
        public string Name { get; set; }
        public string Clinicname { get; set; }
        public string AddressLine { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string TimeZone { get; set; }
        public int? Group { get; set; }
        public int? Position { get; set; }
        public bool IsObsolete { get; set; }
    }
}
