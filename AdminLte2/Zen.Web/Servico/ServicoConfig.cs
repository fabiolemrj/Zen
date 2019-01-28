using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoConfig
    {
        public Config ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Configs.Find(id);
        }

        public void Salvar(ZenContext db, Config objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                db.Configs.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Config objeto)
        {
            db.Configs.Remove(objeto);
            db.SaveChanges();
        }

    }
}