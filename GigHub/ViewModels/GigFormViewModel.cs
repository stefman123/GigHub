using System;
using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public String Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        //For dropdown list you need the numeric value
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}