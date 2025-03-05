using AutoMapper;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Core.Models;
using Moq;

namespace Tests
{
    public class EventServiceTest
    {
        private readonly Mock<IEventRepository> _mockRepo;
        private readonly Mock<IFileStorageService> _mockFileStorage;
        private readonly IMapper _mapper;

        public EventServiceTest()
        {
            _mockRepo = new Mock<IEventRepository>();
            _mockFileStorage = new Mock<IFileStorageService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventReadDto>();
                cfg.CreateMap<EventCreateDto, Event>();
                cfg.CreateMap<EventUpdateDto, Event>()
                    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
                cfg.CreateMap<Event, EventParticipantDto>()
                    .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Title))
                    .ForMember(dest => dest.ParticipantEventDtos, opts => opts.MapFrom(src => new List<ParticipantEventDto>()));
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetEventByIdAsync_EventExists_ReturnsMappedEvent()
        {
            var eventId = Guid.NewGuid();
            var ev = new Event
            {
                Id = eventId,
                Title = "Test Event",
                Description = "Test description",
                Date = DateTime.UtcNow,
                Location = "Test Location",
                MaxParticipants = 100
            };

            _mockRepo.Setup(repo => repo.GetEventByIdAsync(eventId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(ev);

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            var result = await service.GetEventByIdAsync(eventId);

            Assert.NotNull(result);
            Assert.Equal(ev.Id, result.Id);
            Assert.Equal(ev.Title, result.Title);
        }

        [Fact]
        public async Task GetEventByIdAsync_EventNotFound_ThrowsNotFoundException()
        {
            var eventId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetEventByIdAsync(eventId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Event)null);

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            await Assert.ThrowsAsync<NotFoundException>(() => service.GetEventByIdAsync(eventId));
        }

        [Fact]
        public async Task GetEventsByTitleAsync_ValidPagination_ReturnsListOfEvents()
        {
            var title = "Test";
            var events = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Title = "Test event", Description = "Desc", Date = DateTime.UtcNow, Location = "Loc", MaxParticipants = 50 },
                new Event { Id = Guid.NewGuid(), Title = "Another test", Description = "Desc2", Date = DateTime.UtcNow, Location = "Loc2", MaxParticipants = 100 }
            };

