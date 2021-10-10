using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using Data.Admins;
using Data.EmploiDuTemp;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Decids;
using Service.Repository.EmploiDuTemp;
using Xunit;

namespace Tests
{
    public class EmploiDuTempControllerTests : IDisposable
    {
        private Mock<IEmploiDuTempRepo> _mockRepo;
        private EmploiDuTempProfiler _realProfile;
        private MapperConfiguration _configuration;
        private IMapper _mapper;

        public EmploiDuTempControllerTests()
        {
            _mockRepo = new Mock<IEmploiDuTempRepo>();
            _realProfile = new EmploiDuTempProfiler();
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
        public void GetEmploiDuTemp_ReturnsZeroAdmins_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<IEmploiDuTempRepo>();
            mockRepo
                .Setup(repo => repo.GetAllEmploiDuTemp())
                .Returns(GetEmploiDuTemps(1));
            var realProfile = new AdminProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new EmploiDuTempController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private static IEnumerable<EspEmploi> GetEmploiDuTemps(int num)
        {
            var commands = new List<EspEmploi>();
            if (num > 0)
            {
                commands
                    .Add(new EspEmploi
                    {
                        CodeModule = "KD-04",
                        AnneeDeb = "2015",
                        Semestre = 2,
                        CodeCl = "7 B 1",
                        NumSeance = 2,
                        Jour = "01",
                        TypeSeance = "N"
                    });
            }

            return commands;
        }

        [Fact]
        public void GetAllEmploi_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTemp())
                .Returns(GetEmploiDuTemps(2));
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            var okResult = result.Result as OkObjectResult;
            if (okResult?.Value is List<EmploiDuTempReadDto> commands) Assert.Single((IEnumerable) commands);
        }

        [Fact]
        public void GetAllEmploiDuTemps_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTemp())
                .Returns(GetEmploiDuTemps(1));
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllEmploiDuTemps_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTemp())
                .Returns(GetEmploiDuTemps(1));
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAll(); //Assert
            Assert.IsType<ActionResult<IEnumerable<EspEmploi>>>(result);
        }

        //flag

        [Fact]
        public void GetEmploiDuTempByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllEmploiDuTempByClasse("0")).Returns(() => null);
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEmploiDuTempByClasse("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        
/*
        [Fact]
        public void GetEmploiDuTempByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTempByClasse("7 B 1"))
                .Returns(new EspEmploi
                {
                    CodeModule = "KD-04",
                    AnneeDeb = "2015",
                    Semestre = 2,
                    CodeCl = "7 B 1",
                    NumSeance = 2,
                    Jour = "01",
                    TypeSeance = "N"
                });
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEmploiDuTempByClasse("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetEmploiDuTempByID_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTempByClasse("1"))
                .Returns(new EspEmploi
                {
                    CodeModule = "KD-04",
                    AnneeDeb = "2015",
                    Semestre = 2,
                    CodeCl = "7 B 1",
                    NumSeance = 2,
                    Jour = "01",
                    TypeSeance = "N"
                });
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEmploiDuTempByClasse("1");

            //Assert
            Assert.IsType<ActionResult<EmploiDuTempReadDto>>(result);
        }

        [Fact]
        public void CreateAdmin_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTempByClasse("1"))
                .Returns(new EspEmploi
                {
                    CodeModule = "KD-04",
                    AnneeDeb = "2015",
                    Semestre = 2,
                    CodeCl = "7 B 1",
                    NumSeance = 2,
                    Jour = "01",
                    TypeSeance = "N"
                });
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateEmploiDuTemp(new EmploiDuTempCreateDto());

            //Assert
            Assert.IsType<ActionResult<EmploiDuTempReadDto>>(result);
        }

        [Fact]
        public void CreateAdmin_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTempByClasse("1"))
                .Returns(new EspEmploi
                {
                    CodeModule = "KD-04",
                    AnneeDeb = "2015",
                    Semestre = 2,
                    CodeCl = "7 B 1",
                    NumSeance = 2,
                    Jour = "01",
                    TypeSeance = "N"
                });
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateEmploiDuTemp(new EmploiDuTempCreateDto());

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateAdmin_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTempByClasse("1"))
                .Returns(new EspEmploi
                {
                    CodeModule = "KD-04",
                    AnneeDeb = "2015",
                    Semestre = 2,
                    CodeCl = "7 B 1",
                    NumSeance = 2,
                    Jour = "01",
                    TypeSeance = "N"
                });
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateEmploiDuTemp("1", new EmploiDuTempReadDto());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateAdmin_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllEmploiDuTempByClasse("0")).Returns(() => null);
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateEmploiDuTemp("0", new EmploiDuTempReadDto());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialEmploiDuTempUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllEmploiDuTempByClasse("0")).Returns(() => null);
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result =
                controller
                    .PartialEmploiDuTempUpdate("0",
                        new JsonPatchDocument<EmploiDuTempReadDto>());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteEmploiDuTemp_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllEmploiDuTempByClasse("1"))
                .Returns(new EspEmploi
                {
                    CodeModule = "KD-04",
                    AnneeDeb = "2015",
                    Semestre = 2,
                    CodeCl = "7 B 1",
                    NumSeance = 2,
                    Jour = "01",
                    TypeSeance = "N"
                });
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteEmploiDuTemp("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
*/
        [Fact]
        public void DeleteEmploiDuTemp_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllEmploiDuTempByClasse("0")).Returns(() => null);
            var controller = new EmploiDuTempController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteEmploiDuTemp("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}