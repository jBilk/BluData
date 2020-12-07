using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContatosVirtual.DAO
{
    public class EstadosDAO : IEstado
    {
        public IList<Estado> Lista()
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Estados.ToList();
            }
        }

        public Estado BuscaPorId(int id)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Estados.FirstOrDefault(x => x.Id == id);
                }
                catch
                {
                    return null;
                }
            }
        }

        public int? BuscarPorSigla(string estadoSigla)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Estados.FirstOrDefault(x => x.Sigla == estadoSigla).Id;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}