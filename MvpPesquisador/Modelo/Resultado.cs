using System.ComponentModel.DataAnnotations;

namespace MvpPesquisador.Modelo
{
    public class Resultado
    {
        [Required]
        public int Id { get; set; }

        public Projeto Projeto { get; set; }

        [Required(ErrorMessage = "O campo Informações é obrigatório.")]
        [StringLength(1000, ErrorMessage = "O campo Informações deve ter no máximo 1000 caracteres.")]
        public string Informacoes { get; set; }
    }
}
