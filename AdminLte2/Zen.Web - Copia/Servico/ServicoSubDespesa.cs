using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoSubDespesa
    {
        ServicoDespesa servDespesa = new ServicoDespesa();
        public SubDespesa ObterObjetoPorId(ZenContext db, int? id =-1)
        {
            var objeto = db.SubDespesas.Find(id);
            if(objeto != null)
            {
                objeto.Despesa = servDespesa.ObterObjetoPorId(db, objeto.IdDesp);
            }
            return objeto;
        }

        public IQueryable<SubDespesa> ObterListaObjetos(ZenContext db, int idDesp, string filtroNome)
        {
            var lst = new List<SubDespesa>();

            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                lst = db.SubDespesas.Where(l=>l.IdDesp == idDesp).OrderBy(u => u.Nome).ToList();
               
            }
            else
            {
                lst = db.SubDespesas.Where(u => u.IdDesp == idDesp && u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
            }

            var lstCompl = new List<SubDespesa>();
            foreach(var item in lst)
            {
                item.Despesa = servDespesa.ObterObjetoPorId(db, item.IdDesp);
                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }

        public IQueryable<SubDespesa> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            var lst = new List<SubDespesa>();

            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                lst = db.SubDespesas.OrderBy(u => u.Nome).ToList();

            }
            else
            {
                lst = db.SubDespesas.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
            }

            var lstCompl = new List<SubDespesa>();
            foreach (var item in lst)
            {
                item.Despesa = servDespesa.ObterObjetoPorId(db, item.IdDesp);
                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }

        public IQueryable<SubDespesa> ObterListaObjetos(ZenContext db, int idDesp)
        {
            var lst  =  db.SubDespesas.Where(u => u.IdDesp == idDesp).OrderBy(u => u.Nome);
            var lstCompl = new List<SubDespesa>();
            foreach (var item in lst)
            {
                item.Despesa = servDespesa.ObterObjetoPorId(db, item.IdDesp);
                lstCompl.Add(item);
            }

            return lstCompl as IQueryable<SubDespesa>;
        }

        public void Salvar(ZenContext db, SubDespesa subdespesa)
        {
            if (ObterObjetoPorId(db, subdespesa.Id) == null)
            {
                db.SubDespesas.Add(subdespesa);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, SubDespesa subdespesa)
        {
            db.SubDespesas.Remove(subdespesa);
            db.SaveChanges();
        }
    }
}