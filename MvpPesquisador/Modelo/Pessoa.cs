using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MvpPesquisador.Modelo
{
    public class Pessoa : IComparer
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O campo nome deve ser informado.")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo tempo deve ser de 1 a 1000.")]
        [Range(1, 1000)]
        public int CodigoInstituicao { get; set; }

        public int Compare(object? x, object? y)
        {
            var pessoas1 = x as Pessoa;
            var pessoas2 = y as Pessoa;

            if (pessoas1.Id == pessoas2.Id)
                return 1;

            return 0;
        }
    }
}
