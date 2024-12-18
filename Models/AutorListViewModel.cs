using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_EF.Exemplo1.Models
{
    public class AutorListViewModel
    {
        [Required(ErrorMessage = "O nome do autor é obrigatório.")]
        [MaxLength(80, ErrorMessage = "O nome do autor não pode ter mais que 80 caracteres.")]
        public string AutorNome { get; set; }

        [MaxLength(80, ErrorMessage = "O email do autor não pode ter mais que 80 caracteres.")]
        public string? AutorEmail { get; set; }

        public DateOnly? AutorDataNascimento { get; set; }

        public int NumeroDeLivros { get; set; } // Caso deseje incluir a contagem de livros

        // Campo SelectList para associar a um dropdown de autores (se necessário)
        public SelectList AutorSelectList { get; set; }

        [Required(ErrorMessage = "Selecione um autor.")]
        public int? AutorID { get; set; }
    }
}