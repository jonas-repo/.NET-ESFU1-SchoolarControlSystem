namespace esfu1_controlEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeAviso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avisos", "Aviso_fecha", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avisos", "Aviso_fecha");
        }
    }
}
