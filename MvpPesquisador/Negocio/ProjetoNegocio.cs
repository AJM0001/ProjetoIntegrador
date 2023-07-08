namespace MvpPesquisador.Negocio
{
    public class ProjetoNegocio : NegocioBase<Negocio.ProjetoNegocio>
    {
        public void SalvarProjeto(Modelo.Projeto projeto) => Persistencia.ProjetoPersistencia.Instancia.Inserir(projeto);
        public List<Modelo.Projeto> BuscarTudoProjeto() => Persistencia.ProjetoPersistencia.Instancia.BuscarTudoProjeto();
        public void AtualizarProjeto(Modelo.Projeto projeto) => Persistencia.ProjetoPersistencia.Instancia.Atualizar(projeto);
    }
}
