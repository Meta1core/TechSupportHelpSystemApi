using System;
using System.DirectoryServices;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models.DTO;
namespace TechSupportHelpSystem.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthResponseDto LoginActiveDirectory(UserAuthenticationDto userAuthentication)
        {
            try
            {
                SearchResultCollection results;
                DirectorySearcher ds = null;
                DirectoryEntry de = new DirectoryEntry("LDAP://10.190.100.250", userAuthentication.Username, userAuthentication.Password)
                {
                    AuthenticationType = AuthenticationTypes.FastBind
                };
                ds = new DirectorySearcher(de);
                results = ds.FindAll();
                if (results is not null)
                {
                    NLogger.Logger.Info("User " + userAuthentication.Username + " logged into the system!");
                    return new AuthResponseDto() { IsAuthSuccessful = true };
                }
                return new AuthResponseDto() { IsAuthSuccessful = false, ErrorMessage = "Invalid Authentication" };
            }
            catch (Exception e)
            {
                return new AuthResponseDto() { IsAuthSuccessful = false, ErrorMessage = e.Message };
            }
        }
    }
}
