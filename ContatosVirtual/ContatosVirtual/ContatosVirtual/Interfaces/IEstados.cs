using ContatosVirtual.Models;
using System.Collections.Generic;

namespace ContatosVirtual.Interfaces
{
    public interface IEstados
    {
        IList<Estado> Lista();
        Estado BuscaPorId(int id);
        int? BuscarPorSigla(string estadoSigla);
    }
}
