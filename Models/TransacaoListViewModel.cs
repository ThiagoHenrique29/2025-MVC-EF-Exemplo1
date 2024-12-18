namespace MVC_EF.Exemplo1.Models
{
    public class TransacaoListViewModel
    {
        public int OperacaoID { get; set; }
        public string LivroNome { get; set; }
        public DateOnly OperacaoData { get; set; }
        public short OperacaoQuantidade { get; set; }
    }
}