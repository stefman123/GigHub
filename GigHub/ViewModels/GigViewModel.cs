using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using GigHub.Models;

namespace GigHub.ViewModels
{
   
    public class GigViewModel
    {
        public Gig Gig { get; set; }

        public bool IsAttending { get; set; }

        public bool IsFollowing { get; set; }
        //public int Id { get; set; }
        //public bool IsCanceled { get; set; }
        //public ApplicationUser Artist { get; set; }
        //public DateTime DateTime { get; set; }
        //public string Venue { get; set; }
        //public Genre Genre { get; set; }

        

    }
}