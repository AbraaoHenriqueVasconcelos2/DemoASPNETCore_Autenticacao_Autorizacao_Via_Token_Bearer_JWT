using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DemoASPNETCore_Autenticacao_Autorizacao_Via_Token_Bearer_JWT.Models;
using Microsoft.IdentityModel.Tokens;


namespace DemoASPNETCore_Autenticacao_Autorizacao_Via_Token_Bearer_JWT.Services
{
    public static class TokenService
    {
        /*
        O ASP.NET Core possui um JwtSecurityTokenHandler, que vai fazer todo o trabalho duro para nós, precisamos apenas passar para ele um TokenDescriptor, que nada mais é do que a definição do token a ser gerado, com os Claims a data de expiração e as credenciais de acesso.

        */
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
