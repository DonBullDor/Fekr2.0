using System;
using System.Collections.Generic;
using AutoMapper;
using Data.PlanEtude;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Plan_etude;
using Xunit;

namespace Tests
{
    public class PlanEtudeControllerTests : IDisposable
    {
        private Mock<IPlanEtudeApiRepo> _mockRepo;
        private PlanEtudeProfile _realProfile;
        private MapperConfiguration _configuration;
        private IMapper _mapper;

        public PlanEtudeControllerTests()
        {
            _mockRepo = new Mock<IPlanEtudeApiRepo>();
            _realProfile = new PlanEtudeProfile();
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
        public void GetPlanEtudes_ReturnsZeroPlanEtude_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<IPlanEtudeApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllPlanEtude())
                .Returns(GetPlanEtudes(0));
            var realProfile = new PlanEtudeProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new PlanEtudeController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private static IEnumerable<EspModulePanierClasseSaiso> GetPlanEtudes(int num)
        {
            var plans = new List<EspModulePanierClasseSaiso>();
            if (num > 0)
            {
                plans
                    .Add(new EspModulePanierClasseSaiso
                    {
                        CodeModule = "KD-07",
                        CodeCl = "7 B 2",
                        AnneeDeb = "2017",
                        NumSemestre = 1,
                        IdEns = "S-32-16"
                    });
            }
            return plans;
        }

        [Fact]
        public void GetAllPlanEtudes_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtude())
                .Returns(GetPlanEtudes(1));
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<PlanEtudeReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllPlanEtudes_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtude())
                .Returns(GetPlanEtudes(1));
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllPlanEtudes_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtude())
                .Returns(GetPlanEtudes(1));
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAll(); //Assert
            Assert.IsType<ActionResult<IEnumerable<PlanEtudeReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetPlanEtudeById_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllPlanEtudeByEnseignant("0")).Returns(() => null);
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllPlanEtudeByEnseignant("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
/*
        [Fact]
        public void GetPlanEtudeById_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtudeByModule("1"))
                .Returns(new EspModulePanierClasseSaiso
                {
                    CodeModule = "KD-07",
                    CodeCl = "7 B 2",
                    AnneeDeb = "2017",
                    NumSemestre = 1,
                    IdEns = "S-32-16"
                });
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllPlanEtudeByModule("KD-07");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetPlanEtudeById_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtudeByEnseignant("S-32-16"))
                .Returns(new EspModulePanierClasseSaiso
                {
                    CodeModule = "KD-07",
                    CodeCl = "7 B 2",
                    AnneeDeb = "2017",
                    NumSemestre = 1,
                    IdEns = "S-32-16"
                });
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllPlanEtudeByEnseignant("S-32-16");

            //Assert
            Assert.IsType<ActionResult<PlanEtudeReadDto>>(result);
        }

        [Fact]
        public void CreateAdmin_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtudeByEnseignant("S-32-16"))
                .Returns(new EspModulePanierClasseSaiso
                {
                    CodeModule = "KD-07",
                    CodeCl = "7 B 2",
                    AnneeDeb = "2017",
                    NumSemestre = 1,
                    IdEns = "S-32-16"
                });
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreatePlanEtude(new PlanEtudeCreateDto());

            //Assert
            Assert.IsType<ActionResult<PlanEtudeReadDto>>(result);
        }

        [Fact]
        public void CreateAdmin_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtudeByEnseignant("1"))
                .Returns(new EspModulePanierClasseSaiso
                {
                    CodeModule = "KD-07",
                    CodeCl = "7 B 2",
                    AnneeDeb = "2017",
                    NumSemestre = 1,
                    IdEns = "S-32-16"
                });
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreatePlanEtude(new PlanEtudeCreateDto());

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateAdmin_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtude("1"))
                .Returns(new EspModulePanierClasseSaiso
                {
                    CodeModule = "KD-07",
                    CodeCl = "7 B 2",
                    AnneeDeb = "2017",
                    NumSemestre = 1,
                    IdEns = "S-32-16"
                });
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdatePlanEtude("1", new PlanEtudeUpdateDto());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
*/
/*
        [Fact]
        public void UpdateAdmin_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllPlanEtudeByEnseignant("0")).Returns(() => null);
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdatePlanEtude("0", new PlanEtudeUpdateDto());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
*/
/*
        [Fact]
        public void PartialPlanEtudeUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllPlanEtudeByEnseignant("0")).Returns(() => null);
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result =
                controller
                    .PartialPlanEtudeUpdate("0",
                    new JsonPatchDocument<PlanEtudeUpdateDto>());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        */
/*
        [Fact]
        public void DeletePlanEtude_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllPlanEtudeByClasse("1"))
                .Returns(new EspModulePanierClasseSaiso
                {
                    CodeModule = "KD-07",
                    CodeCl = "7 B 2",
                    AnneeDeb = "2017",
                    NumSemestre = 1,
                    IdEns = "S-32-16"
                });
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeletePlanEtude("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
*/
/*
        [Fact]
        public void DeletePlanEtude_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetAllPlanEtudeByClasse("0")).Returns(() => null);
            var controller = new PlanEtudeController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeletePlanEtude("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        */
    }
}