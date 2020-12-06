using ContatosVirtual.Models;
using System.Collections.Generic;

namespace ContatosVirtual.Interfaces
{
    public interface IUsuarios
    {
        void Adicionar(Usuario usuario);
        Usuario Busca(string login, string senha);
        Usuario BuscaPorId(int id);
        void Editar(Usuario usuario);
        Usuario BuscaPorEmail(string email);
        int QuantidadeTotalRegistros();
        void AlterarStatus(Usuario usuario);
        int RetornarQuantidadeRegistroComFiltro(int usuarioLogado, string status, string pesquisa);
        IList<Usuario> ListaComFiltro(string status, string pesquisa, int usuarioLogado, int inicio, int tamanho);
        Usuario BuscarNomeUsuario(string nomeUsuario);
    }
}
