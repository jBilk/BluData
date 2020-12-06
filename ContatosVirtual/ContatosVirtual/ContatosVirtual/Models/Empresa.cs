using System;

namespace ContatosVirtual.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public Estado Estado { get; set; }
        public int? EstadoId { get; set; }
        public string EstadoSigla { get; set; }
        public string Cidade { get; set; }
        public string Status { get; set; }
        public int? UsuarioCadastroId { get; set; }
        public int? UsuarioEdicaoId { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime? DataHoraEdicao { get; set; }
    }
}