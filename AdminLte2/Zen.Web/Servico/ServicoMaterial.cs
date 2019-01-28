using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoMaterial
    {
        ServicoFornecedor servFornecedor = new ServicoFornecedor();

        public Material ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Materiais.Find(id);
        }


        public IQueryable<Material> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            var lst = new List<Material>();

            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                lst = db.Materiais.OrderBy(u => u.Nome).ToList();
            }
            else
            {
                lst = db.Materiais.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
            }

            var lstCompl = new List<Material>();
            foreach (var item in lst)
            {
                int id = -1;
                if(item.IdFornecedor.Value >0 )
                  id =  item.IdFornecedor.Value;

                item.Fornecedor = servFornecedor.ObterObjetoPorId(db, id);
                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }

        public void Salvar(ZenContext db, Material objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                objeto.Id = db.Materiais.Max(c => c.Id) + 1;
                db.Materiais.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Material objeto)
        {
            db.Materiais.Remove(objeto);
            db.SaveChanges();
        }
    }

}