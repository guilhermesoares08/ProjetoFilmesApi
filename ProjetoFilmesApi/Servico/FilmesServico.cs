using ProjetoFilmesApi.Banco;
using ProjetoFilmesApi.Modelo;

namespace ProjetoFilmesApi.Servico
{
    public class FilmesServico
    {
        private readonly FilmeCadastro _filmesCadastro;
        public FilmesServico(FilmeCadastro filmesCadastro)
        {
            _filmesCadastro = filmesCadastro;
        }

        public List<Filme> GetAllFilmes()
        {
            var list = _filmesCadastro.GetAllFilmes();

            foreach (var filme in list) 
            {
                filme.Ator = _filmesCadastro.GetAtorById(filme.AtorId);
            }

            return list;
        }

        public List<Ator> GetAllAtores()
        {
            var list = _filmesCadastro.GetAllAtores();

            return list;
        }

        public bool Login(Login login)
        {
            bool autorizado = _filmesCadastro.UsuarioAutorizado(login);

            return autorizado;
        }

        public List<Filme> GetFilmesByUsuario(int usuarioId)
        {
            var list = _filmesCadastro.GetFilmesByUsuario(usuarioId);

            if (list != null && list.Count > 0)
            {
                foreach (var filme in list)
                {
                    filme.Ator = _filmesCadastro.GetAtorById(filme.AtorId);
                }
            }

            return list;
        }

        public List<Filme> GetFilmesByTitulo(string titulo)
        {
            var list = _filmesCadastro.GetFilmesByTitulo(titulo);

            if (list != null && list.Count > 0)
            {
                foreach (var filme in list)
                {
                    filme.Ator = _filmesCadastro.GetAtorById(filme.AtorId);
                }
            }

            return list;
        }

        public Filme InserirFilme(InserirFilme filme)
        {
            Filme f = new Filme();

            f.Titulo = filme.Titulo;
            f.Nota = filme.Nota;
            f.AtorId = filme.AtorId;
            f.Resenha = filme.Resenha;
            f.Genero = filme.Genero;

            var novoFilme = _filmesCadastro.InserirFilme(f);

            _filmesCadastro.InserirFilmeUsuario(filme.UsuarioId, novoFilme.Id);

            return novoFilme;
        }
    }
}
