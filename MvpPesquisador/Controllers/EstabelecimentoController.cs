using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvpPesquisador.Modelo;
using System.Linq;
using System.Web.Mvc;

namespace MvpPesquisador.Controllers
{
    public class EstabelecimentoController
    {
        public void CriarEstabelecimento(Modelo.Estabelecimento Estabelecimento)
        {
            if (ValidarDados(Estabelecimento))
            {
                var estabelecimento = new Modelo.Estabelecimento();

                Random random = new Random();

                estabelecimento.Id = random.Next();
                estabelecimento.Nome = Estabelecimento.Nome;
                estabelecimento.Pessoas = Estabelecimento.Pessoas;

                LimparFormulario(Estabelecimento);
                Salvar(estabelecimento);
            }
        }

        public void LimparFormulario(Modelo.Estabelecimento Estabelecimento)
        {
            Estabelecimento.Nome = null;
            //Estabelecimento.Pessoas.Clear();
        }

        public void ItemSelecionado(ChangeEventArgs e, Modelo.Estabelecimento Estabelecimento)
        {
            var selectedValues = (IEnumerable<string>)e.Value;

            Estabelecimento.Pessoas.Clear();

            var pessoas = GetPessoas();

            Estabelecimento.Pessoas.AddRange(pessoas.Where(p => selectedValues.Contains(p.Id.ToString())).ToList());
        }

        public List<Pessoa> GetPessoas()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas.AddRange(BuscarTudoPesquisador());
            pessoas.AddRange(BuscarTudoAluno());
          
            var estabelecimentos = Negocio.EstabelecimentoNegocio.Instancia.BuscarTudoEstabelecimento();
            var pessoasMeio = new List<Pessoa>();

            foreach (var estabelecimento in estabelecimentos)
                foreach (var pessoa in estabelecimento.Pessoas)
                    if (!pessoasMeio.Any(x => x.Id == pessoa.Id))
                        pessoasMeio.Add(pessoa);


            var pessoaFinal = pessoas.Where(x => !pessoasMeio.Any(y => y.Id == x.Id));

            return pessoaFinal.ToList();
        }


        public void Salvar(Modelo.Estabelecimento estabelecimento)
        {
            Negocio.EstabelecimentoNegocio.Instancia.SalvarEstabelecimento(estabelecimento);

        }

        public List<Modelo.Estabelecimento> BuscarTudoEstabelecimento()
        {

            var estabelecimentos = Negocio.EstabelecimentoNegocio.Instancia.BuscarTudoEstabelecimento();
            /*int i = 1;
            foreach(var estabelecimento in estabelecimentos)
            {
                estabelecimento.Pessoas = new List<Pessoa> { new Pessoa { Nome = $"Pessoa {i++}" }, new Pessoa { Nome = $"Pessoa {i++}" } };
            }*/

            /*var estabelecimentos = new List<Estabelecimento>()
            {
            new Estabelecimento { Id = 1, Nome = "Estabelecimento 1", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 1" }, new Pessoa { Nome = "Pessoa 2" } }},
            new Estabelecimento { Id = 2, Nome = "Estabelecimento 2", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 3" }, new Pessoa { Nome = "Pessoa 4" } }},
            new Estabelecimento { Id = 3, Nome = "Estabelecimento 3", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 5" }, new Pessoa { Nome = "Pessoa 6" } }}
            // Adicione mais estabelecimentos conforme necessário
            };*/

            return estabelecimentos;

        }

        public void RecarregarPaginaEstabelecimento() => BuscarTudoEstabelecimento();
        public bool ValidacaoNome(Modelo.Estabelecimento Estabelecimento) => string.IsNullOrWhiteSpace(Estabelecimento?.Nome) || Estabelecimento?.Nome?.Length > 50 ? false : true;
        public bool ValidacaoProjeto(Modelo.Estabelecimento Estabelecimento) => Estabelecimento?.Pessoas.Count() == 0 ? false : true;
        public List<Modelo.Pesquisador> BuscarTudoPesquisador() => Negocio.PessoaNegocio.Instancia.BuscarTudoPesquisador();
        public List<Modelo.Aluno> BuscarTudoAluno() => Negocio.PessoaNegocio.Instancia.BuscarTudoAluno();
        public bool ValidarDados(Modelo.Estabelecimento Estabelecimento) 
        {
            if (Estabelecimento.Nome == null || Estabelecimento.Pessoas.Count() == 0)
                return false;

            if (ValidacaoNome(Estabelecimento) && ValidacaoProjeto(Estabelecimento))
                return true;

            return false;
        }
    }
}
