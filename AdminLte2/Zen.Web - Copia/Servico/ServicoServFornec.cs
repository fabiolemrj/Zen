using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoServFornec
    {
       // ServicoServ ServicoServ = new ServicoServ();
       // ServicoFornecedor ServFornecedor = new ServicoFornecedor();

        public IQueryable<ServFornec> ObterListaObjetos(ZenContext db, int idFornec)
        {
            var lst = db.ServFornecs.Where(u => u.IdFornecedor == idFornec).ToList();
            var lstCompl = new List<ServFornec>();
            foreach(var item in lst)
            {
                item.Serv = db.Servicos.FirstOrDefault(c=>c.Id == item.IdServico); //ServicoServ.ObterObjetoPorId(db,item.IdServico);
                item.Fornecedor = db.Fornecedores.FirstOrDefault(c => c.Id == item.IdFornecedor); //ServFornecedor.ObterObjetoPorId(db, item.IdFornecedor);
                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();            
        }

        public ServFornec ObterObjetoPorId(ZenContext db, int idFornec, int idServ)
        {
            var obj = db.ServFornecs.FirstOrDefault(c => c.IdFornecedor == idFornec && c.IdServico == idServ);
            if(obj != null)
            {
                obj.Serv = db.Servicos.FirstOrDefault(c => c.Id == obj.IdServico); 
                obj.Fornecedor = db.Fornecedores.FirstOrDefault(c => c.Id == obj.IdFornecedor); 

            }
            return obj;
        }

        public void Salvar(ZenContext db, ServFornec objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdFornecedor, objeto.IdServico) == null)
            {
                db.ServFornecs.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, ServFornec objeto)
        {
            db.ServFornecs.Remove(objeto);
            db.SaveChanges();
        }
    }
}