using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvpPesquisador.Modelo
{
    public class Projeto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [MinListLength(1, ErrorMessage = "O Projeto deve ter, no mínimo, uma Pessoa cadastrada.")]
        public List<Pessoa> Pessoas { get; set; } = new List<Pessoa>();

        [MinListLength(1, ErrorMessage = "O Projeto deve ter, no mínimo, um Estabelecimento cadastrado.")]
        public List<Estabelecimento> Estabelecimento { get; set; } = new List<Estabelecimento>();

        [Required(ErrorMessage = "O campo área é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo área deve ter no máximo 100 caracteres.")]
        public string Area { get; set; }

        public bool Status { get; set; } = true;
    }
}
