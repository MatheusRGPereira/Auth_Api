using AuthenticationApi.Context;
using AuthenticationApi.Dtos;
using AuthenticationApi.Services;
using AuthenticationApi.Services.Autenticacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Controllers
{

    public class LoginController : ControllerBase
    {
        private readonly DBContext _context;

        public LoginController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] AdministradorDto administradorDto)
        {
            if (string.IsNullOrEmpty(administradorDto.Email) || string.IsNullOrEmpty(administradorDto.Senha)) 
            {
                return StatusCode(400, new
                {
                    Mensagem = "Prencha o email e a senha"
                });

            }
            string email = administradorDto.Email;
            string senha = administradorDto.Senha;
            var administrador = await _context.Administradores.Where(a => a.Email == email && a.Senha == senha).FirstOrDefaultAsync();
            if (administrador == null)
            {
                return StatusCode(404, new
                {
                    Mensagem = "Usuario ou senha não encontrados"
                });
            }
                var admLogado = BuilderService<AdministradorLogado>.Builder(administrador);
                admLogado.Token = TokenJWT.Builder(admLogado);
                return StatusCode(200, admLogado);
        }
    }
}
