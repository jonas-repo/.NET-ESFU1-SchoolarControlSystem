namespace esfu1_controlEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alumno_pass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alumno", "ApellidoPaterno", c => c.String(nullable: false));
            AddColumn("dbo.Alumno", "ApellidoMaterno", c => c.String(nullable: false));
            AddColumn("dbo.Alumno", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Alumno", "Password");
            DropColumn("dbo.Alumno", "ApellidoMaterno");
            DropColumn("dbo.Alumno", "ApellidoPaterno");
        }
    }
}
