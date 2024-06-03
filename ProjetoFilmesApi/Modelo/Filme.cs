namespace ProjetoFilmesApi.Modelo
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int AtorId { get; set; }
        public int Nota { get; set; }
        public string Resenha { get; set; }
        public Ator Ator { get; set; }
    }
}
