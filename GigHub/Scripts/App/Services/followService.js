
var FollowService = function() {
    //var  button = $(e.target);

    var createFollowing = function (artistDetailId, done, fail) {
        $.post("/api/followartist", { ArtistDetailId: artistDetailId })
            .done(done)
            .fail(fail);
    }


    var deleteFollowing = function (artistDetailId, done, fail) {
        $.ajax({
            url: "/api/followartist/" + artistDetailId,
            method: "DELETE"
        })
         .done(done)
         .fail(fail);
    }


    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing

    }
   

}()


