using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Data;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Modules;
using System;
using System.Collections.Generic;
using Xunit;
using Data.Module;

namespace Tests
{
    public class ModulesControllerTests : IDisposable
    {
        private Mock<IModuleApiRepo> _mockRepo;
        private ModuleProfile _realProfile;
        private MapperConfiguration _configuration;
        private IMapper _mapper;

        public ModulesControllerTests()
        {
            _mockRepo = new Mock<IModuleApiRepo>();
            _realProfile = new ModuleProfile();
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
        public void GetModule_ReturnsZeroModule_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<IModuleApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllModules())
                .Returns(GetModules(0));
            var realProfile = new ModuleProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new ModulesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllEspModules();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<EspModule> GetModules(int num)
        {
            var commands = new List<EspModule>();
            if (num > 0)
            {
                commands
                    .Add(new EspModule
                    {
                        CodeModule = "1",
                        Designation = "FKR-TEST2",
                        Etat = "A"
                    });
            }
            return commands;
        }

        [Fact]
        public void GetAllModules_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllModules())
                .Returns(GetModules(1));
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEspModules();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<ModuleReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllModules_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllModules())
                .Returns(GetModules(1));
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEspModules();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllModules_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllModules())
                .Returns(GetModules(1));
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEspModules(); //Assert
            Assert.IsType<ActionResult<IEnumerable<ModuleReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetModuleByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetModule("0")).Returns(() => null);
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetModule("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetModuleByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetModule("1"))
                .Returns(new EspModule
                {
                    CodeModule = "1",
                    Designation = "FKR-TEST2",
                    Etat = "A"
                });
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetModule("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetModuleByID_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetModule("1"))
                .Returns(new EspModule
                {
                    CodeModule = "1",
                    Designation = "FKR-TEST2",
                    Etat = "A"
                });
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetModule("1");

            //Assert
            Assert.IsType<ActionResult<ModuleReadDto>>(result);
        }

        [Fact]
        public void CreateModule_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetModule("1"))
                .Returns(new EspModule
                {
                    CodeModule = "1",
                    Designation = "FKR-TEST2",
                    Etat = "A"
                });
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateModule(new ModuleCreateDto { });

            //Assert
            Assert.IsType<ActionResult<ModuleReadDto>>(result);
        }

        [Fact]
        public void CreateModule_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetModule("1"))
                .Returns(new EspModule
                {
                    CodeModule = "1",
                    Designation = "FKR-TEST2",
                    Etat = "A"
                });
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateModule(new ModuleCreateDto { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateModule_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetModule("1"))
                .Returns(new EspModule
                {
                    CodeModule = "1",
                    Designation = "FKR-TEST2",
                    Etat = "A"
                });
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateModule("1", new ModuleUpdateDto { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateModule_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetModule("0")).Returns(() => null);
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateModule("0", new ModuleUpdateDto { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialModuleUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetModule("0")).Returns(() => null);
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result =
                controller
                    .PartialModuleUpdate("0",
                    new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<ModuleUpdateDto
                    >
                    { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteModule_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetModule("1"))
                .Returns(new EspModule
                {
                    CodeModule = "1",
                    Designation = "FKR-TEST2",
                    Etat = "A"
                });
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteModule("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteModule_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetModule("0")).Returns(() => null);
            var controller = new ModulesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteModule("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
