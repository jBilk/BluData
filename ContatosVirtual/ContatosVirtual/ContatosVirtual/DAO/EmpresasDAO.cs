using ContatosVirtual.Content.Enum;
using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContatosVirtual.DAO
{
    public class EmpresasDAO : IEmpresas
    {
        public void Adicionar(Empresa empresa)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Empresas.Add(empresa);
                context.SaveChanges();
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
                context.Entry(empresa).State = EntityState.Modified;
                context.SaveChanges();
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
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Empresas.ToList();
            }
        }

        public IList<Empresa> ListaEmpresasAtivada()
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Empresas.Where(u => u.Status == EnumStatusEmpresa.StatusAtivada.ToString())
                    .OrderBy(u => u.NomeFantasia).ToList();
            }
        }

        public int QuantidadeTotalRegistros()
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Empresas.Count();
            }
        }

        public void AlterarStatus(Empresa empresa)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Entry(empresa).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public int RetornarQuantidadeRegistroComFiltro(string status, string pesquisa)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Empresas.Include("Estado")
                    .Where(u => u.Status == status && (u.Id.ToString().Contains(pesquisa) || u.NomeFantasia.Contains(pesquisa)
                    || u.Cnpj.Contains(pesquisa) || u.Estado.Sigla.Contains(pesquisa)))
                    .OrderByDescending(u => u.Id)
                    .Count();
            }
        }

        public IList<Empresa> ListaComFiltro(string status, string pesquisa, int inicio, int tamanho)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                List<Empresa> registros = contexto.Empresas.Include("Estado")
                    .Where(u => u.Status == status && (u.Id.ToString().Contains(pesquisa) || u.NomeFantasia.Contains(pesquisa)
                    || u.Cnpj.Contains(pesquisa) || u.Estado.Sigla.Contains(pesquisa)))
                    .OrderByDescending(u => u.Id)
                    .Skip(inicio)
                    .Take(tamanho).ToList();
                return registros;
            }
        }

        public IList<Empresa> ListaEmpresasAtivadaMaisEmoresaFornecedor(int? empresaId)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Empresas.Where(u => u.Status == EnumStatusEmpresa.StatusAtivada.ToString() || u.Id == empresaId)
                    .OrderBy(u => u.NomeFantasia).ToList();
            }
        }
    }
}