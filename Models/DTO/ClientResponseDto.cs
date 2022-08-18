namespace TechSupportHelpSystem.Models.DTO
{
    public class ClientResponseDto
    {
        public int ID_Client { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Connection { get; set; }
        public bool IsBlocked { get; set; }
        public string PortalUrl { get; set; }
        public string LogoUrl { get; set; }
    }
}
