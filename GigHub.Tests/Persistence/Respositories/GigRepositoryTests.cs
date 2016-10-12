using System;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Respositories;
using GigHub.Tests.Extentions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Persistence.Respositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendence>> _mockAttendance;

        [TestInitialize]
        public void TestIntialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();

            _mockAttendance = new Mock<DbSet<Attendence>>();

            var mocContext = new Mock<IApplicationDbContext>();
            mocContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mocContext.SetupGet(c => c.Attendences).Returns(_mockAttendance.Object);

            _repository = new GigRepository(mocContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1"};

            _mockGigs.setSource(new [] {gig});

            var gigs = _repository.GetFutureGigsByUserId(gig.ArtistId);

            gigs.Should().BeEmpty(); 

        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.setSource(new[] { gig });

            var gigs = _repository.GetFutureGigsByUserId(gig.ArtistId);

            gigs.Should().BeEmpty();

        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeFound()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.setSource(new[] { gig });

            var gigs = _repository.GetFutureGigsByUserId(gig.ArtistId + '-');

            gigs.Should().BeEmpty();

        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigsIsForTheGivenArtistAndIsInTheFuture_ShouldBeFound()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.setSource(new[] { gig });

            var gigs = _repository.GetFutureGigsByUserId(gig.ArtistId);

            gigs.Should().Contain(gig);

        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeFound()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            var user = new ApplicationUser();
                _mockGigs.setSource(new[] { gig });   

            var attendance = new Attendence() {Gig = gig, AttendeeId = "1", Attendee = user };

           
            _mockAttendance.setSource(new [] { attendance }); 
           

            var gigs = _repository.GetGigsUserAttending("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsForADifferentArtist_ShouldBeEmpty()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            var user = new ApplicationUser();
            _mockGigs.setSource(new[] { gig });

            var attendance = new Attendence() { Gig = gig, AttendeeId = "1", Attendee = user };

            _mockAttendance.setSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsInTheFutureForArtist_ShouldBeFound()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            var user = new ApplicationUser();
            _mockGigs.setSource(new[] { gig });

            var attendance = new Attendence() { Gig = gig, AttendeeId = "1", Attendee = user };

            _mockAttendance.setSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(gig.ArtistId);

            gigs.Should().Contain(gig);

        }

    }
}
