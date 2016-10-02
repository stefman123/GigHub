using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class FollowingConfigurations : EntityTypeConfiguration<Following>
    {
        public FollowingConfigurations()
        {
            HasKey(f => new {f.FolloweeId, f.FollowerId});



        }
    }
}