namespace YGCGanpati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "EventTime");
        }
    }
}
