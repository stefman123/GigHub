using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class AttendanceConfigurations : EntityTypeConfiguration<Attendence>
    {
        public AttendanceConfigurations()
        {
            HasKey(a => new {a.GigId, a.AttendeeId});

        }
    }
}