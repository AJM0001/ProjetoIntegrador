using System.ComponentModel.DataAnnotations;

namespace MvpPesquisador.Modelo
{
    public class Pesquisador : Pessoa
    {
        [Required]
        [StringLength(30, ErrorMessage = "O campo formação deve ser informado")]
        public string Formacao { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O campo Lattes deve ser informado")]
        public string Lattes { get; set; }

    }
}
