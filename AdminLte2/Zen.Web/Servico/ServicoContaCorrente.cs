using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoContaCorrente
    {
        ServicoBanco servBanco = new ServicoBanco();


        public ContaCorrente ObterObjetoPorId(ZenContext db, int id)
        {
            return db.ContasCorrentes.Find(id);
        }

        public IQueryable<ContaCorrente> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            var lst = new List<ContaCorrente>();

            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                lst = db.ContasCorrentes.OrderBy(u => u.NomeAgencia).ToList();
            }
            else
            {
                lst = db.ContasCorrentes.Where(u => u.NomeAgencia.Contains(filtroNome)).OrderBy(u => u.NomeAgencia).ToList();
            }

            var lstCompl = new List<ContaCorrente>();
            foreach (var item in lst)
            {
                var id = "";
                if (!string.IsNullOrEmpty(item.IdBanco))
                    id = item.IdBanco;

                item.Banco = servBanco.ObterObjetoPorId(db, id);

                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }

        public IQueryable<ContaCorrente> ObterListaObjetos(ZenContext db, string filtroNome, int tpfiltro)
        {
            var lst = new List<ContaCorrente>();
            switch (tpfiltro)
            {
                case 1:
                    if (string.IsNullOrEmpty(filtroNome))
                        lst = db.ContasCorrentes.OrderBy(u => u.NomeAgencia).ToList();
                    else
                        lst = db.ContasCorrentes.Where(u => u.NomeAgencia.Contains(filtroNome)).OrderBy(u => u.NomeAgencia).ToList();

                    break;

                case 2:
                    if (string.IsNullOrEmpty(filtroNome))
                        lst = db.ContasCorrentes.OrderBy(u => u.NumeroConta).ToList();
                    else
                        lst = db.ContasCorrentes.Where(u => u.NumeroConta.Contains(filtroNome)).OrderBy(u => u.NumeroConta).ToList();
                    break;
                case 3:
                    {
                        var banco = servBanco.ObterListaObjetos(db, filtroNome).FirstOrDefault();

                        if (string.IsNullOrEmpty(filtroNome))
                            lst = db.ContasCorrentes.OrderBy(u => u.IdBanco).ToList();
                        else if(banco != null)
                            lst = db.ContasCorrentes.Where(u => u.IdBanco.Contains(banco.IdBanco)).OrderBy(u => u.IdBanco).ToList();
                        break;
                    }
            }

            var lstCompl = new List<ContaCorrente>();
            foreach (var item in lst)
            {
                var id = "";
                if (!string.IsNullOrEmpty(item.IdBanco))
                    id = item.IdBanco;

                item.Banco = servBanco.ObterObjetoPorId(db, id);
                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }


        public void Salvar(ZenContext db, ContaCorrente objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                objeto.Id = db.ContasCorrentes.Max(c => c.Id) + 1;
                db.ContasCorrentes.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, ContaCorrente objeto)
        {
            db.ContasCorrentes.Remove(objeto);
            db.SaveChanges();
        }

    }
}