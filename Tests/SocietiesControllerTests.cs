using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Data;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Societes;
using System;
using System.Collections.Generic;
using Xunit;
using Data.Societes;

namespace Tests
{
    public class SocietiesControllerTests : IDisposable
    {
        private Mock<ISocietesApiRepo> mockRepo;
        private SocieteProfile realProfile;
        private MapperConfiguration configuration;
        private IMapper mapper;

        public SocietiesControllerTests()
        {
            mockRepo = new Mock<ISocietesApiRepo>();
            realProfile = new SocieteProfile();
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
        public void GetSocieteItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<ISocietesApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllSocietes())
                .Returns(GetSocietes(0));
            var realProfile = new SocieteProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllSocietes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<Societe> GetSocietes(int num)
        {
            var commands = new List<Societe>();
            if (num > 0)
            {
                commands
                    .Add(new Societe
                    {
                        CodeSoc = "1",
                        NomSoc = "ECOLE SECONDAIRE COLLEGE LYCEE TEST",
                        AnneeDeb = "2022"
                    });
            }
            return commands;
        }

        [Fact]
        public void GetAllSocietes_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllSocietes())
                .Returns(GetSocietes(1));
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllSocietes();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<SocieteReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllSocietes_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllSocietes())
                .Returns(GetSocietes(1));
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllSocietes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllSocietes_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllSocietes())
                .Returns(GetSocietes(1));
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllSocietes(); //Assert
            Assert.IsType<ActionResult<IEnumerable<SocieteReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetSocieteByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetSociete("0")).Returns(() => null);
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetSociete("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetSocieteByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetSociete("1"))
                .Returns(new Societe
                {
                    CodeSoc = "1",
                    NomSoc = "ECOLE SECONDAIRE COLLEGE LYCEE TEST",
                    AnneeDeb = "2022"
                });
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetSociete("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetSocieteByID_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetSociete("1"))
                .Returns(new Societe
                {
                    CodeSoc = "1",
                    NomSoc = "ECOLE SECONDAIRE COLLEGE LYCEE TEST",
                    AnneeDeb = "2022"
                });
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetSociete("1");

            //Assert
            Assert.IsType<ActionResult<SocieteReadDto>>(result);
        }

        [Fact]
        public void CreateSociete_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetSociete("1"))
                .Returns(new Societe
                {
                    CodeSoc = "1",
                    NomSoc = "ECOLE SECONDAIRE COLLEGE LYCEE TEST",
                    AnneeDeb = "2022"
                });
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateSociete(new SocieteCreateDto { });

            //Assert
            Assert.IsType<ActionResult<SocieteReadDto>>(result);
        }

        [Fact]
        public void CreateSociete_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetSociete("1"))
                .Returns(new Societe
                {
                    CodeSoc = "1",
                    NomSoc = "ECOLE SECONDAIRE COLLEGE LYCEE TEST",
                    AnneeDeb = "2022"
                });
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateSociete(new SocieteCreateDto { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateSociete_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetSociete("1"))
                .Returns(new Societe
                {
                    CodeSoc = "1",
                    NomSoc = "ECOLE SECONDAIRE COLLEGE LYCEE TEST",
                    AnneeDeb = "2022"
                });
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.UpdateSociete("1", new SocieteUpdateDto { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateSociete_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetSociete("0")).Returns(() => null);
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.UpdateSociete("0", new SocieteUpdateDto { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialSocieteUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetSociete("0")).Returns(() => null);
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result =
                controller
                    .PartialSocieteUpdate("0",
                    new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<SocieteUpdateDto
                    >
                    { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteSociete_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetSociete("1"))
                .Returns(new Societe
                {
                    CodeSoc = "1",
                    NomSoc = "ECOLE SECONDAIRE COLLEGE LYCEE TEST",
                    AnneeDeb = "2022"
                });
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteSociete("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteSociete_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetSociete("0")).Returns(() => null);
            var controller = new SocietesController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteSociete("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
