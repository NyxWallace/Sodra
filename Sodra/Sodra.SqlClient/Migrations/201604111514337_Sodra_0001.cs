namespace Sodra.SqlClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sodra_0001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Character",
                c => new
                    {
                        CharacterID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Health = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                        SessionID = c.Int(nullable: false),
                        Class = c.Int(nullable: false),
                        Race = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacterID)
                .ForeignKey("dbo.Player", t => t.PlayerID, cascadeDelete: true)
                .ForeignKey("dbo.Session", t => t.SessionID, cascadeDelete: true)
                .Index(t => t.PlayerID)
                .Index(t => t.SessionID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PlayerID);
            
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        SessionID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SessionID);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        SessionID = c.Int(nullable: false),
                        CharacterID = c.Int(nullable: false),
                        Message = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LogID)
                .ForeignKey("dbo.Character", t => t.CharacterID)
                .ForeignKey("dbo.Session", t => t.SessionID, cascadeDelete: true)
                .Index(t => t.SessionID)
                .Index(t => t.CharacterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Log", "SessionID", "dbo.Session");
            DropForeignKey("dbo.Log", "CharacterID", "dbo.Character");
            DropForeignKey("dbo.Character", "SessionID", "dbo.Session");
            DropForeignKey("dbo.Character", "PlayerID", "dbo.Player");
            DropIndex("dbo.Log", new[] { "CharacterID" });
            DropIndex("dbo.Log", new[] { "SessionID" });
            DropIndex("dbo.Character", new[] { "SessionID" });
            DropIndex("dbo.Character", new[] { "PlayerID" });
            DropTable("dbo.Log");
            DropTable("dbo.Session");
            DropTable("dbo.Player");
            DropTable("dbo.Character");
        }
    }
}
