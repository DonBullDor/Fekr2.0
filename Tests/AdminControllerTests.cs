using System;
using System.Collections.Generic;
using AutoMapper;
using Data.Admins;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Decids;
using Xunit;

namespace Tests
{
    public class AdminControllerTests : IDisposable
    {
        private Mock<IAdminApiRepo> _mockRepo;
        private AdminProfile _realProfile;
        private MapperConfiguration _configuration;
        private IMapper _mapper;

        public AdminControllerTests()
        {
            _mockRepo = new Mock<IAdminApiRepo>();
            _realProfile = new AdminProfile();
            _configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
        }

        public void Dispose()
        {
            _mockRepo = null;
            _mapper = null;
            _configuration = null;
            _realProfile = null;
        }

        [Fact]
        public void GetAdminsItems_ReturnsZeroAdmins_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<IAdminApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllDecids())
                .Returns(GetAdmins(0));
            var realProfile = new AdminProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new AdminController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllAdmins();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<Decid> GetAdmins(int num)
        {
            var commands = new List<Decid>();
            if (num > 0)
            {
                commands
                    .Add(new Decid
                    {
                        IdDecid = "1",
                        NomDecid = "NULL",
                        TitreDecid = "NULL",
                        EtatDecid = "NULL",
                        PwdDecid = "PWD"
                    });
            }
            return commands;
        }

        [Fact]
        public void GetAllAdmins_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllDecids())
                .Returns(GetAdmins(1));
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllAdmins();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<AdminReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllAdmins_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllDecids())
                .Returns(GetAdmins(1));
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllAdmins();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllAdmins_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllDecids())
                .Returns(GetAdmins(1));
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllAdmins(); //Assert
            Assert.IsType<ActionResult<IEnumerable<AdminReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetAdminById_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetDecid("0")).Returns(() => null);
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAdmin("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetAdminById_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAdmin("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAdminById_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAdmin("1");

            //Assert
            Assert.IsType<ActionResult<AdminReadDto>>(result);
        }

        [Fact]
        public void CreateAdmin_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateAdmin(new AdminCreateDto());

            //Assert
            Assert.IsType<ActionResult<AdminReadDto>>(result);
        }

        [Fact]
        public void CreateAdmin_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateAdmin(new AdminCreateDto());

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateAdmin_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateAdmin("1", new AdminUpdateDto());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateAdmin_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetDecid("0")).Returns(() => null);
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateAdmin("0", new AdminUpdateDto());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialDecidUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetDecid("0")).Returns(() => null);
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result =
                controller
                    .PartialAdminUpdate("0",
                    new JsonPatchDocument<AdminUpdateDto>());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteAdmin_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteAdmin("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteAdmin_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetDecid("0")).Returns(() => null);
            var controller = new AdminController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteAdmin("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}