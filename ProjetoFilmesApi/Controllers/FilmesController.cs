using Microsoft.AspNetCore.Mvc;
using ProjetoFilmesApi.Modelo;
using ProjetoFilmesApi.Servico;
using System.Runtime.CompilerServices;

namespace ProjetoFilmesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmesController : Controller
    {
        private readonly FilmesServico _filmesServico;

        public FilmesController(FilmesServico filmesServico)
        {
            _filmesServico = filmesServico;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Filme> filmes = new List<Filme>();

            filmes = _filmesServico.GetAllFilmes();

            return Ok(filmes.ToArray());
        }

        [HttpGet("{usuarioId}")]
        public IActionResult GetFilmesPorUsuario(int usuarioId)
        {
            List<Filme> filmes = new List<Filme>();

            filmes = _filmesServico.GetFilmesByUsuario(usuarioId);

            return Ok(filmes.ToArray());
        }

        [HttpGet("Titulo")]
        public IActionResult GetFilmesPorTitulo([FromQuery] Filtro filtro)
        {
            List<Filme> filmes = new List<Filme>();

            if (string.IsNullOrEmpty(filtro.Texto))
            {
                filmes = _filmesServico.GetAllFilmes();
            }
            else
            {
                filmes = _filmesServico.GetFilmesByTitulo(filtro.Texto);
            }

            return Ok(filmes.ToArray());
        }

        [HttpPost]
        public IActionResult Post(InserirFilme filme)
        {

            var novoFilme = _filmesServico.InserirFilme(filme);

            return Ok(novoFilme);
        }
    }
}
