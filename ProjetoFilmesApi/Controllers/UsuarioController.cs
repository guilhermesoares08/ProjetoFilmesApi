using Microsoft.AspNetCore.Mvc;
using ProjetoFilmesApi.Modelo;
using ProjetoFilmesApi.Servico;

namespace ProjetoFilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly FilmesServico _filmesServico;

        public UsuarioController(FilmesServico filmesServico)
        {
            _filmesServico = filmesServico;
        }

        [HttpPost(Name = "Login")]
        public ActionResult Login([FromBody] Login login) 
        {
            bool usuarioAutorizado = _filmesServico.Login(login);

            if(usuarioAutorizado == true)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
