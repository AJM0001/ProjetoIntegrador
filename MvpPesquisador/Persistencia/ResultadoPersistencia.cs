using MvpPesquisador.Modelo;
using Npgsql;
using System.Text;

namespace MvpPesquisador.Persistencia
{
    public class ResultadoPersistencia : PersistenciaBase<ResultadoPersistencia>
    {
        private string CamposTabelaResultado = "(Id,Informacoes, ProjetoId)";
        public void Inserir(Modelo.Resultado resultado)
        {
            StringBuilder sb = new StringBuilder();
        
            sb.AppendLine(string.Format("INSERT INTO RESULTADO {0}" +
                " VALUES ( @Id,@Informacoes, @ProjetoId);", CamposTabelaResultado));

            NpgsqlCommand comando = new NpgsqlCommand(sb.ToString(), Conexao);

            comando.Parameters.Add(new NpgsqlParameter("Id", resultado.Id));
            comando.Parameters.Add(new NpgsqlParameter("Informacoes", resultado.Informacoes));
            comando.Parameters.Add(new NpgsqlParameter("ProjetoId", resultado.Projeto.Id));

            comando.ExecuteNonQuery();
        }

        public List<Modelo.Resultado> BuscarTudoResultado()
        {
            List<Modelo.Resultado> retorno = new List<Modelo.Resultado>();
            NpgsqlCommand comando = new NpgsqlCommand("select * from projeto p, resultado r where p.id = r.ProjetoId;", Conexao);

            NpgsqlDataReader leitor = comando.ExecuteReader();

            while (leitor.Read())
            {
                var teste = new Resultado();
                teste.Projeto = new Projeto();

                teste.Projeto.Id = leitor.GetInt32(0);
                teste.Projeto.Nome= leitor.GetString(1);
                teste.Projeto.Area = leitor.GetString(2);
                teste.Projeto.Status = leitor.GetBoolean(3);
                teste.Id = leitor.GetInt32(4);
                teste.Informacoes = leitor.GetString(5);

                retorno.Add(teste);
            }
            leitor.Close();

            return retorno;
        }
    }
}
