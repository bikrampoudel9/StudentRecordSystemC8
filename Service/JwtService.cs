using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentMangementSystemC8.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StudentMangementSystemC8.Service
{
    public class JwtService
    {

        private readonly JwtTokenInfo jwtTokenInfo;
        public JwtService(IOptions<JwtTokenInfo> jwtTokenInfo) {
            this.jwtTokenInfo = jwtTokenInfo.Value;
        }

        public string GenerateToken()
        {
            string key = jwtTokenInfo.Key;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            //signingcredentials object needs a security key and an algorithm
            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            //this is the object which stores token values. 
            var tokenObj = new JwtSecurityToken(

                issuer: jwtTokenInfo.Issuer,
                audience: jwtTokenInfo.Audience,
                expires: DateTime.UtcNow.AddMinutes(jwtTokenInfo.ExpiresInMinutes),
                //we need to pass object of SigningCredentials
                signingCredentials: signingCredentials
                );

            //token generation
            var token = new JwtSecurityTokenHandler().WriteToken(tokenObj);

            return token;
        }
    }
}
