using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoCntr_CprCr
    {
        ServicoContaReceber ServCr = new ServicoContaReceber();
        ServicoContaPagar servCp = new ServicoContaPagar();

        public Cntr_CpCr ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Cntr_CpCrs.FirstOrDefault(c=>c.Id == 0);
        }

        public void Salvar(ZenContext db, Cntr_CpCr objeto)
        {          
            db.SaveChanges();
        }
    }
}