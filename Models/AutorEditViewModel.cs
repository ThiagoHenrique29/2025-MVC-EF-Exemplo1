using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_EF.Exemplo1.Models
{
    public class AutorEditViewModel
    {
        public int AutorID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome do autor é obrigatório.")]
        [StringLength(80, ErrorMessage = "O nome deve ter até 80 caracteres.")]
        public string AutorNome { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateOnly? AutorDataNascimento { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string? AutorEmail { get; set; }
    }
}