using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoDaOuvidoria.Models
{
    public class Manifesto
    {
        [Key]
        public int Protocolo { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail deve ser preenchido!")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "E-mail Inválido. Exemplo: nome@gmail.com")]
        public string Email { get; set; }
        [Display(Name = "Telefone")]
        [RegularExpression(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Telefone inválido. Exemplo: 2433400000")]
        public string Telefone { get; set; }
        [Display(Name = "Celular")]
        [RegularExpression(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Celular inválido. Exemplo: 2499998888")]
        public string Celular { get; set; }
        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "Informe perfil do utilizador")]
        public string Perfil { get; set; }
        [Display(Name = "Campus")]
        [Required(ErrorMessage = "Informe o campus")]
        public string Campus { get; set; }
        [Display(Name = "Curso")]
        [Required(ErrorMessage = "Informe o curso")]
        public string Curso { get; set; }
        [Display(Name = "Solicitação")]
        [Required(ErrorMessage = "Informe o tipo de solicitação")]
        public string TipoSolicitacao { get; set; }
        [Display(Name = "Setor")]
        [Required(ErrorMessage = "Informe o setor")]
        public string Setor { get; set; }
        [Display(Name = "Assunto")]
        [Required(ErrorMessage = "Informe o assunto")]
        public string Assunto { get; set; }
        [Display(Name = "Manifestação")]
        [Required(ErrorMessage = "Por favor insira seu manifesto")]
        public string Manifestacao { get; set; }
        [Required(ErrorMessage = "Data Inválida")]
        public DateTime? DataCriacao { get; set; }

        public string RespostaOuvidoria { get; set; }

    }
}