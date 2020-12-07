using ContatosVirtual.Interfaces;
using ContatosVirtual.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContatosVirtualTestes
{
    public class UsuariosFake : IUsuario
    {
        public void Adicionar(Usuario usuario)
        {
            using (var context = new ContatosVirtualContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();

                    transaction.Rollback();
                }
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
                    return contexto.Usuarios.Find(id);
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Entry(usuario).State = EntityState.Modified;
                    context.SaveChanges();

                    transaction.Rollback();
                }
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
            throw new System.NotImplementedException();
        }

        public void AlterarStatus(Usuario usuario)
        {
            using (var context = new ContatosVirtualContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Entry(usuario).State = EntityState.Modified;
                    context.SaveChanges();

                    transaction.Rollback();
                }
            }
        }

        public int RetornarQuantidadeRegistroComFiltro(int usuarioLogado, string status, string pesquisa)
        {
            throw new System.NotImplementedException();
        }

        public IList<Usuario> ListaComFiltro(string status, string pesquisa, int usuarioLogado, int inicio, int tamanho)
        {
            throw new System.NotImplementedException();
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