using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoFornecedor
    {
        public Fornecedor ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Fornecedores.Find(id);
        }


        public IQueryable<Fornecedor> ObterListaObjetos(ZenContext db, string filtroNome, int tpfiltro)
        {
            var lst = new List<Fornecedor>();
            switch (tpfiltro)
            {
                case 1:
                    if (string.IsNullOrEmpty(filtroNome))
                        lst = db.Fornecedores.OrderBy(u => u.Nome).ToList();
                    else
                        lst = db.Fornecedores.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();

                    break;

                case 2:
                    if (string.IsNullOrEmpty(filtroNome))
                        lst = db.Fornecedores.OrderBy(u => u.Contato).ToList();
                    else
                        lst = db.Fornecedores.Where(u => u.Contato.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
                    break;
                case 3:
                    if (string.IsNullOrEmpty(filtroNome))
                        lst = db.Fornecedores.OrderBy(u => u.Tel1).ToList();
                    else
                        lst = db.Fornecedores.Where(u => u.Tel1.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
                    break;
                case 4:
                    if (string.IsNullOrEmpty(filtroNome))
                        lst = db.Fornecedores.OrderBy(u => u.Contato).ToList();
                    else
                        lst = db.Fornecedores.Where(u => u.Contato.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
                    break;
            }

            return lst.AsQueryable();
        }

        public void Salvar(ZenContext db, Fornecedor objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                objeto.Id = db.Fornecedores.Max(c => c.Id) + 1;
                db.Fornecedores.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Fornecedor objeto)
        {
            db.Fornecedores.Remove(objeto);
            db.SaveChanges();
        }


    }
}