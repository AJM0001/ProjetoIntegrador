using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Web.Mvc;

namespace MvpPesquisador.Controllers
{
    public class PessoaController
    {
        public void CriarPessoa(Modelo.Pesquisador Pessoa) 
        {
            if(Pessoa is Modelo.Pesquisador Pesquisador && ValidarDados(Pesquisador))
            {
                var pesquisador = new Modelo.Pesquisador();

                Random random = new Random();

                pesquisador.Id = random.Next();
                pesquisador.Nome = Pesquisador.Nome;
                pesquisador.CodigoInstituicao = Pesquisador.CodigoInstituicao;
                pesquisador.Formacao = Pesquisador.Formacao;
                pesquisador.Lattes = Pesquisador.Lattes;

                LimparFormulario(Pesquisador);
                Salvar(pesquisador);
            }
        }

        public void CriarPessoa(Modelo.Aluno Pessoa)
        {
            if (Pessoa is Modelo.Aluno Aluno && ValidarDados(Aluno))
            {
                var aluno = new Modelo.Aluno();

                Random random = new Random();

                aluno.Id = random.Next();
                aluno.Nome = Aluno.Nome;
                aluno.CodigoInstituicao = Aluno.CodigoInstituicao;
                aluno.Curso = Aluno.Curso;

                LimparFormulario(Aluno);
                Salvar(aluno);
            }
        }

        public void LimparFormulario(Modelo.Pesquisador Pesquisador) 
        {
            Pesquisador.Nome = null;
            Pesquisador.CodigoInstituicao = 0;
            Pesquisador.Formacao = null;
            Pesquisador.Lattes = null;
        }

        public void LimparFormulario(Modelo.Aluno Aluno)
        {
            Aluno.Nome = null;
            Aluno.CodigoInstituicao = 0;
            Aluno.Curso = null;
        }

        public void Salvar(Modelo.Pesquisador pesquisador) => Negocio.PessoaNegocio.Instancia.SalvarPesquisador(pesquisador);
        public void Salvar(Modelo.Aluno aluno) => Negocio.PessoaNegocio.Instancia.SalvarAluno(aluno);
        public List<Modelo.Pesquisador> BuscarTudoPesquisador() => Negocio.PessoaNegocio.Instancia.BuscarTudoPesquisador();
        public void RecarregarPaginaPesquisador() => BuscarTudoPesquisador();
        public List<Modelo.Aluno> BuscarTudoAluno() => Negocio.PessoaNegocio.Instancia.BuscarTudoAluno();
        public void RecarregarPaginaAluno() => BuscarTudoAluno();
        public bool ValidacaoNome(Modelo.Pessoa Pessoa) => string.IsNullOrWhiteSpace(Pessoa?.Nome) || Pessoa?.Nome?.Length > 50 ? false : true;
        public bool ValidacaoFormacao(Modelo.Pesquisador Pesquisador) => string.IsNullOrWhiteSpace(Pesquisador?.Formacao) || Pesquisador?.Formacao?.Length > 30 ? false : true;
        public bool ValidacaoLattes(Modelo.Pesquisador Pesquisador) => string.IsNullOrWhiteSpace(Pesquisador?.Lattes) || Pesquisador?.Lattes?.Length > 100 ? false : true;
        public bool ValidacaoCurso(Modelo.Aluno Aluno) => string.IsNullOrWhiteSpace(Aluno?.Curso) || Aluno?.Curso?.Length > 50 ? false : true;

        public bool ValidarDados(Modelo.Pesquisador Pesquisador) 
        {
            if (Pesquisador.Nome == null || Pesquisador.Formacao == null || Pesquisador.Lattes == null)
                return false;

            if (ValidacaoNome(Pesquisador) && ValidacaoFormacao(Pesquisador) && ValidacaoLattes(Pesquisador))
                return true;

            return false;
        }

        public bool ValidarDados(Modelo.Aluno Aluno)
        {
            if (Aluno.Nome == null || Aluno.Curso == null)
                return false;

            if (ValidacaoNome(Aluno) && ValidacaoCurso(Aluno) )
                return true;

            return false;
        }
    }
}
