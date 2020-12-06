namespace ContatosVirtual.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int? FornecedorId { get; set; }
        public string Numero { get; set; }  
    }
}