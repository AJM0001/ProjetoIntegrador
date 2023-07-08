namespace MvpPesquisador.Negocio
{
    public class EstabelecimentoNegocio : NegocioBase<Negocio.EstabelecimentoNegocio>
    {
        public void SalvarEstabelecimento(Modelo.Estabelecimento estabelecimento) => Persistencia.EstabelecimentoPersistencia.Instancia.Inserir(estabelecimento);
        public List<Modelo.Estabelecimento> BuscarTudoEstabelecimento() => Persistencia.EstabelecimentoPersistencia.Instancia.BuscarTudoEstabelecimento();
    }
}
