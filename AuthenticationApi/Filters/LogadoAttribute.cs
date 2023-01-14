using Microsoft.AspNetCore.Mvc.Filters;
using Jose;
using AuthenticationApi.Dtos;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AuthenticationApi.Filters
{
    public class LogadoAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            if (string.IsNullOrEmpty(filterContext.HttpContext.Request.Headers["Authorization"]))
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                await filterContext.HttpContext.Response.WriteAsJsonAsync(new
                {
                    Mensagem = "Token JWT obrigatório"
                });
                return;
            }

            var token = filterContext.HttpContext.Request.Headers["Authorization"].ToString();

            string json = string.Empty;

            try
            {
                json = Jose.JWT.Decode(token);
            }
            catch
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                await filterContext.HttpContext.Response.WriteAsJsonAsync(new
                {
                    Mensagem = "Token inválido"
                });
                return;
            }

            var administradorLogado = JsonSerializer.Deserialize<AdministradorJwtDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (administradorLogado is null)
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                await filterContext.HttpContext.Response.WriteAsJsonAsync(new
                {
                    Mensagem = "Token inválido"
                });
                return;
            }

            if (administradorLogado.Expiracao < DateTime.Now)
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                await filterContext.HttpContext.Response.WriteAsJsonAsync(new
                {
                    Mensagem = "Token expirado"
                });
                return;
            }

            await base.OnActionExecutionAsync(filterContext, next);
        }
    }
}
