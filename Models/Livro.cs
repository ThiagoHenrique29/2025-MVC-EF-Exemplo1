using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVC_EF.Exemplo1.Models
{
    public class Livro
    {
        public int LivroID { get; set; }
        public string LivroTitulo { get; set; }
        public ushort LivroPaginas { get; set; }
        public string LivroISBN { get; set; }
        public ushort LivroAnoPublicacao { get; set; }
        public int EditoraID { get; set; }
        public int Estoque { get; set; }

        public Editora EditoraDoLivro { get; set; }
        public ICollection<OperacaoCompraVenda>? OperacoesDoLivro { get; set; }
        public ICollection<AutorLivro>? AutoresDoLivro { get; set; }
    }

    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(p => p.LivroID);  // Definindo a chave primária

            builder.Property(p => p.LivroTitulo)
                .HasMaxLength(120)
                .IsRequired();  // O título do livro é obrigatório

            builder.Property(p => p.LivroPaginas)
                .HasDefaultValue(0)
                .IsRequired();  // O número de páginas é obrigatório

            builder.Property(p => p.LivroISBN)
                .HasMaxLength(13)
                .IsRequired();  // O ISBN do livro é obrigatório

            // Relacionamento com a Editora
            builder.HasOne(p => p.EditoraDoLivro)
                .WithMany(p => p.LivrosDaEditora)  // A editora pode ter muitos livros
                .HasForeignKey(p => p.EditoraID)  // A chave estrangeira é EditoraID
                .IsRequired();  // Editora é obrigatória para o livro

            // Relacionamento com OperacaoCompraVenda
            builder.HasMany(p => p.OperacoesDoLivro)  // Um livro pode ter muitas operações
                .WithOne(p => p.LivroDaOperacao)  // Cada operação pertence a um livro
                .HasForeignKey(p => p.LivroID)  // A chave estrangeira é LivroID
                .OnDelete(DeleteBehavior.Cascade);  // Se o livro for deletado, as operações associadas também serão deletadas
        }
    }
}
