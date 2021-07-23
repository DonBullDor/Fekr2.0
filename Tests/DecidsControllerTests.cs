using AutoMapper;
using Data;
using Data.Decids;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Decids;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class DecidsControllerTests : IDisposable
    {
        private Mock<IDecidsApiRepo> mockRepo;
        private DecidProfile realProfile;
        private MapperConfiguration configuration;
        private IMapper mapper;

        public DecidsControllerTests()
        {
            mockRepo = new Mock<IDecidsApiRepo>();
            realProfile = new DecidProfile();
            configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }

        [Fact]
        public void GetDecidItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<IDecidsApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllDecids())
                .Returns(GetDecids(0));
            var realProfile = new DecidProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllDecids();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<Decid> GetDecids(int num)
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
        public void GetAllDecids_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllDecids())
                .Returns(GetDecids(1));
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllDecids();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<DecidReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllDecids_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllDecids())
                .Returns(GetDecids(1));
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllDecids();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllDecids_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllDecids())
                .Returns(GetDecids(1));
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllDecids(); //Assert
            Assert.IsType<ActionResult<IEnumerable<DecidReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetDecidByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetDecid("0")).Returns(() => null);
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetDecid("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetDecidByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetDecid("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetDecidByID_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetDecid("1");

            //Assert
            Assert.IsType<ActionResult<DecidReadDto>>(result);
        }

        [Fact]
        public void CreateDecid_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateDecid(new DecidCreateDto { });

            //Assert
            Assert.IsType<ActionResult<DecidReadDto>>(result);
        }

        [Fact]
        public void CreateDecid_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateDecid(new DecidCreateDto { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateDecid_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.UpdateDecid("1", new DecidUpdateDto { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateDecid_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetDecid("0")).Returns(() => null);
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.UpdateDecid("0", new DecidUpdateDto { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialDecidUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetDecid("0")).Returns(() => null);
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result =
                controller
                    .PartialDecidUpdate("0",
                    new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<DecidUpdateDto
                    >
                    { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteDecid_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetDecid("1"))
                .Returns(new Decid
                {
                    IdDecid = "1",
                    NomDecid = "NULL",
                    TitreDecid = "NULL",
                    EtatDecid = "NULL",
                    PwdDecid = "PWD"
                });
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteDecid("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteDecid_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetDecid("0")).Returns(() => null);
            var controller = new DecidsController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteDecid("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}