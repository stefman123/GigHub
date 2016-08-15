namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowArtist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowArtists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false),
                        FollwerId = c.String(nullable: false, maxLength: 128),
                        Artist_Id = c.String(maxLength: 128),
                        Follower_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ArtistId, t.FollwerId })
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Follower_Id)
                .Index(t => t.Artist_Id)
                .Index(t => t.Follower_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowArtists", "Follower_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowArtists", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FollowArtists", new[] { "Follower_Id" });
            DropIndex("dbo.FollowArtists", new[] { "Artist_Id" });
            DropTable("dbo.FollowArtists");
        }
    }
}
