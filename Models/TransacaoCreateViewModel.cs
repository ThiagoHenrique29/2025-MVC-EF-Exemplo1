using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_EF.Exemplo1.Models
{
    public class TransacaoCreateViewModel
    {
        // Lista de livros para o MultiSelectList
        public MultiSelectList LivrosDisponiveis { get; set; }

        // Lista de IDs dos livros selecionados
        public List<int> LivrosSelecionados { get; set; } = new List<int>();
        
        // Propriedade para o ID do livro selecionado
        [Required(ErrorMessage = "Selecione um livro.")]
        public int? LivroID { get; set; }

        // Propriedade para a data da operação
        [Required(ErrorMessage = "A data da operação é obrigatória.")]
        public DateOnly OperacaoData { get; set; }

        // Propriedade para a quantidade da operação
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        public short OperacaoQuantidade { get; set; }

        // Lista de livros para o dropdown (select)
        public SelectList LivroSelectList { get; set; }
    }
}