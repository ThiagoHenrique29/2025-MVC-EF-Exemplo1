using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_EF.Exemplo1.Models
{
    public class EditoraListViewModel
    {
        // Campo para exibir o nome da editora, com validação de tamanho
        [Required(ErrorMessage = "O nome da editora é obrigatório.")]
        [MaxLength(80, ErrorMessage = "O nome da editora não pode ter mais que 80 caracteres.")]
        public string EditoraNome { get; set; }

        // Cidade da editora com validação de comprimento máximo
        [MaxLength(60, ErrorMessage = "A cidade não pode ter mais que 60 caracteres.")]
        public string? EditoraCidade { get; set; }

        // Estado da editora (UF) com validação de 2 caracteres
        [MaxLength(2, ErrorMessage = "O estado (UF) deve ter 2 caracteres.")]
        public string? EditoraUF { get; set; }

        // Contagem de livros associados à editora (campo somente leitura, sem validação)
        public int NumeroDeLivros { get; set; }

        // Se necessário um campo SelectList para associar a um dropdown de editoras
        public SelectList EditorasSelectList { get; set; }

        // Campo para armazenar a seleção de uma editora (útil em formulários)
        [Required(ErrorMessage = "Selecione uma editora.")]
        public int? EditoraID { get; set; }
    }
}