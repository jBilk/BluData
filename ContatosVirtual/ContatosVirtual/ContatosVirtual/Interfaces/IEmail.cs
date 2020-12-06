using ContatosVirtual.Models;

namespace ContatosVirtual.Interfaces
{
    public interface IEmail
    {
        void ProcessoDeEnvioDeEmailNovoUsuario(Usuario usuario, string senhaCriptografada);
    }
}
