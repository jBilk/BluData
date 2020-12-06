using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContatosVirtualTestes.TestesTelefones
{
    public class TelefonesFake : ITelefones
    {
        public void Adiciona(Telefone telefone)
        {
            using (var context = new ContatosVirtualContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Telefones.Add(telefone);
                    context.SaveChanges();

                    transaction.Rollback();
                }
            }
        }

        public void Excluir(Telefone telefone)
        {
            using (var context = new ContatosVirtualContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Entry(telefone).State = EntityState.Deleted;
                    context.SaveChanges();

                    transaction.Rollback();
                }
            }
        }

        public Telefone BuscaPorId(int id)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Telefones.First(x => x.Id == id);
                }
                catch
                {
                    return null;
                }
            }
        }

        public Telefone VerificarNumeroSeJahExiste(string numero)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Telefones.FirstOrDefault(x => x.Numero == numero);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IList<Telefone> ListaComFiltro(string idFornecedor, string pesquisa, int inicio, int tamanho)
        {
            throw new System.NotImplementedException();
        }

        public int QuantidadeTotalRegistros(string fornecedorId)
        {
            throw new System.NotImplementedException();
        }

        public int RetornarQuantidadeRegistroComFiltro(string idFornecedor, string pesquisa)
        {
            throw new System.NotImplementedException();
        }

        public IList<Telefone> BuscarPorIdFornecedor(int idFornecedor)
        {
            throw new System.NotImplementedException();
        }
    }
}
