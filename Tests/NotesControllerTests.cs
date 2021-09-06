using System;
using System.Collections.Generic;
using AutoMapper;
using Data.Notes;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Profiles;
using Service.Repository.Notes;
using Xunit;

namespace Tests
{
    public class NotesControllerTests : IDisposable
    {
        private Mock<INotesApiRepo> _mockRepo;
        private NotesProfile _realProfile;
        private MapperConfiguration _configuration;
        private IMapper _mapper;

        public NotesControllerTests()
        {
            _mockRepo = new Mock<INotesApiRepo>();
            _realProfile = new NotesProfile();
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
        public void GetNotesById_ReturnsZeroNotes_WhenDBIsEmpty()
        {
            //Arrange
            var mockRepo = new Mock<INotesApiRepo>();
            mockRepo
                .Setup(repo => repo.GetAllNotes())
                .Returns(GetNotesByIds(0));
            var realProfile = new NotesProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            var controller = new NotesController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllNotes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<ANote> GetNotesByIds(int num)
        {
            var commands = new List<ANote>();
            if (num > 0)
            {
                commands
                    .Add(new ANote
                    {
                        IdEt = "1",
                        IdEns = "1",
                        CodeCl = "4GL2",
                        AnneeDeb = "2020",
                        CodeModule = "Dev",
                        Orale = 12,
                        Semestre = 1,
                        Dc1 = 16,
                        Dc2 = 18,
                        Ds = 12,
                        DateSaisie = DateTime.Now
                    });
            }

            return commands;
        }

        [Fact]
        public void GetAllNotes_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllNotes())
                .Returns(GetNotesByIds(1));
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllNotes();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<NoteReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllNotes_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllNotes())
                .Returns(GetNotesByIds(1));
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllNotes();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllNotes_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetAllNotes())
                .Returns(GetNotesByIds(1));
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetAllNotes(); //Assert
            Assert.IsType<ActionResult<IEnumerable<NoteReadDto>>>(result);
        }

        //flag

        [Fact]
        public void GetNotesByIdByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetNotesById("0")).Returns(() => null);
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetNotesById("1");

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetNotesByIdByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetNotesById("1"))
                .Returns(new ANote
                {
                    IdEt = "1",
                    IdEns = "1",
                    CodeCl = "4GL2",
                    AnneeDeb = "2020",
                    CodeModule = "Dev",
                    Orale = 12,
                    Semestre = 1,
                    Dc1 = 16,
                    Dc2 = 18,
                    Ds = 12,
                    DateSaisie = DateTime.Now
                });
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetNotesById("1");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetNotesByIdByID_Returns200OK__WhenValidIDProvided2()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetNotesById("1"))
                .Returns(new ANote
                {
                    IdEt = "1",
                    IdEns = "1",
                    CodeCl = "4GL2",
                    AnneeDeb = "2020",
                    CodeModule = "Dev",
                    Orale = 12,
                    Semestre = 1,
                    Dc1 = 16,
                    Dc2 = 18,
                    Ds = 12,
                    DateSaisie = DateTime.Now
                });
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.GetNotesById("1");

            //Assert
            Assert.IsType<ActionResult<NoteReadDto>>(result);
        }

        [Fact]
        public void CreateNotes_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetNotesById("1"))
                .Returns(new ANote
                {
                    IdEt = "1",
                    IdEns = "1",
                    CodeCl = "4GL2",
                    AnneeDeb = "2020",
                    CodeModule = "Dev",
                    Orale = 12,
                    Semestre = 1,
                    Dc1 = 16,
                    Dc2 = 18,
                    Ds = 12,
                    DateSaisie = DateTime.Now
                });
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateNotes(new NoteCreateDto { });

            //Assert
            Assert.IsType<ActionResult<NoteReadDto>>(result);
        }

        [Fact]
        public void CreateNotes_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetNotesById("1"))
                .Returns(new ANote
                {
                    IdEt = "1",
                    IdEns = "1",
                    CodeCl = "4GL2",
                    AnneeDeb = "2020",
                    CodeModule = "Dev",
                    Orale = 12,
                    Semestre = 1,
                    Dc1 = 16,
                    Dc2 = 18,
                    Ds = 12,
                    DateSaisie = DateTime.Now
                });
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.CreateNotes(new NoteCreateDto { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateNotes_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetNotesById("1"))
                .Returns(new ANote
                {
                    IdEt = "1",
                    IdEns = "1",
                    CodeCl = "4GL2",
                    AnneeDeb = "2020",
                    CodeModule = "Dev",
                    Orale = 12,
                    Semestre = 1,
                    Dc1 = 16,
                    Dc2 = 18,
                    Ds = 12,
                    DateSaisie = DateTime.Now
                });
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateNotes("1", new NoteUpdateDto { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateNotes_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetNotesById("0")).Returns(() => null);
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.UpdateNotes("0", new NoteUpdateDto { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialNotesUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetNotesById("0")).Returns(() => null);
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result =
                controller
                    .PartialNotesUpdate("0",
                        new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<NoteUpdateDto
                            >
                            { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteNotes_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            //Arrange
            _mockRepo
                .Setup(repo => repo.GetNotesById("1"))
                .Returns(new ANote
                {
                    IdEt = "1",
                    IdEns = "1",
                    CodeCl = "4GL2",
                    AnneeDeb = "2020",
                    CodeModule = "Dev",
                    Orale = 12,
                    Semestre = 1,
                    Dc1 = 16,
                    Dc2 = 18,
                    Ds = 12,
                    DateSaisie = DateTime.Now
                });
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteNotes("1");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteNotes_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetNotesById("0")).Returns(() => null);
            var controller = new NotesController(_mockRepo.Object, _mapper);

            //Act
            var result = controller.DeleteNotes("0");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}