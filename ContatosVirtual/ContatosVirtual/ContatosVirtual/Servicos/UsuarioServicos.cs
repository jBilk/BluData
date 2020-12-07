using ContatosVirtual.Interfaces;

namespace ContatosVirtual.Servicos
{
    public class UsuarioServicos
    {
        private readonly IUsuario _usuarios;

        public UsuarioServicos(IUsuario usuarios)
        {
            _usuarios = usuarios;
        }

        public bool EmailJahExiste(int id, string email)
        {
            bool ehIgualRegistroAtual = id == 0 ? false : _usuarios.BuscaPorId(id).Email.ToString().Equals(email);

            if (ehIgualRegistroAtual)
                return false;

            if (_usuarios.BuscaPorEmail(email) != null)
                return true;

            return false;
        }

        public bool NomeUsuarioJahExiste(int id, string nomeUsuario)
        {
            bool ehIgualRegistroAtual = id == 0 ? false : _usuarios.BuscaPorId(id).NomeUsuario.ToString().Equals(nomeUsuario);

            if (ehIgualRegistroAtual)
                return false;

            if (_usuarios.BuscarNomeUsuario(nomeUsuario) != null)
                return true;

            return false;
        }
    }
}