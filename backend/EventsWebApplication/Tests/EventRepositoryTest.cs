using EventsWebApplication.Core.Models;
using EventsWebApplication.DataAccess;
using EventsWebApplication.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Tests.DataAccess
{
    

    public class EventRepositoryTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly EventRepository _repository;

        public EventRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            _repository = new EventRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetEventByIdAsync_EventExists_ReturnsEvent()
        {
            var ev = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Test Event",
                Description = "Description",
                Date = DateTime.UtcNow,
                Location = "Location",
                MaxParticipants = 100
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEventByIdAsync(ev.Id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(ev.Id, result.Id);
            Assert.Equal(ev.Title, result.Title);
        }

        [Fact]
        public async Task GetEventByTitleAsync_EventExists_ReturnsEvent()
        {
            var ev = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Unique Test Title",
                Description = "Description",
                Date = DateTime.UtcNow,
                Location = "Location",
                MaxParticipants = 100
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEventByTitleAsync(ev.Title, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(ev.Id, result.Id);
        }

        [Fact]
        public async Task GetEventsByTitleAsync_Filtered_ReturnsMatchingEvents()
        {
            var event1 = new Event
            {
                Id = Guid.NewGuid(),
                Title = "First Event",
                Description = "Desc",
                Date = DateTime.UtcNow,
                Location = "Location 1",
                MaxParticipants = 50
            };
            var event2 = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Second Event",
                Description = "Desc",
                Date = DateTime.UtcNow,
                Location = "Location 2",
                MaxParticipants = 60
            };
            _context.Events.AddRange(event1, event2);
            await _context.SaveChangesAsync();

            var result = await _repository.GetEventsByTitleAsync("Event", 1, 10, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
