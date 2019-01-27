using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdminLte2.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ZenContext:DbContext
    {
        public ZenContext(): base("dbContextZen")
        {
        
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<PerfilAcoes> PerfilAcoes { get; set; }
        public DbSet<ConfigEmail> ConfigEmail { get; set; }

    }
}