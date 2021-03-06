﻿
using AdminLte2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AdminLte2.Servico
{
    public class ServicoConfigEmail
    {
        public ConfigEmail ObterObjetoPorId(ZenContext db, string email)
        {
            return db.ConfigEmail.FirstOrDefault(e=>e.Email == email);
        }

        public ConfigEmail ObterObjeto(ZenContext db)
        {
            return db.ConfigEmail.FirstOrDefault();
        }

        public void Salvar(ZenContext db, ConfigEmail configEmail)
        {
            if (ObterObjetoPorId(db, configEmail.Email) == null)
            {
                db.ConfigEmail.Add(configEmail);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, ConfigEmail configEmail)
        {
            db.ConfigEmail.Remove(configEmail);
            db.SaveChanges();
        }
    }
}