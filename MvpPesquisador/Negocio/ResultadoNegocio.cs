namespace MvpPesquisador.Negocio
{
    public class ResultadoNegocio : NegocioBase<ResultadoNegocio>
    {
        public void SalvarResultado(Modelo.Resultado resultado) => Persistencia.ResultadoPersistencia.Instancia.Inserir(resultado);
        public List<Modelo.Resultado> BuscarTudoResultado() => Persistencia.ResultadoPersistencia.Instancia.BuscarTudoResultado();
    }
}
