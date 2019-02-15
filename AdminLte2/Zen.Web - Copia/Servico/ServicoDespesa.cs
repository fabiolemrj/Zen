using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoDespesa
    {
        public Despesa ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Despesas.Find(id);
        }

        public IQueryable<Despesa> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Despesas.OrderBy(u => u.Nome);
            }
            else
            {
                return db.Despesas.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, Despesa despesa)
        {
            if (ObterObjetoPorId(db, despesa.Id) == null)
            {                
                db.Despesas.Add(despesa);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Despesa despesa)
        {
            var servsubDespesa = new ServicoSubDespesa();
            var lstSubDespesa = servsubDespesa.ObterListaObjetos(db, despesa.Id);
            foreach(var item in lstSubDespesa)
            {
                db.SubDespesas.Remove(item);                
            }            
            db.Despesas.Remove(despesa);
            db.SaveChanges();
        }
    }
}