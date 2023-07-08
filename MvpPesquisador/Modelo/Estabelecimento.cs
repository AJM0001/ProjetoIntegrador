using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvpPesquisador.Modelo
{
    public class Estabelecimento
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O campo nome deve ter no máximo 255 caracteres.")]
        public string Nome { get; set; }

        [MinListLength(1, ErrorMessage = "O Estabelecimento deve ter, no mínimo, uma Pessoa cadastrada.")]
        public List<Pessoa> Pessoas { get; set; } = new List<Pessoa>();
    }

    public class MinListLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public MinListLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count >= _minLength;
            }
            return false;
        }
    }
}
