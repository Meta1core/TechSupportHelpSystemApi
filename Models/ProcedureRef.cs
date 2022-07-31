using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models
{
    public class ProcedureRef
    {
        [Key]
        public int ID_ProcedureRef { get; set; }
        public string Name { get; set; }
        public char? Part { get; set; }
        public int MultipleProcedures { get; set; }
        public int ID_Modality { get; set; }
        public int? GroupColumn { get; set; }
        public int? GroupNumber { get; set; }
        public string GroupName { get; set; }
        public int? GroupOrder { get; set; }
        public string LoincNumber_Rsna { get; set; }
        public int PriorityIndex { get; set; }
        public int? Duration { get; set; }
        public string Color { get; set; }
        public bool IsHidden { get; set; }
    }
}
