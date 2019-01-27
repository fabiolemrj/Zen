using AdminLte2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace AdminLte2.Servico
{
    public class ServicoPerfilAcoes
    {
        public void Salvar(ZenContext db, List<PerfilAcoes> ltsperfilAcoes,int idPerfil)
        {
            if (idPerfil > 0)
            {
                using (DbContextTransaction dbTran = db.Database.BeginTransaction())
                {
                    try
                    {
                        var lstAcoes = ObterPerfilAcoesPorPerfil(db, idPerfil);

                        foreach (var item in lstAcoes)
                        {
                            db.PerfilAcoes.Remove(item);
                            db.SaveChanges();
                        }
                       

                        foreach (var item in ltsperfilAcoes)
                        {
                            if (ObterObjetoPorId(db, idPerfil, item.Acao) == null)
                            {
                                db.PerfilAcoes.Add(item);
                                db.SaveChanges();
                            }
                            
                        }

                        if (ltsperfilAcoes.Count() > 0)
                        {
                           
                        }

                        dbTran.Commit();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbTran.Rollback();
                        throw;
                    }
                }
            }
            
        }

        public PerfilAcoes ObterObjetoPorId(ZenContext db, int idPerfil, int idAcoes)
        {
            return db.PerfilAcoes.FirstOrDefault(u => u.IdPerfil == idPerfil && u.Acao == idAcoes);
        }

        public List<PerfilAcoes> ObterPerfilAcoesPorPerfil(ZenContext db, int idPerfil)
        {
            return db.PerfilAcoes.Where(u => u.IdPerfil == idPerfil).ToList();
        }
    }
}