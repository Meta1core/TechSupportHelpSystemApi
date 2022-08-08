using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechSupportHelpSystem.Models.DTO;
using System.Data.Common;

namespace TechSupportHelpSystem.Services.Auth
{
    interface IAuthenticationService
    {
        public Task<bool> Login(UserAuthenticationDto userAuthentication);
    }
}
