using TechSupportHelpSystem.Models.DTO;

namespace TechSupportHelpSystem.Services.Auth
{
    interface IAuthenticationService
    {
        public AuthResponseDto LoginActiveDirectory(UserAuthenticationDto userAuthentication);
    }
}
