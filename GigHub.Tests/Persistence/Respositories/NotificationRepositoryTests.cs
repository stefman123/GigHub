using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    public class NotificationRepositoryTests
    {
        //private Mock<DbSet<Notification>> _mockNotifications;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;
        private NotificationsRepository _repository;

        [TestInitialize]
        public void TestIntialize()
        {
            //_mockNotifications = new Mock<DbSet<Notification>>();
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();
            var mocContext = new Mock<IApplicationDbContext>();

            //mocContext.SetupGet(c => c.Notifications).Returns(_mockNotifications.Object);
            mocContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);

            _repository = new NotificationsRepository(mocContext.Object);

        }

        [TestMethod]
        public void GetNotificationsForUser_NotificationAreForDifferentUser_ShouldBeNotBeFound()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

           _mockUserNotifications.setSource(new[] { userNotification} );

            var notifications = _repository.GetNotificationsForUser(user.Id + '-');

            notifications.Should().BeEmpty();
        }


        [TestMethod]
        public void GetNotificationForUser_NotificationHasBeRead_ShouldBeEmpty()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            var notification = Notification.GigCanceled(gig);
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            userNotification.Read();

            _mockUserNotifications.setSource(new[] { userNotification });

            var notifications = _repository.GetNotificationsForUser(user.Id);

            notifications.Should().BeEmpty();

        }


        [TestMethod]
        public void GetNewNotificationsFor_NewNotificationForTheGivenUser_ShouldBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.setSource(new[] { userNotification });

            var notifications = _repository.GetNotificationsForUser(user.Id);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(notification);
        }

    }
}
