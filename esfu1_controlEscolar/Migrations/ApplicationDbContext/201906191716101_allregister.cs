namespace esfu1_controlEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allregister : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Citatorio",
                c => new
                    {
                        Citatorio_id = c.Guid(nullable: false),
                        Citatorio_descripcion = c.String(nullable: false),
                        Citatorio_fecha = c.DateTime(nullable: false),
                        Alumno_Alumno_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Citatorio_id)
                .ForeignKey("dbo.Alumno", t => t.Alumno_Alumno_id, cascadeDelete: true)
                .Index(t => t.Alumno_Alumno_id);
            
            CreateTable(
                "dbo.DocumentosFaltantes",
                c => new
                    {
                        DocumentosFaltantes_id = c.Guid(nullable: false),
                        DocumentosFaltantes_descripcion = c.String(nullable: false),
                        DocumentosFaltantes_fecha = c.DateTime(nullable: false),
                        Alumno_Alumno_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentosFaltantes_id)
                .ForeignKey("dbo.Alumno", t => t.Alumno_Alumno_id, cascadeDelete: true)
                .Index(t => t.Alumno_Alumno_id);
            
            CreateTable(
                "dbo.Reporte",
                c => new
                    {
                        Reporte_id = c.Guid(nullable: false),
                        Reporte_descripcion = c.String(nullable: false),
                        Reporte_fecha = c.DateTime(nullable: false),
                        Alumno_Alumno_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Reporte_id)
                .ForeignKey("dbo.Alumno", t => t.Alumno_Alumno_id, cascadeDelete: true)
                .Index(t => t.Alumno_Alumno_id);
            
            CreateTable(
                "dbo.Avisos",
                c => new
                    {
                        Aviso_id = c.Guid(nullable: false),
                        Aviso_img = c.String(),
                        Aviso_descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Aviso_id);
            
            CreateTable(
                "dbo.Extraordinarios",
                c => new
                    {
                        Extraordinario_id = c.Guid(nullable: false),
                        Extraordinario_img = c.String(),
                        Extraordinario_fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Extraordinario_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reporte", "Alumno_Alumno_id", "dbo.Alumno");
            DropForeignKey("dbo.DocumentosFaltantes", "Alumno_Alumno_id", "dbo.Alumno");
            DropForeignKey("dbo.Citatorio", "Alumno_Alumno_id", "dbo.Alumno");
            DropIndex("dbo.Reporte", new[] { "Alumno_Alumno_id" });
            DropIndex("dbo.DocumentosFaltantes", new[] { "Alumno_Alumno_id" });
            DropIndex("dbo.Citatorio", new[] { "Alumno_Alumno_id" });
            DropTable("dbo.Extraordinarios");
            DropTable("dbo.Avisos");
            DropTable("dbo.Reporte");
            DropTable("dbo.DocumentosFaltantes");
            DropTable("dbo.Citatorio");
        }
    }
}
