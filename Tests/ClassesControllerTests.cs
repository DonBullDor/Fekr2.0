using AutoMapper;
using Data;
using Data.Classes;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Classes;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class ClassesControllerTests : IDisposable
    {
        private Mock<IClassesApiRepo> mockRepo;
        private ClasseProfile realProfile;
        private MapperConfiguration configuration;
        private IMapper mapper;

        public ClassesControllerTests()
        {
            mockRepo = new Mock<IClassesApiRepo>();
            realProfile = new ClasseProfile();
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
        public void GetClasseItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<IClassesApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllClasses())
                .Returns(GetClasses(0));
            var realProfile = new ClasseProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllClasses();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<Classe> GetClasses(int num)
        {
            var commands = new List<Classe>();
            if (num > 0)
            {
                commands
                    .Add(new Classe
                    {
                        CodeCl = "1",
                        LibelleCl = "4t2",
                        DescriptionCl = "desc"
                    });
            }
            return commands;
        }

        [Fact]
        public void GetAllClasses_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllClasses())
                .Returns(GetClasses(1));
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllClasses();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<ClasseReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllClasses_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllClasses())
                .Returns(GetClasses(1));
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllClasses();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllClasses_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllClasses())
                .Returns(GetClasses(1));
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllClasses(); //Assert
            Assert.IsType<ActionResult<IEnumerable<ClasseReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetClasseByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetClasse("0")).Returns(() => null);
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetClasse("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetClasseByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetClasse("1"))
                .Returns(new Classe
                {
                    CodeCl = "1",
                    LibelleCl = "4t2",
                    DescriptionCl = "desc"
                });
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetClasse("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetClasseByID_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetClasse("1"))
                .Returns(new Classe
                {
                    CodeCl = "1",
                    LibelleCl = "4t2",
                    DescriptionCl = "desc"
                });
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetClasse("1");

            //Assert
            Assert.IsType<ActionResult<ClasseReadDto>>(result);
        }

        [Fact]
        public void CreateClasse_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetClasse("1"))
                .Returns(new Classe
                {
                    CodeCl = "1",
                    LibelleCl = "4t2",
                    DescriptionCl = "desc"
                });
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateClasse(new ClasseCreateDto { });

            //Assert
            Assert.IsType<ActionResult<ClasseReadDto>>(result);
        }

        [Fact]
        public void CreateClasse_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetClasse("1"))
                .Returns(new Classe
                {
                    CodeCl = "1",
                    LibelleCl = "4t2",
                    DescriptionCl = "desc"
                });
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateClasse(new ClasseCreateDto { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateClasse_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetClasse("1"))
                .Returns(new Classe
                {
                    CodeCl = "1",
                    LibelleCl = "4t2",
                    DescriptionCl = "desc"
                });
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.UpdateClasse("1", new ClasseUpdateDto { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateClasse_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetClasse("0")).Returns(() => null);
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.UpdateClasse("0", new ClasseUpdateDto { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialClasseUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetClasse("0")).Returns(() => null);
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result =
                controller
                    .PartialClasseUpdate("0",
                    new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<ClasseUpdateDto
                    >
                    { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteClasse_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetClasse("1"))
                .Returns(new Classe
                {
                    CodeCl = "1",
                    LibelleCl = "4t2",
                    DescriptionCl = "desc"
                });
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteClasse("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteClasse_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetClasse("0")).Returns(() => null);
            var controller = new ClassesController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteClasse("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}