namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFollowArtistUserIdToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowArtists", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowArtists", "Follower_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FollowArtists", new[] { "Artist_Id" });
            DropIndex("dbo.FollowArtists", new[] { "Follower_Id" });
            DropTable("dbo.FollowArtists");
        }
        
        public override void Down()
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
                .PrimaryKey(t => new { t.ArtistId, t.FollwerId });
            
            CreateIndex("dbo.FollowArtists", "Follower_Id");
            CreateIndex("dbo.FollowArtists", "Artist_Id");
            AddForeignKey("dbo.FollowArtists", "Follower_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FollowArtists", "Artist_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
