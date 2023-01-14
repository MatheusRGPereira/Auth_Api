using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationApi.Services.Autenticacao
{
    public class TokenJWT
    {
        public readonly static string Key = "SEGREDO_do_CoDigoDO-Futuro";
        public static string Builder(AdministradorLogado administradorLogado)
        {
            var key = Encoding.ASCII.GetBytes(Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Name, administradorLogado.Nome),
                new Claim(ClaimTypes.Role, administradorLogado.Permissao),
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
