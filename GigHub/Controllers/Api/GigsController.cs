using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{

    [System.Web.Http.Authorize]
    public class GigsController : ApiController
    { 
    //private ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    
    public GigsController(IUnitOfWork unitOfWork)
    {
        //_context = new ApplicationDbContext();
        _unitOfWork = unitOfWork;
    }

    [System.Web.Http.HttpDelete]
    public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null)
                return NotFound();

            if (gig.ArtistId != userId)
            {
                 throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            //_context.Gigs
                //.Include(g => g.Attendances.Select(a => a.Attendee))
                //.Single(g => g.Id == id && g.ArtistId == userId);



            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }

     


        //public IHttpActionResult Update(int id)
        //{
        //    var userId = User.Identity.GetUserId();
        //}

    }
}
