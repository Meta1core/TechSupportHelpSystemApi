using Novell.Directory.Ldap;
using System;
using System.DirectoryServices;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models.DTO;

namespace TechSupportHelpSystem.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {

        [Obsolete("LoginActiveDirectoryOld is deprecated, please use LoginActiveDirectory instead.")]
        public AuthResponseDto LoginActiveDirectoryOld(UserAuthenticationDto userAuthentication)
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
                NLogger.Logger.Error(e);
                return new AuthResponseDto() { IsAuthSuccessful = false, ErrorMessage = e.Message };
            }
        }

        public AuthResponseDto LoginActiveDirectory(UserAuthenticationDto userAuthentication)
        {
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect("corp.velox.me", LdapConnection.DefaultPort);
                    connection.Bind(userAuthentication.Username, userAuthentication.Password);
                    if (connection.Bound)
                    {
                        NLogger.Logger.Info("User " + userAuthentication.Username + " logged into the system!");
                        return new AuthResponseDto() { IsAuthSuccessful = true };
                    }
                    return new AuthResponseDto() { IsAuthSuccessful = false, ErrorMessage = "Invalid Authentication" };
                }
            }
            catch (LdapException ex)
            {
                NLogger.Logger.Error(ex);
                return new AuthResponseDto() { IsAuthSuccessful = false, ErrorMessage = ex.Message };
            }
        }

    }
}
