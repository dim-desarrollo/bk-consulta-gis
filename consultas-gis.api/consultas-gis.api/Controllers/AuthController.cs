using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using consultas_gis.api.DTOs;
using consultas_gis.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace consultas_gis.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Response> Auth(UserDto user)
        {
            if (user.NameUser.Equals("isaias") && user.Password.Equals("$FINO"))
            {
                return Ok(new Response
                {
                    Status = 200,
                    Content = $"Token = {CreateToken(user.NameUser)}" 
            });
            }

            return BadRequest(new Response
            {
                Status = 400,
                Content = null
            });

        }


        private string CreateToken(string user)
        {
            var tokenHangler = new JwtSecurityTokenHandler();
            var byteKey = Encoding.UTF8.GetBytes("kn5ln23nm4jn5kj43n1kn43325nkj6543");
            var tokerDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHangler.CreateToken(tokerDes);
            return tokenHangler.WriteToken(token);
        }


    }
}

