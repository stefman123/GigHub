
var ArtistController = function (followService) {
    var button;
    
    
    var init = function(container) {
        $(container).on("click", ".js-toggle-followDetails", toggleFollowing);
        //$(".js-toggle-followDetails").click(toggleFollowing);

    };

    var toggleFollowing = function (e) {
        button = $(e.target);
        var artistDetailId = button.attr("data-artistDetail-id");

        debugger;

       if (button.hasClass("btn-default"))
           followService.createFollowing(artistDetailId, done, fail);
       else 
           followService.deleteFollowing(artistDetailId, done, fail);
    };


    var done = function () {
        var text = (button.text() == "Following!" || button.hasClass("btn-info")) ? "Follow?" : "Following!";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);


        //button.removeClass("btn-default")
        //    .addClass("btn-info")
        //    .text("Following!");
    };

    var fail = function() {
        alert("Something Failed Artist");
    };

    return {
        init : init
    }

}(FollowService)