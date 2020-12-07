using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContatosVirtualTestes.TestesLogins
{
    public class FornecedoresFake : IFornecedor
    {
        public void Adicionar(Fornecedor fornecedor)
        {
            using (var context = new ContatosVirtualContext())
            {
                using(var trasaction = context.Database.BeginTransaction())
                {
                    context.Fornecedores.Add(fornecedor);
                    context.SaveChanges();

                    trasaction.Rollback();
                }
            }
        }

        public Fornecedor BuscaPorId(int id)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Fornecedores.FirstOrDefault(u => u.Id == id);
                }
                catch
                {
                    return null;
                }
            }
        }

        public void Editar(Fornecedor fornecedor)
        {
            using (var context = new ContatosVirtualContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    context.Entry(fornecedor).State = EntityState.Modified;
                    context.SaveChanges();

                    transaction.Rollback();
                }
            }
        }

        public Fornecedor BuscaPorCpfCnpj(string cnpjCpf)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Fornecedores.FirstOrDefault(x => x.CpfCnpj == cnpjCpf);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IList<Fornecedor> Lista()
        {
            throw new System.NotImplementedException();
        }

        public int QuantidadeTotalRegistros()
        {
            throw new System.NotImplementedException();
        }

        public void AlterarStatus(Fornecedor fornecedor)
        {
            using (var context = new ContatosVirtualContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Entry(fornecedor).State = EntityState.Modified;
                    context.SaveChanges();

                    transaction.Rollback();
                }
            }
        }

        public int RetornarQuantidadeRegistroComFiltro(string status, string pesquisa)
        {
            throw new System.NotImplementedException();
        }

        public IList<Fornecedor> ListaComFiltro(string status, string pesquisa, int inicio, int tamanho)
        {
            throw new System.NotImplementedException();
        }
    }
}
