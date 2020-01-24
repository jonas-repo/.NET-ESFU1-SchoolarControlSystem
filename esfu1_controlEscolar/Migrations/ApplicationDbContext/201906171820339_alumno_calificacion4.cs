namespace esfu1_controlEscolar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alumno_calificacion4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alumno",
                c => new
                    {
                        Alumno_id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                        CicloEscolar = c.String(nullable: false),
                        Grado = c.String(nullable: false),
                        Grupo = c.String(nullable: false),
                        Usuario_id = c.String(),
                    })
                .PrimaryKey(t => t.Alumno_id);
            
            CreateTable(
                "dbo.Calificacion",
                c => new
                    {
                        Calificacion_id = c.Guid(nullable: false),
                        Materia = c.String(),
                        Nombre = c.String(),
                        PrimerBimestre = c.Single(),
                        SegundoBimestre = c.Single(),
                        TercerBimestre = c.Single(),
                        CuartoBimestre = c.Single(),
                        QuintoBimestre = c.Single(),
                        FaltasPrimerBimestre = c.Int(),
                        FaltasSegundoBimestre = c.Int(),
                        FaltasTercerBimestre = c.Int(),
                        FaltasCuartoBimestre = c.Int(),
                        FaltasQuintoBimestre = c.Int(),
                        TotalFaltas = c.Int(),
                        TotalCalificacion = c.Single(),
                        Alumno_Alumno_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Calificacion_id)
                .ForeignKey("dbo.Alumno", t => t.Alumno_Alumno_id, cascadeDelete: true)
                .Index(t => t.Alumno_Alumno_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calificacion", "Alumno_Alumno_id", "dbo.Alumno");
            DropIndex("dbo.Calificacion", new[] { "Alumno_Alumno_id" });
            DropTable("dbo.Calificacion");
            DropTable("dbo.Alumno");
        }
    }
}
