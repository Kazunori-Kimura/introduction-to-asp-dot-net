namespace KnockoutTodo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Todoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        summary = c.String(nullable: false),
                        detail = c.String(),
                        limit = c.DateTime(nullable: false),
                        done = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Todoes");
        }
    }
}
