using ContatosVirtual.Models;
using System.Collections.Generic;

namespace ContatosVirtual.Interfaces
{
    public interface IFornecedor
    {
        void Adicionar(Fornecedor fornecedor);
        Fornecedor BuscaPorId(int id);
        void Editar(Fornecedor fornecedor);
        Fornecedor BuscaPorCpfCnpj(string cnpjCpf);
        IList<Fornecedor> Lista();
        int QuantidadeTotalRegistros();
        void AlterarStatus(Fornecedor fornecedor);
        int RetornarQuantidadeRegistroComFiltro(string status, string pesquisa);
        IList<Fornecedor> ListaComFiltro(string status, string pesquisa, int inicio, int tamanho);
    }
}
