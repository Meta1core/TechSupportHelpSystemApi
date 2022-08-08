using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechSupportHelpSystem.Models.DTO;
using System.Data.Common;
using MySqlConnector;

namespace TechSupportHelpSystem.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> Login(UserAuthenticationDto userAuthentication)
        {
            try
            {
                String connString = "Server=10.169.100.55;Database=vi_112_amc;port=3306;User Id=" + userAuthentication.Username + ";password=" + userAuthentication.Password;
                MySqlConnection conn = new MySqlConnection(connString);
                await conn.OpenAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
