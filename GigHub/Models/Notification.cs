using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        public Notification()
        {           
        }

        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
            {
                throw new ArgumentException("gig");
            }

            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }
        public static Notification GigUpated(Gig newGig, DateTime origianlDateTime, string originalVenue)
        {
            var notification = new Notification( newGig, NotificationType.GigUpdated);
            notification.OriginalDateTime = origianlDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }

    }
}