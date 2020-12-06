using ContatosVirtual.Models;
using System.Collections.Generic;

namespace ContatosVirtual.Interfaces
{
    public interface IEmpresas
    {
        void Adicionar(Empresa empresa);
        Empresa BuscaPorId(int id);
        void Editar(Empresa empresa);
        Empresa BuscaPorCnpj(string cnpj);
        IList<Empresa> Lista();
        IList<Empresa> ListaEmpresasAtivada();
        int QuantidadeTotalRegistros();
        void AlterarStatus(Empresa empresa);
        int RetornarQuantidadeRegistroComFiltro(string status, string pesquisa);
        IList<Empresa> ListaComFiltro(string status, string pesquisa, int inicio, int tamanho);
        IList<Empresa> ListaEmpresasAtivadaMaisEmoresaFornecedor(int? empresaId);
    }
}
