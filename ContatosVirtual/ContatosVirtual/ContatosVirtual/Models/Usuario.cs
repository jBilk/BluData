using System;

namespace ContatosVirtual.Models
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Permissao { get; set; }
        public string Status { get; set; }
        public int? UsuarioCadastroId { get; set; }
        public int? UsuarioEdicaoId { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime? DataHoraEdicao { get; set; }
    }
}