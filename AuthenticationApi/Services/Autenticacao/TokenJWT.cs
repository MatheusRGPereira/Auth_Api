using AuthenticationApi.Dtos;
using Jose;
using System.Security.Claims;
using System.Text;

namespace AuthenticationApi.Services.Autenticacao
{
    public class TokenJWT
    {
        public static string Builder(AdministradorLogado AdministradorLogado)
        {
            var key = "SEGREDO_do_CoDigoDO-Futuro";

            var payload = new AdministradorJwtDto
            {
                Id = AdministradorLogado.Id,
                Email = AdministradorLogado.Email,
                Permissao = AdministradorLogado.Permissao,
                Expiracao = DateTime.Now.AddDays(2)
            };

            string token = Jose.JWT.Encode(payload, key, JwsAlgorithm.none);

            return token;
        }
    }
}
