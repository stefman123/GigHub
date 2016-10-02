using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class ApplicationUserConfigurations : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfigurations()
        {
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

              HasMany(f => f.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);


            HasMany(f => f.Followees)
            .WithRequired(f => f.Follower)
            .WillCascadeOnDelete(false);


        }


    }
}