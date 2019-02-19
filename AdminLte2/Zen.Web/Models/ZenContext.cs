using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
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
        public DbSet<Cliente> CLientes { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Cor> Cores { get; set; }
        public DbSet<Facas> Facas { get; set; }
        public DbSet<FormaPag> FormasPag { get; set; }
        public DbSet<Impressor> Impressores { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<SubDespesa> SubDespesas { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<TipoReceita> TipoReceitas { get; set; }
        public DbSet<TipoDoc> TiposDoc { get; set; }
        public DbSet<TipoTinta> TipoTintas { get; set; }
        public DbSet<Transporte> Transportes { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Serv> Servicos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<ServFornec> ServFornecs { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<ContaCorrente> ContasCorrentes { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<ContasReceber> ContasReceber { get; set; }
        public DbSet<ContaPagar> ContasPagar { get; set; }
        public DbSet<ContaPagarFixa> ContasPagarFixas { get; set; }
        public DbSet<MovCc> MovCcs { get; set; }
        public DbSet<Cntr_CpCr> Cntr_CpCrs { get; set; }

    }
}