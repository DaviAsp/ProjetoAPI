using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [Route("[action]")]
        [HttpPost]
        public IActionResult Validar(ViewModels.LoginViewModel loginVM)
        {
            int num;
            int numet;
            var sucesso = false;
            string msg = "";
            if (loginVM.Nome == "a" && loginVM.Senha == "1")
            {
                var userClaims = new List<Claim>()
                {
                    
                    new Claim("usuarioId", "111111"),
                    new Claim("nome", "Andre")
                };

                var identity = new ClaimsIdentity(userClaims, "Identificação do Usuário");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                AuthenticationHttpContextExtensions.SignInAsync(HttpContext, principal);

                sucesso = true;
            }
            else
            {
                msg = "Dados inválidos.";
            }


            return Ok(new
            {
                sucesso,
                msg
            });
        }


    }
}
