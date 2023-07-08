using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvpPesquisador.Modelo;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web.Mvc;

namespace MvpPesquisador.Controllers
{
    public class ProjetoController
    {
        public bool CriarProjeto(Modelo.Projeto Projeto)
        {
            if (ValidarDados(Projeto))
            {
                var projeto = new Modelo.Projeto();

                Random random = new Random();

                projeto.Id = random.Next();
                projeto.Nome = Projeto.Nome;
                projeto.Pessoas.AddRange(Projeto.Pessoas);
                projeto.Estabelecimento.AddRange(Projeto.Estabelecimento);
                projeto.Area = Projeto.Area;
                projeto.Status = true;

                LimparFormulario(Projeto);
                Salvar(projeto);

                return true;
            }

            return false;
        }

        public bool AtualizarProjeto(Modelo.Projeto Projeto) 
        {
            var projeto = new Modelo.Projeto();

            Random random = new Random();

            if (Projeto.Nome == null)
            {
                projeto = GetProjetos().FirstOrDefault();
                projeto.Status = Projeto.Status;
            }
            else
            {
                projeto.Id = Projeto.Id;
                projeto.Nome = Projeto.Nome;
                projeto.Pessoas.AddRange(Projeto.Pessoas);
                projeto.Estabelecimento.AddRange(Projeto.Estabelecimento);
                projeto.Area = Projeto.Area;
                projeto.Status = Projeto.Status;

            }

            Negocio.ProjetoNegocio.Instancia.AtualizarProjeto(projeto);
            //atualizar(projeto);

            return true;
        }

        public void ItemSelecionado(ChangeEventArgs e, Modelo.Projeto Projeto)
        {
            var selectedValues = e.Value.ToString();

            var projetos = GetProjetos();

            var projeto = projetos.FirstOrDefault(p => p.Id.ToString() == selectedValues);

            Projeto.Id = projeto.Id;
            Projeto.Nome = projeto.Nome;
            Projeto.Pessoas = projeto.Pessoas;
            Projeto.Estabelecimento= projeto.Estabelecimento;
            Projeto.Area = projeto.Area;
            Projeto.Status = projeto.Status;
        }

        public List<Projeto> GetProjetos()
        {
            // banco de dados

            return Negocio.ProjetoNegocio.Instancia.BuscarTudoProjeto().Where(x => x.Status.Equals(true)).ToList(); 

            /*var projetos = new List<Modelo.Projeto>()
            {
            new Projeto { Id = 1, Nome = "Projeto 1", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 1" }, new Pessoa { Nome = "Pessoa 2" } }, Area = "Area 1", Status = true },
            new Projeto { Id = 2, Nome = "Projeto 2", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 3" }, new Pessoa { Nome = "Pessoa 4" } }, Area = "Area 2", Status = false },
            new Projeto { Id = 3, Nome = "Projeto 3", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 5" }, new Pessoa { Nome = "Pessoa 6" } }, Area = "Area 3", Status = true },
            new Projeto { Id = 4, Nome = "Projeto 4", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 7" }, new Pessoa { Nome = "Pessoa 8" } }, Area = "Area 4", Status = false },
            new Projeto { Id = 5, Nome = "Projeto 5", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 9" }, new Pessoa { Nome = "Pessoa 10" } }, Area = "Area 5", Status = true }
            };

            return projetos.Where(x => x.Status.Equals(true)).ToList();*/
        }

        public void LimparFormulario(Modelo.Projeto Projeto)
        {
            Projeto.Nome = null;
            Projeto.Area = null;
            Projeto.Pessoas.Clear();
            Projeto.Estabelecimento.Clear();
            Projeto.Status = false;
        }

        public void ItemSelecionadoPessoa(ChangeEventArgs e, Modelo.Projeto Projeto)
        {
            var selectedValues = (IEnumerable<string>)e.Value;

            Projeto.Pessoas.Clear();

            var pessoas = GetPessoas();

            Projeto.Pessoas.AddRange(pessoas.Where(p => selectedValues.Contains(p.Id.ToString())).ToList());
        }

        public void ItemSelecionadoEstabelecimento(ChangeEventArgs e, Modelo.Projeto Projeto)
        {
            var selectedValues = (IEnumerable<string>)e.Value;

            Projeto.Estabelecimento.Clear();

            var estabelecimento = GetEstabelecimento();

            Projeto.Estabelecimento.AddRange(estabelecimento.Where(p => selectedValues.Contains(p.Id.ToString())).ToList());
        }

