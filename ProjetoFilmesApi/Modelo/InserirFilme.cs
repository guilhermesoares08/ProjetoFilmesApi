namespace ProjetoFilmesApi.Modelo
{
    public class InserirFilme
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int AtorId { get; set; }
        public int Nota { get; set; }
        public string Resenha { get; set; }
        public int UsuarioId { get; set; }
    }
}
