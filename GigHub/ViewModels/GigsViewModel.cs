using System.Collections.Generic;
using System.Linq;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public bool ShowActions { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }
        public string SearchChars { get; set; }
        public ILookup<int, Attendence> Attendances { get; set; }
    }
}