using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoServ
    {
        ServicoSubDespesa servSubDesp = new ServicoSubDespesa();
        ServicoDespesa servDespesa = new ServicoDespesa(); 

        public Serv ObterObjetoPorId(ZenContext db, int id)
        {
            var _servico = db.Servicos.Find(id);
            if(_servico != null && _servico.IdSubDesp != null)
            {
                _servico.SubDespesa = servSubDesp.ObterObjetoPorId(db, _servico.IdSubDesp);

                if (_servico.IdSubDesp > 0)
                {
                    _servico.Despesa = _servico.SubDespesa.Nome;
                }else if(_servico.IdDesp > 0)
                {
                    var idDesp = _servico.IdDesp > 0 ? _servico.IdDesp.Value : -1;
                    _servico.Despesa = servDespesa.ObterObjetoPorId(db, idDesp).Nome;
                }
            }

            return _servico;
        }

        public IQueryable<Serv> ObterListaObjetos(ZenContext db, int idServ)
        {
            var lst = new List<Serv>();

            lst = db.Servicos.Where(u => u.Id == idServ).ToList();
          
            var lstCompl = new List<Serv>();
            foreach (var item in lst)
            {
                item.SubDespesa = servSubDesp.ObterObjetoPorId(db, item.IdSubDesp);

                if (item.IdSubDesp > 0)
                {
                    item.Despesa = servSubDesp.ObterObjetoPorId(db, item.IdSubDesp).Nome;
                }
                else if (item.IdDesp > 0)
                {
                    var idDesp = item.IdDesp > 0 ? item.IdDesp.Value : -1;
                    //_servico.Despesa = servDespesa.ObterObjetoPorId(db, idDesp).Nome;
                    item.Despesa = servDespesa.ObterObjetoPorId(db, idDesp).Nome;
                }

                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }

        public IQueryable<Serv> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            var lst = new List<Serv>();
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                lst = db.Servicos.OrderBy(u => u.Nome).ToList();
            }
            else
            {
                lst =  db.Servicos.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome).ToList();
            }

            var lstCompl = new List<Serv>();
            foreach (var item in lst)
            {
                item.SubDespesa = servSubDesp.ObterObjetoPorId(db, item.IdSubDesp);

                if (item.IdSubDesp > 0)
                {
                    item.Despesa = servSubDesp.ObterObjetoPorId(db,item.IdSubDesp).Nome;
                }
                else if (item.IdDesp > 0)
                {
                    var idDesp = item.IdDesp > 0 ? item.IdDesp.Value : -1;
                    //_servico.Despesa = servDespesa.ObterObjetoPorId(db, idDesp).Nome;
                    item.Despesa = servDespesa.ObterObjetoPorId(db, idDesp).Nome;
                }

                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }

        public void Salvar(ZenContext db, Serv objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                db.Servicos.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Serv objeto)
        {
            db.Servicos.Remove(objeto);
            db.SaveChanges();
        }
    }
}