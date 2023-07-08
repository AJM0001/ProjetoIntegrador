using MvpPesquisador.Modelo;
using Npgsql;
using System.Security.Cryptography;
using System.Text;

namespace MvpPesquisador.Persistencia
{
    public class EstabelecimentoPersistencia : PersistenciaBase<EstabelecimentoPersistencia>
    {
        private string CamposTabelaEstabelecimento = "(Id, Nome)";
        public void Inserir(Modelo.Estabelecimento estabelecimento)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("INSERT INTO ESTABELECIMENTO {0}" +
                " VALUES ( @Id,@Nome);", CamposTabelaEstabelecimento));

            NpgsqlCommand comando = new NpgsqlCommand(sb.ToString(), Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", estabelecimento.Id));
            comando.Parameters.Add(new NpgsqlParameter("Nome", estabelecimento.Nome));

            comando.ExecuteNonQuery();

            InserirPessoaEstabelecimento(estabelecimento);

            

            
        }

        public void InserirPessoaEstabelecimento(Modelo.Estabelecimento estabelecimento)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("INSERT INTO PessoaEstabelecimento (Id, PesquisadorId, AlunoId, EstabelecimentoId) " +
                "VALUES (@Id, @PesquisadorId, @AlunoId, @EstabelecimentoId);"));

            foreach(Pessoa p in estabelecimento.Pessoas)
            {
                NpgsqlCommand comando = null;

                if (p is Modelo.Aluno)
                {
                    var sql = "INSERT INTO PessoaEstabelecimento (Id, AlunoId, EstabelecimentoId) " +
                "VALUES (@Id, @AlunoId, @EstabelecimentoId);";
                    comando = new NpgsqlCommand(sql, Conexao);

                    comando.Parameters.Add(new NpgsqlParameter("Id", random.Next()));
                    comando.Parameters.Add(new NpgsqlParameter("AlunoId", p.Id));
                    comando.Parameters.Add(new NpgsqlParameter("EstabelecimentoId", estabelecimento.Id));
                }
                else if (p is Modelo.Pesquisador)
                {
                    var sql = "INSERT INTO PessoaEstabelecimento (Id, PesquisadorId, EstabelecimentoId) " +
                "VALUES (@Id, @PesquisadorId, @EstabelecimentoId);";
                    comando = new NpgsqlCommand(sql, Conexao);

                    comando.Parameters.Add(new NpgsqlParameter("Id", random.Next()));
                    comando.Parameters.Add(new NpgsqlParameter("PesquisadorId", p.Id));
                    comando.Parameters.Add(new NpgsqlParameter("EstabelecimentoId", estabelecimento.Id));
                }

                comando.ExecuteNonQuery();
            }
        }

        public List<Modelo.Estabelecimento> BuscarTudoEstabelecimento()
        {
            List<Modelo.Estabelecimento> retorno = new List<Modelo.Estabelecimento>();
            NpgsqlCommand comando = new NpgsqlCommand("select * from estabelecimento;", Conexao);

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var teste = new Estabelecimento();

                teste.Id = leitor.GetInt32(0);
                teste.Nome = leitor.GetString(1);

                retorno.Add(teste);
            }
            leitor.Close();

            foreach(var e in retorno )
            {
                
                e.Pessoas = BuscaEstabelecimentoPessoas(e.Id);
            }

            return retorno;
        }

        private List<Modelo.Pessoa> BuscaEstabelecimentoPessoas(int id)
        {
            List<Modelo.Pessoa> pessoa = new List<Modelo.Pessoa>();

            var sql = "SELECT * FROM pessoaestabelecimento WHERE ESTABELECIMENTOID = @EstabelecimentoId";
            
            NpgsqlCommand comando = new NpgsqlCommand(sql, Conexao);

            comando.Parameters.Add(new NpgsqlParameter("EstabelecimentoId", id));

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {

                try
                {
                    pessoa.Add(Persistencia.PessoaConsulta.Instancia.BuscaPesquisadorPorId(leitor.GetInt32(1)));
                }
                catch
                {
                    pessoa.Add(Persistencia.PessoaConsulta.Instancia.BuscaAlunoPorId(leitor.GetInt32(2)));
                }
    
            }
            leitor.Close();
            return pessoa;
        }

        public Modelo.Estabelecimento BuscEstabelecimentoPorId(int id)
        {
            Modelo.Estabelecimento estabelecimento = new Modelo.Estabelecimento();
            NpgsqlCommand comando = new NpgsqlCommand("select * from estabelecimento where id = @Id;", Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", id));

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                estabelecimento.Id = leitor.GetInt32(0);
                estabelecimento.Nome = leitor.GetString(1);
            }
            leitor.Close();

            return estabelecimento;
        }
    }
}
