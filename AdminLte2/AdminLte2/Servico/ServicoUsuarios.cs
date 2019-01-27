using AdminLte2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLte2.Servico
{
    public class ServicoUsuarios
    {
        public IEnumerable<Models.Usuario> ObterListaUsuarios(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Usuarios.OrderBy(u => u.Nome);
            }
            else
            {
                return db.Usuarios.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }

        }

        public Usuario ObterUsuarioPorLogin(ZenContext db, string login)
        {
            return db.Usuarios.FirstOrDefault(u => u.Login == login);
        }

        public Usuario ObterUsuarioPorId(ZenContext db, int id)
        {
            return db.Usuarios.Find(id);
        }

        public void Salvar(ZenContext db, Usuario usuario)
        {
            if (ObterUsuarioPorId(db, usuario.Id) == null)
            {
                db.Usuarios.Add(usuario);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Usuario usuario)
        {
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
        }

        
    }
}