using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContatosVirtual.DAO
{
    public class TelefonesDAO : ITelefones
    {
        public void Adiciona(Telefone telefone)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Telefones.Add(telefone);
                context.SaveChanges();
            }
        }

        public void Excluir(Telefone telefone)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Entry(telefone).State = EntityState.Deleted;
                context.SaveChanges();
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
            using(var contexto = new ContatosVirtualContext())
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
            using (var contexto = new ContatosVirtualContext())
            {
                List<Telefone> registros = contexto.Telefones
                    .Where(u => u.FornecedorId.ToString() == idFornecedor.ToString() && (u.Numero.ToString().Contains(pesquisa)))
                    .OrderByDescending(u => u.Id)
                    .Skip(inicio)
                    .Take(tamanho).ToList();
                return registros;
            }
        }

        public int QuantidadeTotalRegistros(string fornecedorId)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Telefones.Count(u => u.FornecedorId.ToString() == fornecedorId.ToString());
            }
        }

        public int RetornarQuantidadeRegistroComFiltro(string idFornecedor, string pesquisa)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Telefones
                    .Where(u => u.FornecedorId.ToString() == idFornecedor.ToString() && ( u.Numero.ToString().Contains(pesquisa)))
                    .OrderByDescending(u => u.Id)
                    .Count();
            }
        }

        public IList<Telefone> BuscarPorIdFornecedor(int idFornecedor)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Telefones.Where(u => u.FornecedorId == idFornecedor).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}