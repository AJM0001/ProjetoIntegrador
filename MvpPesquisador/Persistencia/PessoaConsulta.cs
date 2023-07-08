using Npgsql;
using System.Text;

namespace MvpPesquisador.Persistencia
{
    public class PessoaConsulta:PersistenciaBase<PessoaConsulta>
    {
        private Modelo.Pesquisador CriarModeloPesquisador()
        {
            return new Modelo.Pesquisador();
        }

        private string CamposTabelaPesquisador = "(Id, Nome, CodigoInstituicao, Formacao, Lattes)";

        public void Inserir(Modelo.Pesquisador modelo) 
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine(string.Format("INSERT INTO PESQUISADOR {0}" +
                " VALUES ( @Id,@Nome,@CodigoInstituicao,@Formacao, @Lattes);", CamposTabelaPesquisador));

            NpgsqlCommand comando = new NpgsqlCommand(sb.ToString(), Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", modelo.Id));
            comando.Parameters.Add(new NpgsqlParameter("Nome", modelo.Nome));
            comando.Parameters.Add(new NpgsqlParameter("CodigoInstituicao", modelo.CodigoInstituicao));
            comando.Parameters.Add(new NpgsqlParameter("Formacao", modelo.Formacao));
            comando.Parameters.Add(new NpgsqlParameter("Lattes", modelo.Lattes));

            comando.ExecuteNonQuery();

        }

        public List<Modelo.Pesquisador> BuscarTudoPesquisador()
        {
            List<Modelo.Pesquisador> retorno = new List<Modelo.Pesquisador>();
            NpgsqlCommand comando = new NpgsqlCommand("select * from pesquisador;", Conexao);

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var teste = CriarModeloPesquisador();

                teste.Id = leitor.GetInt32(0);
                teste.Nome = leitor.GetString(1);
                teste.CodigoInstituicao = leitor.GetInt32(2);
                teste.Formacao = leitor.GetString(3);
                teste.Lattes = leitor.GetString(4);

                retorno.Add(teste);
            }
            leitor.Close();

            return retorno;
        }

        private Modelo.Aluno CriarModeloAluno()
        {
            return new Modelo.Aluno();
        }

        private string CamposTabelaAluno = "(Id, Nome, CodigoInstituicao, Curso)";

        public void Inserir(Modelo.Aluno modelo)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("INSERT INTO ALUNO {0}" +
                " VALUES ( @Id,@Nome,@CodigoInstituicao,@Curso);", CamposTabelaAluno));

            NpgsqlCommand comando = new NpgsqlCommand(sb.ToString(), Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", modelo.Id));
            comando.Parameters.Add(new NpgsqlParameter("Nome", modelo.Nome));
            comando.Parameters.Add(new NpgsqlParameter("CodigoInstituicao", modelo.CodigoInstituicao));
            comando.Parameters.Add(new NpgsqlParameter("Curso", modelo.Curso));

            comando.ExecuteNonQuery();

        }

        public List<Modelo.Aluno> BuscarTudoAluno()
        {
            List<Modelo.Aluno> retorno = new List<Modelo.Aluno>();
            NpgsqlCommand comando = new NpgsqlCommand("select * from aluno;", Conexao);

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var teste = CriarModeloAluno();

                teste.Id = leitor.GetInt32(0);
                teste.Nome = leitor.GetString(1);
                teste.CodigoInstituicao = leitor.GetInt32(2);
                teste.Curso = leitor.GetString(3);

                retorno.Add(teste);
            }
            leitor.Close();

            return retorno;
        }

        public Modelo.Aluno BuscaAlunoPorId(int id)
        {
            var sql = "SELECT * FROM ALUNO WHERE Id = @Id";

            NpgsqlCommand comando = new NpgsqlCommand(sql, Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", id));

            NpgsqlDataReader leitor = comando.ExecuteReader();

            Modelo.Aluno aluno = new Modelo.Aluno();

            while (leitor.Read())
            {
                var teste = CriarModeloAluno();

                aluno.Id = leitor.GetInt32(0);
                aluno.Nome = leitor.GetString(1);
                aluno.CodigoInstituicao = leitor.GetInt32(2);
                aluno.Curso = leitor.GetString(3);

                
            }
            leitor.Close();

            return aluno;
        }

        public Modelo.Pesquisador BuscaPesquisadorPorId(int id)
        {
            var sql = "SELECT * FROM PESQUISADOR WHERE Id = @Id";

            NpgsqlCommand comando = new NpgsqlCommand(sql, Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", id));

            NpgsqlDataReader leitor = comando.ExecuteReader();

            Modelo.Pesquisador pesquisador = new Modelo.Pesquisador();

            while (leitor.Read())
            {
                pesquisador.Id = leitor.GetInt32(0);
                pesquisador.Nome = leitor.GetString(1);
                pesquisador.CodigoInstituicao = leitor.GetInt32(2);
                pesquisador.Formacao = leitor.GetString(3);
                pesquisador.Lattes = leitor.GetString(3);


            }
            leitor.Close();

            return pesquisador;
        }
    }
}
