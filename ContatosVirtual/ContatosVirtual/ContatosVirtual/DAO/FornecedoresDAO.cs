using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContatosVirtual.DAO
{
    public class FornecedoresDAO : IFornecedores
    {
        public void Adicionar(Fornecedor fornecedor)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Fornecedores.Add(fornecedor);
                context.SaveChanges();
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
                context.Entry(fornecedor).State = EntityState.Modified;
                context.SaveChanges();
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
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Fornecedores.ToList();
            }
        }

        public int QuantidadeTotalRegistros()
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Fornecedores.Count();
            }
        }

        public void AlterarStatus(Fornecedor fornecedor)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Entry(fornecedor).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public int RetornarQuantidadeRegistroComFiltro(string status, string pesquisa)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Fornecedores.Include("Empresa")
                    .Where(u => u.Status == status && (u.Id.ToString().Contains(pesquisa) || u.Nome.Contains(pesquisa)
                    || u.CpfCnpj.Contains(pesquisa) || u.DataHoraCadastro.ToString().Contains(pesquisa)
                    || u.Empresa.NomeFantasia.ToString().Contains(pesquisa)))
                    .OrderByDescending(u => u.Id)
                    .Count();
            }
        }

        public IList<Fornecedor> ListaComFiltro(string status, string pesquisa, int inicio, int tamanho)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                List<Fornecedor> registros = contexto.Fornecedores.Include("Empresa")
                    .Where(u => u.Status == status && (u.Id.ToString().Contains(pesquisa) || u.Nome.Contains(pesquisa)
                    || u.CpfCnpj.Contains(pesquisa) || u.DataHoraCadastro.ToString().Contains(pesquisa)
                    || u.Empresa.NomeFantasia.ToString().Contains(pesquisa)))
                    .OrderByDescending(u => u.Id)
                    .Skip(inicio)
                    .Take(tamanho).ToList();
                return registros;
            }
        }
    }
}