using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVC_EF.Exemplo1.Models
{
    public class Editora
    {
        public int EditoraID { get; set; }
        public string EditoraNome { get; set; }
        public string? EditoraLogradouro { get; set; }
        public ushort? EditoraNumero { get; set; }
        public string? EditoraComplemento { get; set; }
        public string? EditoraCidade { get; set; }
        public string? EditoraUF { get; set; }
        public string? EditoraPais { get; set; }
        public string? EditoraCEP { get; set; }
        public string? EditoraTelefone { get; set; }

        public ICollection<Livro>? LivrosDaEditora { get; set; }
    }

    public class EditoraConfiguration : IEntityTypeConfiguration<Editora>
    {
        public void Configure(EntityTypeBuilder<Editora> builder)
        {
            // Define EditoraID como chave primária
            builder.HasKey(p => p.EditoraID);

            // Especifica que o EditoraID será gerado automaticamente
            builder.Property(p => p.EditoraID)
                   .ValueGeneratedOnAdd(); // Isso faz o EF gerar o valor automaticamente quando a entidade for adicionada

            // Cria índice único para EditoraNome
            builder.HasIndex(p => p.EditoraNome);

            // Define as propriedades da Editora e suas limitações
            builder.Property(p => p.EditoraNome)
                   .HasMaxLength(80)
                   .IsRequired();  // EditoraNome é obrigatório

            builder.Property(p => p.EditoraLogradouro)
                   .HasMaxLength(80);

            builder.Property(p => p.EditoraComplemento)
                   .HasMaxLength(80);

            builder.Property(p => p.EditoraCidade)
                   .HasMaxLength(60);

            builder.Property(p => p.EditoraUF)
                   .HasMaxLength(2);

            builder.Property(p => p.EditoraPais)
                   .HasMaxLength(40);

            builder.Property(p => p.EditoraCEP)
                   .HasMaxLength(12);

            builder.Property(p => p.EditoraTelefone)
                   .HasMaxLength(15);

            // Configura relacionamento com a entidade Livro
            builder.HasMany<Livro>(p => p.LivrosDaEditora)
                   .WithOne(p => p.EditoraDoLivro);
        }
    }
}
