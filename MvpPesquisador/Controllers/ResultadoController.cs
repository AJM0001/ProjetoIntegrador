using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvpPesquisador.Modelo;
using System.Linq;
using System.Web.Mvc;

namespace MvpPesquisador.Controllers
{
    public class ResultadoController
    {
        public bool CriarResultado(Modelo.Resultado Resultado)
        {
            if (ValidarDados(Resultado))
            {
                var resultado = new Modelo.Resultado();

                Random random = new Random();

                resultado.Id = random.Next();
                resultado.Informacoes = Resultado.Informacoes;
                resultado.Projeto = Resultado.Projeto;

                LimparFormulario(Resultado);
                Salvar(resultado);

                return true;
            }

            return false;
        }

        public void LimparFormulario(Modelo.Resultado Resultado)
        {
            Resultado.Projeto = null;
            Resultado.Informacoes = null;
        }

        public void ItemSelecionado(ChangeEventArgs e, Modelo.Resultado Resultado)
        {
            var selectedValues = e.Value.ToString();

            var projeto = GetProjetos();

            Resultado.Projeto = projeto.FirstOrDefault(p => p.Id.ToString() == selectedValues);
        }

        public List<Projeto> GetProjetos()
        {
            //pegar todas as pessoas do banco 

            return Negocio.ProjetoNegocio.Instancia.BuscarTudoProjeto().Where(x => x.Status.Equals(true)).ToList();

            /*var projetos = new List<Modelo.Projeto>()
            {
            new Projeto { Id = 1, Nome = "Projeto 1", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 1" }, new Pessoa { Nome = "Pessoa 2" } }, Area = "Area 1", Status = true },
            new Projeto { Id = 2, Nome = "Projeto 2", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 3" }, new Pessoa { Nome = "Pessoa 4" } }, Area = "Area 2", Status = false },
            new Projeto { Id = 3, Nome = "Projeto 3", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 5" }, new Pessoa { Nome = "Pessoa 6" } }, Area = "Area 3", Status = true },
            new Projeto { Id = 4, Nome = "Projeto 4", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 7" }, new Pessoa { Nome = "Pessoa 8" } }, Area = "Area 4", Status = false },
            new Projeto { Id = 5, Nome = "Projeto 5", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 9" }, new Pessoa { Nome = "Pessoa 10" } }, Area = "Area 5", Status = true }
            };

            return projetos.Where(x => x.Status.Equals(false)).ToList();*/
        }

        public void Salvar(Modelo.Resultado resultado)
        {
            Negocio.ResultadoNegocio.Instancia.SalvarResultado(resultado);

        }

        public List<Modelo.Resultado> BuscarTudoResultado()
        {
            var resultados = Negocio.ResultadoNegocio.Instancia.BuscarTudoResultado();

            /*var projetos = new Projeto[]
            {
            new Projeto { Id = 1, Nome = "Projeto 1", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 1" } }, Area = "Area 1", Status = true },
            new Projeto { Id = 2, Nome = "Projeto 2", Pessoas = new List<Pessoa> { new Pessoa { Nome = "Pessoa 2" } }, Area = "Area 2", Status = false }
            };

            var resultados = new List<Resultado>()
            {
            new Resultado { Id = 1, Projeto = projetos[0], Informacoes = "Resultado 1" },
            new Resultado { Id = 2, Projeto = projetos[1], Informacoes = "Resultado 2" }
            };*/

            return resultados;

        }
        public void RecarregarPaginaResultado() => BuscarTudoResultado();
        public bool ValidacaoInformacoes(Modelo.Resultado Resultado) => string.IsNullOrWhiteSpace(Resultado?.Informacoes) || Resultado?.Informacoes?.Length > 500 ? false : true;
        public bool ValidacaoProjeto(Modelo.Resultado Resultado) => Resultado?.Projeto == null ? false : true;
        public bool SalvoComSucesso { get; set; }

        public bool ValidarDados(Modelo.Resultado Resultado) 
        {
            if (Resultado.Projeto == null)
                Resultado.Projeto = GetProjetos().FirstOrDefault();

            if (Resultado.Informacoes == null)
                return false;

            if (ValidacaoInformacoes(Resultado) && ValidacaoProjeto(Resultado)) 
                return true;

            return false;
        }
    }
}
