using FluentValidation;

namespace EventsWebApplication.Application.Validators.Events
{
    public class EventCreateValidator : AbstractValidator<EventCreateDto>
    {
        public EventCreateValidator()
        {
            RuleFor(e => e.Title)
                .TitleOrName();

            RuleFor(e => e.Description)
                .EventDescription();

            RuleFor(e => e.Location)
                .EventLocation();

            RuleFor(e => e.MaxParticipants)
                .EventMaxParticipants();

            RuleFor(e => e.Date)
                .Must(BeAValidDate).WithMessage("Events should happen in the future");

            RuleFor(e => e.Image)
                .Must(i =>
                {
                    var extension = Path.GetExtension(i.FileName);
                    var extensions = new[] { ".jpg", ".jpeg", ".png" };
                    return extensions.Contains(extension);
                }).WithMessage("Image must be with one of the following extensions: .jpg, .jpeg, .png")
                .When(e => e.Image != null);
        }
        private bool BeAValidDate(DateTime date)
        {
            return date >= DateTime.UtcNow;
        }
    }
}
