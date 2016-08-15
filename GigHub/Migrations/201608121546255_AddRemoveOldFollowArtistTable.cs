namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRemoveOldFollowArtistTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowArtists", "ArtistId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowArtists", "Follower_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FollowArtists", new[] { "ArtistId" });
            DropIndex("dbo.FollowArtists", new[] { "Follower_Id" });
            DropTable("dbo.FollowArtists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FollowArtists",
                c => new
                    {
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        FollwerId = c.String(nullable: false, maxLength: 128),
                        Follower_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ArtistId, t.FollwerId });
            
            CreateIndex("dbo.FollowArtists", "Follower_Id");
            CreateIndex("dbo.FollowArtists", "ArtistId");
            AddForeignKey("dbo.FollowArtists", "Follower_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FollowArtists", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
