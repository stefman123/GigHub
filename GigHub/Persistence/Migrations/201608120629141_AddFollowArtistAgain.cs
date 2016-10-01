namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowArtistAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowArtists",
                c => new
                    {
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        FollwerId = c.String(nullable: false, maxLength: 128),
                        Follower_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ArtistId, t.FollwerId })
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Follower_Id)
                .Index(t => t.ArtistId)
                .Index(t => t.Follower_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowArtists", "Follower_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowArtists", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.FollowArtists", new[] { "Follower_Id" });
            DropIndex("dbo.FollowArtists", new[] { "ArtistId" });
            DropTable("dbo.FollowArtists");
        }
    }
}
