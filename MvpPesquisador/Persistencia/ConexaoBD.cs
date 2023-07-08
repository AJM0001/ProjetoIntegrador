using Npgsql;

namespace MvpPesquisador.Persistencia
{
    public class ConexaoBD
    {
        private NpgsqlConnection _conexao;

        public NpgsqlConnection Conexao
        {
            get
            {
                if (_conexao == null)
                {
                    _conexao = new NpgsqlConnection("Server=localhost;Port=5432;Database=trabalho;User ID=postgres;Password=ucs");
                    _conexao.Open();
                }

                return _conexao;
            }
        }
    }
}