        public List<Pessoa> GetPessoas()
        {
            //pegar todas as pessoas do banco 

            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas.AddRange(Negocio.PessoaNegocio.Instancia.BuscarTudoPesquisador());
            pessoas.AddRange(Negocio.PessoaNegocio.Instancia.BuscarTudoAluno());

            var projetos = BuscarTudoProjeto();
            var pessoasMeio = new List<Pessoa>();

            foreach (var projeto in projetos)
                foreach (var pessoa in projeto.Pessoas)
                    if (!pessoasMeio.Any(x => x.Id == pessoa.Id))
                        pessoasMeio.Add(pessoa);


            var pessoaFinal = pessoas.Where(x => !pessoasMeio.Any(y => y.Id == x.Id));


            return pessoaFinal.ToList();
        }

        public List<Estabelecimento> GetEstabelecimento()
        {
            //pegar todas as pessoas do banco 

            return Negocio.EstabelecimentoNegocio.Instancia.BuscarTudoEstabelecimento();

            /*var estabelecimentos = new List<Estabelecimento>()
            {
            new Estabelecimento { Id = 1, Nome = "Estabelecimento 1", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 1" }, new Pessoa { Nome = "Pessoa 2" } }},
            new Estabelecimento { Id = 2, Nome = "Estabelecimento 2", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 3" }, new Pessoa { Nome = "Pessoa 4" } }},
            new Estabelecimento { Id = 3, Nome = "Estabelecimento 3", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 5" }, new Pessoa { Nome = "Pessoa 6" } }}*/
            // Adicione mais estabelecimentos conforme necessário
            //};

            //return estabelecimentos;
        }

        public void Salvar(Modelo.Projeto projeto)
        {
            Negocio.ProjetoNegocio.Instancia.SalvarProjeto(projeto);

        }

        public List<Modelo.Projeto> BuscarTudoProjeto()
        {
            var projetos = Negocio.ProjetoNegocio.Instancia.BuscarTudoProjeto();

            /*var projetos = new List<Modelo.Projeto>()
            {       
            new Projeto { Id = 1, Nome = "Projeto 1", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 1" }, new Pessoa { Nome = "Pessoa 2" } }, Area = "Area 1", Status = true },
            new Projeto { Id = 2, Nome = "Projeto 2", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 3" }, new Pessoa { Nome = "Pessoa 4" } }, Area = "Area 2", Status = false },
            new Projeto { Id = 3, Nome = "Projeto 3", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 5" }, new Pessoa { Nome = "Pessoa 6" } }, Area = "Area 3", Status = true },
            new Projeto { Id = 4, Nome = "Projeto 4", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 7" }, new Pessoa { Nome = "Pessoa 8" } }, Area = "Area 4", Status = false },
            new Projeto { Id = 5, Nome = "Projeto 5", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 9" }, new Pessoa { Nome = "Pessoa 10" } }, Area = "Area 5", Status = true }
            };*/

            return projetos;

        }
        public void RecarregarPaginaProjeto() => BuscarTudoProjeto();
        public bool ValidacaoNome(Modelo.Projeto Projeto) => string.IsNullOrWhiteSpace(Projeto?.Nome) || Projeto?.Nome?.Length > 50 ? false : true;
        public bool ValidacaoArea(Modelo.Projeto Projeto) => string.IsNullOrWhiteSpace(Projeto?.Area) || Projeto?.Area?.Length > 30 ? false : true;
        public bool ValidacaoPessoas(Modelo.Projeto Projeto) => Projeto?.Pessoas?.Count() == 0 ? false : true;
        public bool ValidacaoEstabelecimentos(Modelo.Projeto Projeto) => Projeto?.Estabelecimento?.Count() == 0 ? false : true;

        public bool ValidarDados(Modelo.Projeto Projeto)
        {
            if (Projeto.Nome == null || Projeto.Area == null || Projeto.Pessoas.Count() == 0 /*|| Projeto.Estabelecimento.Count() == 0*/)
                return false;

            if (ValidacaoNome(Projeto) && ValidacaoArea(Projeto) && ValidacaoPessoas(Projeto) /*&& ValidacaoEstabelecimentos(Projeto)*/)
                return true;

            return false;
        }
    }
}
