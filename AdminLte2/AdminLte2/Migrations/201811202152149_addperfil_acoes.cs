namespace AdminLte2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addperfil_acoes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PerfilAcoes",
                c => new
                    {
                        IdPerfil = c.Int(nullable: false),
                        Acao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPerfil, t.Acao })
                .ForeignKey("dbo.Perfil", t => t.IdPerfil, cascadeDelete: true)
                .Index(t => t.IdPerfil);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PerfilAcoes", "IdPerfil", "dbo.Perfil");
            DropIndex("dbo.PerfilAcoes", new[] { "IdPerfil" });
            DropTable("dbo.PerfilAcoes");
        }
    }
}
