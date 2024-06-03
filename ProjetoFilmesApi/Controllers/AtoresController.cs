using Microsoft.AspNetCore.Mvc;
using ProjetoFilmesApi.Modelo;
using ProjetoFilmesApi.Servico;

namespace ProjetoFilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtoresController : Controller
    {
        private readonly FilmesServico _filmesServico;

        public AtoresController(FilmesServico filmesServico)
        {
            _filmesServico = filmesServico;
        }

        [HttpGet(Name = "Atores")]
        public IEnumerable<Ator> Get()
        {
            List<Ator> atores = new List<Ator>();

            atores = _filmesServico.GetAllAtores();

            return atores.ToArray();
        }
    }
}
