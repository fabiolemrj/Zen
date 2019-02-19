using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoContaPagarObs
    {
        public ContaPagarObs ObterObjetoPorId(ZenContext db, int id)
        {
            string sqlString = "SELECT * FROM contaspagarobs " +
                    "where IDTITULO = "+id.ToString();
            var objctx = (db as IObjectContextAdapter).ObjectContext;

            ObjectQuery<ContaPagarObs> objeto = objctx.CreateQuery<ContaPagarObs>(sqlString);
            

            return null;
        }
    }
}