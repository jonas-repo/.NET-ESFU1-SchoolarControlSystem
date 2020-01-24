namespace esfu1_controlEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avisoNorequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Avisos", "Aviso_descripcion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Avisos", "Aviso_descripcion", c => c.String(nullable: false));
        }
    }
}
