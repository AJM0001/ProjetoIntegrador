

namespace MvpPesquisador.Negocio
{
    public class PessoaNegocio:NegocioBase<Negocio.PessoaNegocio>
    {
        public void SalvarPesquisador(Modelo.Pesquisador modelo) => Persistencia.PessoaConsulta.Instancia.Inserir(modelo);

        public List<Modelo.Pesquisador> BuscarTudoPesquisador() =>  Persistencia.PessoaConsulta.Instancia.BuscarTudoPesquisador();

        public void SalvarAluno(Modelo.Aluno modelo) => Persistencia.PessoaConsulta.Instancia.Inserir(modelo);

        public List<Modelo.Aluno> BuscarTudoAluno() => Persistencia.PessoaConsulta.Instancia.BuscarTudoAluno();
    }
}
