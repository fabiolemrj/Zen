namespace AdminLte2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        IdSituacao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Login = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Senha = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Tipo = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        IDSITUACAO = c.Int(nullable: false),
                        Foto = c.Binary(),
                        ExtFoto = c.String(maxLength: 50, storeType: "nvarchar"),
                        Funcao = c.String(maxLength: 50, storeType: "nvarchar"),
                        IdPerfil = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Perfil", t => t.IdPerfil, cascadeDelete: true)
                .Index(t => t.IdPerfil);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "IdPerfil", "dbo.Perfil");
            DropIndex("dbo.Usuarios", new[] { "IdPerfil" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Perfil");
        }
    }
}
