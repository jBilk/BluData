using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContatosVirtual.DAO
{
    public class UsuariosDAO : IUsuario
    {
        public void Adicionar(Usuario usuario)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
        }

        public Usuario Busca(string login, string senha)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Usuarios.FirstOrDefault(u => (u.NomeUsuario == login || u.Email == login) && u.Senha == senha);
                }
                catch
                {
                    return null;
                }
            }
        }

        public Usuario BuscaPorId(int id)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Usuarios.FirstOrDefault(u => u.Id == id);
                }
                catch
                {
                    return null;
                }
            }
        }

        public void Editar(Usuario usuario)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Entry(usuario).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Usuario BuscaPorEmail(string email)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Usuarios.FirstOrDefault(u => u.Email.ToString() == email.ToString());
                }
                catch
                {
                    return null;
                }
            }
        }

        public int QuantidadeTotalRegistros()
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Usuarios.Count();
            }
        }

        public void AlterarStatus(Usuario usuario)
        {
            using (var context = new ContatosVirtualContext())
            {
                context.Entry(usuario).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public int RetornarQuantidadeRegistroComFiltro(int usuarioLogado, string status, string pesquisa)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                return contexto.Usuarios
                    .Where(u => u.Status == status && u.Id != usuarioLogado && (u.Id.ToString().Contains(pesquisa)
                    || u.Nome.Contains(pesquisa) || u.NomeUsuario.Contains(pesquisa) || u.Permissao.Contains(pesquisa)))
                    .OrderByDescending(u => u.Id)
                    .Count();
            }
        }

        public IList<Usuario> ListaComFiltro(string status, string pesquisa, int usuarioLogado, int inicio, int tamanho)
        {
            using (var contexto = new ContatosVirtualContext())
            {

                List<Usuario> registros = contexto.Usuarios
                    .Where(u => u.Status == status && u.Id != usuarioLogado && (u.Id.ToString().Contains(pesquisa)
                    || u.Nome.Contains(pesquisa) || u.NomeUsuario.Contains(pesquisa) || u.Permissao.Contains(pesquisa)))
                    .OrderByDescending(u => u.Id)
                    .Skip(inicio)
                    .Take(tamanho).ToList();
                return registros;
            }
        }

        public Usuario BuscarNomeUsuario(string nomeUsuario)
        {
            using (var contexto = new ContatosVirtualContext())
            {
                try
                {
                    return contexto.Usuarios.FirstOrDefault(u => u.NomeUsuario.ToString() == nomeUsuario.ToString());
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}