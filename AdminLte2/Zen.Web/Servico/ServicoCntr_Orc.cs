using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoCntr_Orc
    {
        public CntrOrc ObterObjetoPorId(ZenContext db, int id)
        {
            return db.CntrOrcs.FirstOrDefault();
        }

        public void Salvar(ZenContext db, CntrOrc objeto)
        {
            db.SaveChanges();
        }
    }
}