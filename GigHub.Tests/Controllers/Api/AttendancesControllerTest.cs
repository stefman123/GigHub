using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extentions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTest
    {
        private Mock<IAttendanceRepository> _mockRepository;
        private AttendancesController _controller;
        private string _userId;
        private int _gigId;

        [TestInitialize]
        public void TestInitializer()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _gigId = 1;
            _controller.MockCurrentUser(_userId, "userTest1@domain.com");
        }

        [TestMethod]
        public void Attend_NoAttendanceWithGivenGigIdExsist_ShouldReturnNotFoundResult()
        {
            var attendancedto = new Attendance();
            var result = _controller.Attend(attendancedto);

            result.Should().BeOfType<NotFoundResult>();

        }

        [TestMethod]
        public void Attend_AttendanceAlreadyExsits_ShouldReturnBadRequest()
        {
            var attendance = new Attendence { AttendeeId = _userId, GigId = _gigId};

            _mockRepository.Setup(r => r.GetAttendence(_userId, _gigId)).Returns(attendance);

            var attendancedto = new Attendance {GigId = _gigId};

            var result = _controller.Attend(attendancedto);

            result.Should().BeOfType<BadRequestErrorMessageResult>("The attendance already exists.");
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOK()
        {
            var attendance = new Attendence { AttendeeId = _userId, GigId = _gigId };

            _mockRepository.Setup(r => r.GetAttendence(_userId, _gigId)).Returns(attendance);

            var attendancedto = new Attendance { GigId = _gigId };

            var result = _controller.Attend(attendancedto);

            result.Should().BeOfType<BadRequestErrorMessageResult>("The attendance already exists.");

        }

    }
}
