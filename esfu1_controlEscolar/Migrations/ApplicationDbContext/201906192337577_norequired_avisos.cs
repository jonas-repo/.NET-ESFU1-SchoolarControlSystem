namespace esfu1_controlEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class norequired_avisos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Citatorio", "Citatorio_descripcion", c => c.String());
            AlterColumn("dbo.DocumentosFaltantes", "DocumentosFaltantes_descripcion", c => c.String());
            AlterColumn("dbo.Reporte", "Reporte_descripcion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reporte", "Reporte_descripcion", c => c.String(nullable: false));
            AlterColumn("dbo.DocumentosFaltantes", "DocumentosFaltantes_descripcion", c => c.String(nullable: false));
            AlterColumn("dbo.Citatorio", "Citatorio_descripcion", c => c.String(nullable: false));
        }
    }
}
