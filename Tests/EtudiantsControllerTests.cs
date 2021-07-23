using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Data;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository;
using Service.Repository.Etudiant;
using System;
using System.Collections.Generic;
using Xunit;
using Data.Etudiant;

namespace Tests
{
    public class EtudiantsControllerTests : IDisposable
    {
        private Mock<IEtudiantApiRepo> mockRepo;
        private EtudiantProfile realProfile;
        private MapperConfiguration configuration;
        private IMapper mapper;

        public EtudiantsControllerTests()
        {
            mockRepo = new Mock<IEtudiantApiRepo>();
            realProfile = new EtudiantProfile();
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
        public void GetEtudiantItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<IEtudiantApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllEtudiant())
                .Returns(GetEtudiants(0));
            var realProfile = new EtudiantProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllEspEtudiants();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<EspEtudiant> GetEtudiants(int num)
        {
            var commands = new List<EspEtudiant>();
            if (num > 0)
            {
                commands
                    .Add(new EspEtudiant
                    {
                        IdEt = "1",
                        NomEt = "Test",
                        Password = "desc"
                    });
            }
            return commands;
        }

        [Fact]
        public void GetAllEtudiants_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllEtudiant())
                .Returns(GetEtudiants(1));
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllEspEtudiants();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<EtudiantReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllEtudiants_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllEtudiant())
                .Returns(GetEtudiants(1));
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllEspEtudiants();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllEtudiants_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllEtudiant())
                .Returns(GetEtudiants(1));
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllEspEtudiants(); //Assert
            Assert.IsType<ActionResult<IEnumerable<EtudiantReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetEtudiantByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetEtudiant("0")).Returns(() => null);
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetEtudiant("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetEtudiantByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetEtudiant("1"))
                .Returns(new EspEtudiant
                {
                    IdEt = "1",
                    NomEt = "Test",
                    Password = "desc"
                });
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetEtudiant("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetEtudiantByID_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetEtudiant("1"))
                .Returns(new EspEtudiant
                {
                    IdEt = "1",
                    NomEt = "Test",
                    Password = "desc"
                });
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetEtudiant("1");

            //Assert
            Assert.IsType<ActionResult<EtudiantReadDto>>(result);
        }

        [Fact]
        public void CreateEtudiant_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetEtudiant("1"))
                .Returns(new EspEtudiant
                {
                    IdEt = "1",
                    NomEt = "Test",
                    Password = "desc"
                });
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateEtudiant(new EtudiantCreateDto { });

            //Assert
            Assert.IsType<ActionResult<EtudiantReadDto>>(result);
        }

        [Fact]
        public void CreateEtudiant_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetEtudiant("1"))
                .Returns(new EspEtudiant
                {
                    IdEt = "1",
                    NomEt = "Test",
                    Password = "desc"
                });
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateEtudiant(new EtudiantCreateDto { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateEtudiant_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetEtudiant("1"))
                .Returns(new EspEtudiant
                {
                    IdEt = "1",
                    NomEt = "Test",
                    Password = "desc"
                });
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.UpdateEtudiant("1", new EtudiantUpdateDto { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateEtudiant_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetEtudiant("0")).Returns(() => null);
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.UpdateEtudiant("0", new EtudiantUpdateDto { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialEtudiantUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetEtudiant("0")).Returns(() => null);
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result =
                controller
                    .PartialEtudiantUpdate("0",
                    new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<EtudiantUpdateDto
                    >
                    { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteEtudiant_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetEtudiant("1"))
                .Returns(new EspEtudiant
                {
                    IdEt = "1",
                    NomEt = "Test",
                    Password = "desc"
                });
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteEtudiant("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteEtudiant_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetEtudiant("0")).Returns(() => null);
            var controller = new EtudiantsController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteEtudiant("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
