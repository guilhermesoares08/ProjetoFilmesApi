using ProjetoFilmesApi.Modelo;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProjetoFilmesApi.Banco
{
    public class FilmeCadastro
    {
        //mudar Initial Catalog para filmes_cadastro depois
        private readonly string conexao = "Server=localhost;Database=agenda_filmes;Uid=root;Pwd=200296;";
        

        public List<Filme> GetAllFilmes()
        {
            var filmes = new List<Filme>();

            MySqlConnection connection = new MySqlConnection(conexao);

            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM Filmes", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Filme filme = new Filme();
                filme.Id = Convert.ToInt32(row["Id"]);
                filme.Titulo = Convert.ToString(row["Titulo"]);
                filme.Nota = Convert.ToInt32(row["Nota"]);
                filme.AtorId = Convert.ToInt32(row["AtorId"]);
                filme.Resenha = Convert.ToString(row["Resenha"]);
                filme.Genero = Convert.ToString(row["Genero"]);

                filmes.Add(filme);
            }

            connection.Close();

            return filmes;
        }

        public Ator GetAtorById(int id)
        {
            var ator = new Ator();

            MySqlConnection connection = new MySqlConnection(conexao);
            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM Atores WHERE Id = {id}", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ator.Id = Convert.ToInt32(row["Id"]);
                    ator.Nome = Convert.ToString(row["Nome"]);
                }
            }
            else
            {
                throw new Exception($"O ator {id} não existe");
            }

            connection.Close();

            return ator;
        }

        public List<Ator> GetAllAtores()
        {
            var atores = new List<Ator>();

            MySqlConnection connection = new MySqlConnection(conexao);

            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM Atores", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Ator filme = new Ator();
                filme.Id = Convert.ToInt32(row["Id"]);
                filme.Nome = Convert.ToString(row["Nome"]);

                atores.Add(filme);
            }

            connection.Close();

            return atores;
        }        

        public bool UsuarioAutorizado(Login login)
        {
            MySqlConnection connection = new MySqlConnection(conexao);

            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM Usuario WHERE Username = '{login.UserName}' AND Senha = '{login.Senha}'", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        

        public List<Filme> GetFilmesByUsuario(int usuarioId)
        {
            var filmes = new List<Filme>();

            MySqlConnection connection = new MySqlConnection(conexao);
            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand(@$"SELECT f.* FROM Filmes f
                                                        INNER JOIN FilmesUsuario fu ON fu.FilmeId = f.Id
                                                        WHERE fu.UsuarioId = {usuarioId}", connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Filme filme = new Filme();
                    filme.Id = Convert.ToInt32(row["Id"]);
                    filme.Titulo = Convert.ToString(row["Titulo"]);
                    filme.Nota = Convert.ToInt32(row["Nota"]);
                    filme.AtorId = Convert.ToInt32(row["AtorId"]);
                    filme.Resenha = Convert.ToString(row["Resenha"]);
                    filme.Genero = Convert.ToString(row["Genero"]);

                    filmes.Add(filme);
                }
            }
            else
            {
                throw new Exception($"O usuario {usuarioId} não existe");
            }

            connection.Close();

            return filmes;
        }

        public List<Filme> GetFilmesByTitulo(string titulo)
        {
            var filmes = new List<Filme>();

            MySqlConnection connection = new MySqlConnection(conexao);

            connection.Open();

            MySqlCommand sqlCommand = new MySqlCommand($"SELECT * FROM Filmes f WHERE f.Titulo LIKE '%{titulo}%'", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);
                
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Filme filme = new Filme();
                    filme.Id = Convert.ToInt32(row["Id"]);
                    filme.Titulo = Convert.ToString(row["Titulo"]);
                    filme.Nota = Convert.ToInt32(row["Nota"]);
                    filme.AtorId = Convert.ToInt32(row["AtorId"]);
                    filme.Resenha = Convert.ToString(row["Resenha"]);
                    filme.Genero = Convert.ToString(row["Genero"]);

                    filmes.Add(filme);
                }
            }

            connection.Close();

            return filmes;
        }

        public Filme InserirFilme(Filme filme)
        {
            string query = "INSERT INTO Filmes (Titulo, Nota, AtorId, Resenha, Genero) " +
               "VALUES (@Titulo, @Nota, @AtorId, @Resenha, @Genero); " +
               "SELECT LAST_INSERT_ID();";

            using (MySqlConnection connection = new MySqlConnection(conexao))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Titulo", filme.Titulo);
                command.Parameters.AddWithValue("@AtorId", filme.AtorId);
                command.Parameters.AddWithValue("@Genero", filme.Genero);
                command.Parameters.AddWithValue("@Nota", filme.Nota);
                command.Parameters.AddWithValue("@Resenha", filme.Resenha);

                connection.Open();
                filme.Id = Convert.ToInt32(command.ExecuteScalar());

                return filme;
            }
        }

        public void InserirFilmeUsuario(int usuarioId, int filmeId)
        {
            string query = @"INSERT INTO FilmesUsuario (FilmeId, UsuarioId)
                             VALUES (@FilmeId, @UsuarioId)";

            using (MySqlConnection connection = new MySqlConnection(conexao))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@FilmeId", filmeId);
                command.Parameters.AddWithValue("@UsuarioId", usuarioId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
