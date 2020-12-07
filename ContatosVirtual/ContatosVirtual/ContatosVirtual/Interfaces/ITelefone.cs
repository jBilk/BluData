using ContatosVirtual.Models;
using System.Collections.Generic;

namespace ContatosVirtual.Interfaces
{
    public interface ITelefone
    {
        void Adiciona(Telefone telefone);
        void Excluir(Telefone telefone);
        Telefone BuscaPorId(int id);
        Telefone VerificarNumeroSeJahExiste(string numero);
        IList<Telefone> ListaComFiltro(string idFornecedor, string pesquisa, int inicio, int tamanho);
        int QuantidadeTotalRegistros(string fornecedorId);
        int RetornarQuantidadeRegistroComFiltro(string idFornecedor, string pesquisa);
        IList<Telefone> BuscarPorIdFornecedor(int idFornecedor);
    }
}
