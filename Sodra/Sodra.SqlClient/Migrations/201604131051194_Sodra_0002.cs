namespace Sodra.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sodra_0002 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Character", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Character", "ImagePath");
        }
    }
}
