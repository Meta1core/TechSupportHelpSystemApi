using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models
{
    public class Client
    {
        [Key]
        public int ID_Client { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Connection { get; set; }
        public bool IsBlocked { get; set; }
    }
}
