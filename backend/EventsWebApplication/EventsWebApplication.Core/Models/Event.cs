using CSharpFunctionalExtensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventsWebApplication.Core.Models
{
    public class Event
    {
        public const int MAX_TITLE_LENGTH = 100;
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public string Location { get; private set; }
        public Guid CategoryId { get; private set; }
        public int MaxParticipants { get; private set; }
        public string? ImagePath { get; private set; }
        public Guid UserId { get; private set; }
        private readonly List<Participant> _participants = new();
        public IReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();

        private Event()
        {
            
        }

        private Event(Guid id, string title, string description, DateTime date, string location, Guid categoryId, Guid userId, int maxParticipants, string? imagePath)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            Location = location;
            CategoryId = categoryId;
            MaxParticipants = maxParticipants;
            UserId = userId;
            ImagePath = imagePath;
        }

        public static Result<Event> Create(
            string title,
            string description,
            DateTime date,
            string location,
            Guid categoryId,
            Guid userId,
            int maxParticipants,
            string? imagePath = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result.Failure<Event>("Название события обязательно.");

            if (title.Length > MAX_TITLE_LENGTH)
                return Result.Failure<Event>("Название события не должно быть больше 100 символов");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Event>("Описание события обязательно.");

            if (string.IsNullOrWhiteSpace(location))
                return Result.Failure<Event>("Место проведения события обязательно.");

            if (categoryId == Guid.Empty)
                return Result.Failure<Event>("ID категории не должно быть пустым");
            
            if (userId == Guid.Empty)
                return Result.Failure<Event>("ID пользователя не должно быть пустым");

            if (date < DateTime.UtcNow)
                return Result.Failure<Event>("Дата события должна быть в будущем.");

            if (maxParticipants <= 0)
                return Result.Failure<Event>("Максимальное количество участников должно быть больше нуля.");

            return Result.Success(new Event(Guid.NewGuid(), title, description, date, location, categoryId, userId, maxParticipants, imagePath));
        }

        public Result AddParticipant(Guid userId)
        {
            if (userId == Guid.Empty)
                return Result.Failure("ID пользователя не должно быть пустым");
            if (_participants.Any(p => p.UserId == userId))
                return Result.Failure("Пользователь уже зарегистрирован.");
            if (_participants.Count >= MaxParticipants)
                return Result.Failure("Событие полностью заполнено.");

            var participant = Participant.Create(userId, Id);
            _participants.Add(participant.Value);
            return Result.Success();
        }

        public Result RemoveParticipant(Guid userId)
        {
            var participant = _participants.FirstOrDefault(p => p.UserId == userId);

            if (participant is null)
                return Result.Failure("Пользователь не зарегистрирован на это событие.");

            _participants.Remove(participant);
            return Result.Success();
        }

        public Result Update(
            string title,
            string description,
            DateTime date,
            string location,
            Guid categoryId,
            int maxParticipants,
            string? imagePath = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Result.Failure("Название события обязательно.");

            if (title.Length > MAX_TITLE_LENGTH)
                return Result.Failure("Название события не должно быть больше 100 символов");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure("Описание события обязательно.");

            if (string.IsNullOrWhiteSpace(location))
                return Result.Failure("Место проведения события обязательно.");

            if (categoryId == Guid.Empty)
                return Result.Failure("ID категории не должно быть пустым");

            if (date < DateTime.UtcNow)
                return Result.Failure("Дата события должна быть в будущем.");

            if (maxParticipants <= 0)
                return Result.Failure("Максимальное количество участников должно быть больше нуля.");
            Title = title;
            Description = description;
            Date = date;
            Location = location;
            CategoryId = categoryId;
            MaxParticipants = maxParticipants;

            return Result.Success();

            // Генерация доменного события
            //AddDomainEvent(new EventUpdatedDomainEvent(Id));
        }
    }
}
