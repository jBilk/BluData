using System;

namespace ContatosVirtual.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public Empresa Empresa { get; set; }
        public int? EmpresaId { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string Rg { get; set; }
        public string PessoaJuridicaFisica { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string DataNascimentoBR { get { return DataNascimento?.ToString("dd/MM/yyyy"); } }
        public DateTime DataHoraCadastro { get; set; }
        public string DataCadastroBR { get { return DataHoraCadastro.ToString("dd/MM/yyyy H:mm"); } }
        public string Status { get; set; }
        public int? UsuarioCadastroId { get; set; }
        public int? UsuarioEdicaoId { get; set; }
        public DateTime? DataHoraEdicao { get; set; }
    }
}