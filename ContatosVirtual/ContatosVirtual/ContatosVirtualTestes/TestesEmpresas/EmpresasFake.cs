using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContatosVirtualTestes.TestesEmpresas
{
    public class EmpresasFake : IEmpresa
    {
        public void Adicionar(Empresa empresa)
        {
            using (var context = new ContatosVirtualContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Empresas.Add(empresa);
                    context.SaveChanges();

                    transaction.Rollback();
                }
            }
        }

        public Empresa BuscaPorId(int id)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Empresas.FirstOrDefault(x => x.Id == id);
                }
                catch
                {
                    return null;
                }
            }
        }

        public void Editar(Empresa empresa)
        {
            using (var context = new ContatosVirtualContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Entry(empresa).State = EntityState.Modified;
                    context.SaveChanges();

                    transaction.Rollback();
                }
            }
        }

        public Empresa BuscaPorCnpj(string cnpj)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Empresas.FirstOrDefault(x => x.Cnpj == cnpj);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IList<Empresa> Lista()
        {
            throw new System.NotImplementedException();
        }

        public IList<Empresa> ListaEmpresasAtivada()
        {
            throw new System.NotImplementedException();
        }

        public int QuantidadeTotalRegistros()
        {
            throw new System.NotImplementedException();
        }

        public void AlterarStatus(Empresa empresa)
        {
            using (var context = new ContatosVirtualContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Entry(empresa).State = EntityState.Modified;
                    context.SaveChanges();

                    transaction.Rollback();
                }
            }
        }

        public int RetornarQuantidadeRegistroComFiltro(string status, string pesquisa)
        {
            throw new System.NotImplementedException();
        }

        public IList<Empresa> ListaComFiltro(string status, string pesquisa, int inicio, int tamanho)
        {
            throw new System.NotImplementedException();
        }

        public IList<Empresa> ListaEmpresasAtivadaMaisEmoresaFornecedor(int? empresaId)
        {
            throw new System.NotImplementedException();
        }
    }
}
