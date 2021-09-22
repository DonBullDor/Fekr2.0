using System;
using System.Collections.Generic;
using AutoMapper;
using Data.Moyenne;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Moyenne;
using Xunit;

namespace Tests
{
    public class MoyenneClasseTests : IDisposable
    {
        private Mock<IMoyenneApiRepo> _mockRepo;
        private MoyenneProfile _realProfile;
        private MapperConfiguration _configuration;
        private IMapper _mapper;

        public MoyenneClasseTests()
        {
            _mockRepo = new Mock<IMoyenneApiRepo>();
            _realProfile = new MoyenneProfile();
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
        public void GetMoyenne_ReturnsZeroMoyennes_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<IMoyenneApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllMoyenne())
                .Returns(GetMoyenne(0));
            var realProfile = new MoyenneProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new MoyenneController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllEspMoyennes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<AMoyenne> GetMoyenne(int num)
        {
            var commands = new List<AMoyenne>();
            if (num > 0)
            {
                commands
                    .Add(new AMoyenne()
                    {
                        IdEt = "1",
                        CodeCl = "1 S 3",
                        CodeModule = "FKR-ECIV",
                        Semestre = 1,
                        Moyenne = 17
                    });
            }

            return commands;
        }

        [Fact]
        public void GetAllMoyennes_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllMoyenne())
                .Returns(GetMoyenne(1));
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEspMoyennes();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<MoyenneReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllMoyennes_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllMoyenne())
                .Returns(GetMoyenne(1));
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEspMoyennes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllMoyennes_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllMoyenne())
                .Returns(GetMoyenne(1));
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllEspMoyennes(); //Assert
            Assert.IsType<ActionResult<IEnumerable<MoyenneReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetMoyenneById_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetMoyenneById("0")).Returns(() => null);
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetMoyenne("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetMoyenneById_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetMoyenneById("1"))
                .Returns(new AMoyenne
                {
                    IdEt = "1",
                    CodeCl = "1 S 3",
                    CodeModule = "FKR-ECIV",
                    Semestre = 1,
                    Moyenne = 17
                });
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetMoyenne("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetMoyenneById_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetMoyenneById("1"))
                .Returns(new AMoyenne
                {
                    IdEt = "1",
                    CodeCl = "1 S 3",
                    CodeModule = "FKR-ECIV",
                    Semestre = 1,
                    Moyenne = 17
                });
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetMoyenne("1");

            //Assert
            Assert.IsType<ActionResult<MoyenneReadDto>>(result);
        }

        [Fact]
        public void c_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetMoyenneById("1"))
                .Returns(new AMoyenne
                {
                    IdEt = "1",
                    CodeCl = "1 S 3",
                    CodeModule = "FKR-ECIV",
                    Semestre = 1,
                    Moyenne = 17
                });
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateMoyenne(new MoyenneCreateDto());

            //Assert
            Assert.IsType<ActionResult<MoyenneReadDto>>(result);
        }

        [Fact]
        public void CreateMoyenne_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetMoyenneById("1"))
                .Returns(new AMoyenne
                {
                    IdEt = "1",
                    CodeCl = "1 S 3",
                    CodeModule = "FKR-ECIV",
                    Semestre = 1,
                    Moyenne = 17
                });
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateMoyenne(new MoyenneCreateDto());

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateMoyenne_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetMoyenneById("1"))
                .Returns(new AMoyenne
                {
                    IdEt = "1",
                    CodeCl = "1 S 3",
                    CodeModule = "FKR-ECIV",
                    Semestre = 1,
                    Moyenne = 17
                });
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateMoyenne("1", new MoyenneUpdateDto());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateMoyenne_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetMoyenneById("0")).Returns(() => null);
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateMoyenne("0", new MoyenneUpdateDto());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialMoyenneUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetMoyenneById("0")).Returns(() => null);
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result =
                controller
                    .PartialMoyenneUpdate("0",
                        new JsonPatchDocument<MoyenneUpdateDto>());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteMoyenne_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetMoyenneById("1"))
                .Returns(new AMoyenne
                {
                    IdEt = "1",
                    CodeCl = "1 S 3",
                    CodeModule = "FKR-ECIV",
                    Semestre = 1,
                    Moyenne = 17
                });
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteMoyenne("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMoyenne_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetMoyenneById("0")).Returns(() => null);
            var controller = new MoyenneController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteMoyenne("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}