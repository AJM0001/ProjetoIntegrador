using System.ComponentModel.DataAnnotations;

namespace MvpPesquisador.Modelo
{
    public class Aluno : Pessoa
    {
        [Required]
        [StringLength(100, ErrorMessage = "O campo Curso deve ser informado")]
        public string Curso { get; set; }      
    }
}
