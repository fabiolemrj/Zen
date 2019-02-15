using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoMaquina
    {
        ServicoImpressor servImpressor = new ServicoImpressor();

        public Maquina ObterObjetoPorId(ZenContext db, int id)
        {
            var maquina = db.Maquinas.Find(id);
            if(maquina != null)
            {
                maquina.Auxiliar = servImpressor.ObterObjetoPorId(db, maquina.IdAuxiliar);
                maquina.Impressor = servImpressor.ObterObjetoPorId(db, maquina.IdImpressor);
            }          

            return maquina;
        }

        public IQueryable<Maquina> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            var lst = new List<Maquina>();

            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                lst = db.Maquinas.OrderBy(u => u.Nome).ToList();
            }
            else
            {
                lst = db.Maquinas.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
            }

            var lstCompl = new List<Maquina>();
            foreach(var item in lst)
            {
                item.Auxiliar = servImpressor.ObterObjetoPorId(db, item.IdAuxiliar);
                item.Impressor = servImpressor.ObterObjetoPorId(db, item.IdImpressor);
                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }
               

        public void Salvar(ZenContext db, Maquina maquina)
        {
            if (ObterObjetoPorId(db, maquina.Id) == null)
            {
                maquina.Id = db.Maquinas.Max(c=>c.Id)+1;
                db.Maquinas.Add(maquina);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Maquina maquina)
        {
            db.Maquinas.Remove(maquina);
            db.SaveChanges();
        }
    }
}