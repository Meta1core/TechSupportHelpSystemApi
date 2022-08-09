using System;

namespace TechSupportHelpSystem.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        internal static string FindFirst(object name)
        {
            throw new NotImplementedException();
        }
    }
}
