using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfigurations :EntityTypeConfiguration<UserNotification>
    {

        public UserNotificationConfigurations()
        {
            HasKey(u => new {u.UserId, u.NotificationId});

        }

    }
}