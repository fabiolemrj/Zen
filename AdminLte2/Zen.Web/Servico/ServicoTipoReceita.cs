
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoTipoReceita
    {

        public TipoReceita ObterObjetoPorId(ZenContext db, int id)
        {
            return db.TipoReceitas.Find(id);
        }

        public IQueryable<TipoReceita> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.TipoReceitas.OrderBy(u => u.Nome);
            }
            else
            {
                return db.TipoReceitas.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, TipoReceita objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                db.TipoReceitas.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, TipoReceita objeto)
        {
            db.TipoReceitas.Remove(objeto);
            db.SaveChanges();
        }


    }

}