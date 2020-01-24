namespace esfu1_controlEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class curp_alumno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alumno", "Curp", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alumno", "Curp");
        }
    }
}
