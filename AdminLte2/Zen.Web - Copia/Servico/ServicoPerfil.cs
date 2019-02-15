
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoPerfil
    {
        public IEnumerable<Models.Perfil> ObterListaPerfils(ZenContext db, string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                return db.Perfis.OrderBy(u => u.Descricao);
            }
            else
            {
                return db.Perfis.Where(u => u.Descricao.Contains(filtro)).OrderBy(u => u.Descricao);
            }
        }

        public Perfil ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Perfis.Find(id);
        }

        public void Salvar(ZenContext db, Perfil perfil)
        {
            if (ObterObjetoPorId(db, perfil.Id) == null)
            {
                db.Perfis.Add(perfil);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Perfil perfil)
        {
            db.Perfis.Remove(perfil);
            db.SaveChanges();
        }
    }
}