            _mockRepo.Setup(repo => repo.GetEventsByTitleAsync(title, 1, 20, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(events);

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            var result = await service.GetEventsByTitleAsync(title, 1, 20);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetEventsByTitleAsync_InvalidPagination_ThrowsInvalidPaginationException()
        {
            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            await Assert.ThrowsAsync<InvalidPaginationException>(() => service.GetEventsByTitleAsync("Test", 0, 20));
            await Assert.ThrowsAsync<InvalidPaginationException>(() => service.GetEventsByTitleAsync("Test", 1, 0));
        }

        [Fact]
        public async Task GetEventParticipantsByIdAsync_EventExists_ReturnsParticipantDto()
        {
            var eventId = Guid.NewGuid();
            var ev = new Event
            {
                Id = eventId,
                Title = "Test Event",
                Description = "Test description",
                Date = DateTime.UtcNow,
                Location = "Location",
                MaxParticipants = 100
            };

            _mockRepo.Setup(repo => repo.GetEventByIdAsync(eventId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(ev);

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            var result = await service.GetEventParticipantsByIdAsync(eventId);

            Assert.NotNull(result);
            Assert.Equal(ev.Title, result.Title);
        }

        [Fact]
        public async Task GetAllEventsAsync_InvalidPagination_ThrowsInvalidPaginationException()
        {
            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            await Assert.ThrowsAsync<InvalidPaginationException>(() => service.GetAllEventsAsync(0, 20));
            await Assert.ThrowsAsync<InvalidPaginationException>(() => service.GetAllEventsAsync(1, 0));
        }

        [Fact]
        public async Task CreateEventAsync_NewEvent_SavesAndReturnsEvent()
        {
            var createDto = new EventCreateDto
            {
                Title = "Unique Title",
                Description = "Description",
                Date = DateTime.UtcNow.AddDays(1),
                Location = "Some Location",
                MaxParticipants = 50,
                Image = null
            };

            _mockRepo.Setup(repo => repo.GetEventByTitleAsync(createDto.Title, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Event)null);

            _mockRepo.Setup(repo => repo.AddEventAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            var result = await service.CreateEventAsync(createDto);

            Assert.NotNull(result);
            Assert.Equal(createDto.Title, result.Title);
            _mockRepo.Verify(repo => repo.AddEventAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateEventAsync_EventAlreadyExists_ThrowsNotUniqueException()
        {
            var createDto = new EventCreateDto
            {
                Title = "Existing Title",
                Description = "Description",
                Date = DateTime.UtcNow.AddDays(1),
                Location = "Location",
                MaxParticipants = 50,
                Image = null
            };

            var existingEvent = new Event { Title = createDto.Title };
            _mockRepo.Setup(repo => repo.GetEventByTitleAsync(createDto.Title, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(existingEvent);

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            await Assert.ThrowsAsync<NotUniqueException>(() => service.CreateEventAsync(createDto));
        }

        [Fact]
        public async Task UpdateEventAsync_EventExists_UpdatesEventSuccessfully()
        {
            var updateDto = new EventUpdateDto
            {
                Id = Guid.NewGuid(),
                Title = "Updated Title",
                Description = "Updated description",
                Date = DateTime.UtcNow.AddDays(2),
                Location = "New Location",
                MaxParticipants = 100,
                RemoveImage = false,
                Image = null
            };

            var existingEvent = new Event
            {
                Id = updateDto.Id,
                Title = "Old Title",
                Description = "Old description",
                Date = DateTime.UtcNow.AddDays(1),
                Location = "Old Location",
                MaxParticipants = 50,
                ImagePath = "oldpath.jpg"
            };

            _mockRepo.Setup(repo => repo.GetEventByIdAsync(updateDto.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(existingEvent);

            _mockRepo.Setup(repo => repo.UpdateEventAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            var result = await service.UpdateEventAsync(updateDto);

            Assert.NotNull(result);
            Assert.Equal(updateDto.Title, result.Title);
            Assert.Equal(updateDto.Location, result.Location);
            _mockRepo.Verify(repo => repo.UpdateEventAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateEventAsync_EventNotFound_ThrowsNotFoundException()
        {
            var updateDto = new EventUpdateDto
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Desc",
                Date = DateTime.UtcNow.AddDays(1),
                Location = "Location",
                MaxParticipants = 50,
                RemoveImage = false,
                Image = null
            };

            _mockRepo.Setup(repo => repo.GetEventByIdAsync(updateDto.Id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Event)null);

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            await Assert.ThrowsAsync<NotFoundException>(() => service.UpdateEventAsync(updateDto));
        }

        [Fact]
        public async Task DeleteEventByIdAsync_EventExists_DeletesEvent()
        {
            var eventId = Guid.NewGuid();
            var existingEvent = new Event { Id = eventId, Title = "Event to Delete" };

            _mockRepo.Setup(repo => repo.GetEventByIdAsync(eventId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(existingEvent);

            _mockRepo.Setup(repo => repo.DeleteEventAsync(eventId, It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            await service.DeleteEventByIdAsync(eventId);

            _mockRepo.Verify(repo => repo.DeleteEventAsync(eventId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEventByIdAsync_EventNotFound_ThrowsNotFoundException()
        {
            var eventId = Guid.NewGuid();

            _mockRepo.Setup(repo => repo.GetEventByIdAsync(eventId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Event)null);

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteEventByIdAsync(eventId));
        }

        [Fact]
        public async Task GetEventsByCriteriaAsync_ValidCriteria_ReturnsMappedEvents()
        {
            var criteria = new EventFilterDto
            {
                Date = DateTime.UtcNow.Date,
                Location = "Location",
                CategoryId = null,
                SearchQuery = "Test",
                Page = 1,
                PageSize = 20
            };

            var events = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Title = "Test Event", Description = "Desc", Date = criteria.Date.Value, Location = "Location", MaxParticipants = 50 },
            };

            _mockRepo.Setup(repo => repo.GetEventsByCriteriaAsync(
                    criteria.Date,
                    criteria.Location,
                    criteria.CategoryId,
                    criteria.SearchQuery,
                    criteria.Page,
                    criteria.PageSize,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(events);

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            var result = await service.GetEventsByCriteriaAsync(criteria);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetEventsByCriteriaAsync_InvalidPagination_ThrowsInvalidPaginationException()
        {
            var criteria = new EventFilterDto
            {
                Date = null,
                Location = null,
                CategoryId = null,
                SearchQuery = null,
                Page = 0,
                PageSize = 0
            };

            var service = new EventService(_mockRepo.Object, _mapper, _mockFileStorage.Object);

            await Assert.ThrowsAsync<InvalidPaginationException>(() => service.GetEventsByCriteriaAsync(criteria));
        }
    }
}