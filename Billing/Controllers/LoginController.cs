using Billing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //1.dependency injection for configuration
        private readonly IConfiguration _config;

        //constructor injection
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        //3 httpPost
        [HttpPost("token")]
        public IActionResult Login([FromBody] UserModel user)
        {
            //checking Unauthorised or not
            IActionResult response = Unauthorized();

            //Authenticate the user
            var loginUser = AuthenticateUser(user);

            //validate the user and generate Jwt token
            if (loginUser != null)
            {
                var tokenString = GenerateJWTToken(loginUser);
                response = Ok(new { token = tokenString });
            }
            return response;
        }



        //4 Authenticate User
        private UserModel AuthenticateUser(UserModel user)
        {
            UserModel loginUser = null;

            //validate the user credentials
            //retrieve data from database
            if (user.UserName == "Rizwan")
            {
                loginUser = new UserModel
                {
                    UserName = "Rizwan",
                    EmailAddress = "rizulullu@gmail.com",
                    DateOfJoining = new DateTime(2020, 12, 10),
                    Role = "Administrator"
                };
            }
            return loginUser;
        }


        //5 Generate JWT token
        private string GenerateJWTToken(UserModel loginUser)
        {
            //securitykey
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));


            //signing credential
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claims---roles

            //generate token
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],//header
                _config["Jwt:Issuer"],//payload
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

