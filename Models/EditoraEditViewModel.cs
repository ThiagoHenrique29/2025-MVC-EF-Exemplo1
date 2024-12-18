using System.ComponentModel.DataAnnotations;

namespace MVC_EF.Exemplo1.Models
{
    public class EditoraEditViewModel
    {
        public int EditoraID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome da editora é obrigatório.")]
        [StringLength(80, ErrorMessage = "O nome deve ter até 80 caracteres.")]
        public string EditoraNome { get; set; }

        [Display(Name = "Logradouro")]
        [StringLength(80, ErrorMessage = "O logradouro deve ter até 80 caracteres.")]
        public string? EditoraLogradouro { get; set; }

        [Display(Name = "Número")]
        public ushort? EditoraNumero { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(80, ErrorMessage = "O complemento deve ter até 80 caracteres.")]
        public string? EditoraComplemento { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(60, ErrorMessage = "A cidade deve ter até 60 caracteres.")]
        public string? EditoraCidade { get; set; }

        [Display(Name = "UF")]
        [StringLength(2, ErrorMessage = "A UF deve ter 2 caracteres.")]
        public string? EditoraUF { get; set; }

        [Display(Name = "País")]
        [StringLength(40, ErrorMessage = "O país deve ter até 40 caracteres.")]
        public string? EditoraPais { get; set; }

        [Display(Name = "CEP")]
        [StringLength(12, ErrorMessage = "O CEP deve ter até 12 caracteres.")]
        public string? EditoraCEP { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(15, ErrorMessage = "O telefone deve ter até 15 caracteres.")]
        public string? EditoraTelefone { get; set; }
    }
}
