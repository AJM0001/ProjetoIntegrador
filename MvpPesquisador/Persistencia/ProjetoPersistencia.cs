using MvpPesquisador.Modelo;
using Npgsql;
using System.Text;

namespace MvpPesquisador.Persistencia
{
    public class ProjetoPersistencia : PersistenciaBase<ProjetoPersistencia>
    {
        private string CamposTabelaProjeto = "(Id, Nome, Area, Status)";
        public void Inserir(Modelo.Projeto projeto)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("INSERT INTO PROJETO {0}" +
                " VALUES ( @Id,@Nome, @Area, @Status);", CamposTabelaProjeto));

            NpgsqlCommand comando = new NpgsqlCommand(sb.ToString(), Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", projeto.Id));
            comando.Parameters.Add(new NpgsqlParameter("Nome", projeto.Nome));
            comando.Parameters.Add(new NpgsqlParameter("Area", projeto.Area));
            comando.Parameters.Add(new NpgsqlParameter("Status", projeto.Status));


            comando.ExecuteNonQuery();
            InserirProjetoPessoa(projeto);
            InserirProjetoEstabelecimento(projeto);
        }

        public void InserirProjetoPessoa(Modelo.Projeto projeto)
        {
            Random random = new Random();

            foreach (Pessoa p in projeto.Pessoas)
            {
                NpgsqlCommand comando = null;

                if (p is Modelo.Aluno)
                {
                    var sql = "INSERT INTO ProjetoPessoa (Id, AlunoId, ProjetoId) " +
                "VALUES (@Id, @AlunoId, @ProjetoId);";
                    comando = new NpgsqlCommand(sql, Conexao);

                    comando.Parameters.Add(new NpgsqlParameter("Id", random.Next()));
                    comando.Parameters.Add(new NpgsqlParameter("AlunoId", p.Id));
                    comando.Parameters.Add(new NpgsqlParameter("ProjetoId", projeto.Id));
                }
                else if (p is Modelo.Pesquisador)
                {
                    var sql = "INSERT INTO ProjetoPessoa (Id, PesquisadorId, ProjetoId) " +
                "VALUES (@Id, @PesquisadorId, @ProjetoId);";
                    comando = new NpgsqlCommand(sql, Conexao);

                    comando.Parameters.Add(new NpgsqlParameter("Id", random.Next()));
                    comando.Parameters.Add(new NpgsqlParameter("PesquisadorId", p.Id));
                    comando.Parameters.Add(new NpgsqlParameter("ProjetoId", projeto.Id));
                }

                comando.ExecuteNonQuery();
            }
        }

        public void InserirProjetoEstabelecimento(Modelo.Projeto projeto)
        {
            Random random = new Random();

            foreach (Estabelecimento e in projeto.Estabelecimento)
            {
                NpgsqlCommand comando = null;

                var sql = "INSERT INTO ProjetoEstabelecimento (Id, EstabelecimentoId, ProjetoId) " +
                "VALUES (@Id, @EstabelecimentoId, @ProjetoId);";
                comando = new NpgsqlCommand(sql, Conexao);

                comando.Parameters.Add(new NpgsqlParameter("Id", random.Next()));
                comando.Parameters.Add(new NpgsqlParameter("EstabelecimentoId", e.Id));
                comando.Parameters.Add(new NpgsqlParameter("ProjetoId", projeto.Id));


                comando.ExecuteNonQuery();
            }
        }

        public void Atualizar(Modelo.Projeto projeto)
        {
            var sql = "UPDATE PROJETO SET STATUS = @Status WHERE ID = @Id";

            NpgsqlCommand comando = new NpgsqlCommand(sql, Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", projeto.Id));
            comando.Parameters.Add(new NpgsqlParameter("Status", projeto.Status));

            comando.ExecuteNonQuery();
        }

        public List<Modelo.Projeto> BuscarTudoProjeto()
        {
            List<Modelo.Projeto> retorno = new List<Modelo.Projeto>();
            NpgsqlCommand comando = new NpgsqlCommand("select * from projeto;", Conexao);

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var teste = new Projeto();

                teste.Id = leitor.GetInt32(0);
                teste.Nome = leitor.GetString(1);
                teste.Area = leitor.GetString(2);
                teste.Status = leitor.GetBoolean(3);

                retorno.Add(teste);
            }
            leitor.Close();

            foreach(var p in retorno)
            {
                p.Pessoas = BuscaProjetoPessoa(p.Id);
                p.Estabelecimento = BuscarProjetoEstabelecimento(p.Id);
            }

            return retorno;
        }

        private List<Estabelecimento> BuscarProjetoEstabelecimento(int id)
        {
            List<Modelo.Estabelecimento> estabelecimento = new List<Modelo.Estabelecimento>();

            var sql = "SELECT * FROM PROJETOESTABELECIMENTO WHERE PROJETOID = @ProjetoId";

            NpgsqlCommand comando = new NpgsqlCommand(sql, Conexao);

            comando.Parameters.Add(new NpgsqlParameter("ProjetoId", id));

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {

                estabelecimento.Add(Persistencia.EstabelecimentoPersistencia.Instancia.BuscEstabelecimentoPorId(leitor.GetInt32(1)));

            }
            leitor.Close();
            return estabelecimento;
        }

        private List<Pessoa> BuscaProjetoPessoa(int id)
        {
            List<Modelo.Pessoa> pessoa = new List<Modelo.Pessoa>();

            var sql = "SELECT * FROM ProjetoPessoa WHERE PROJETOID = @ProjetoId";

            NpgsqlCommand comando = new NpgsqlCommand(sql, Conexao);

            comando.Parameters.Add(new NpgsqlParameter("ProjetoId", id));

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {

                try
                {
                    pessoa.Add(Persistencia.PessoaConsulta.Instancia.BuscaPesquisadorPorId(leitor.GetInt32(2)));
                }
                catch
                {
                    pessoa.Add(Persistencia.PessoaConsulta.Instancia.BuscaAlunoPorId(leitor.GetInt32(1)));
                }
            }
            leitor.Close();
            return pessoa;
        }
    }
}
