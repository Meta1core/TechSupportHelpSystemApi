using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DTO;
using TechSupportHelpSystem.Services.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechSupportHelpSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthenticationService AuthenticationService;
        private JwtHandler _jwtHandler;

        public AuthenticationController(JwtHandler jwtHandler)
        {
            AuthenticationService = new AuthenticationService();
            _jwtHandler = jwtHandler;
        }
        // GET: <AuthenticationController>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserAuthenticationDto userAuthentication)
        {
            bool isLogin = await AuthenticationService.Login(userAuthentication);
            if (!isLogin)
            {
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            }
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(new User() {Username = userAuthentication.Username, Password = userAuthentication.Password  });
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
    }
}
