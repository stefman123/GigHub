using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendences.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendence
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _context.Attendences.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

           var attendance = _context.Attendences.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);

            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendences.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}