using FluentValidation;

namespace EventsWebApplication.Application.Validators
{
    public class EventCreateValidator : AbstractValidator<EventCreateDto>
    {
        public EventCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Максимальная длина названия 100 символов");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание обязательно")
                .MaximumLength(1000).WithMessage("Максимальная длина описания 1000 символов");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Дата обязательна")
                .GreaterThan(DateTime.Now).WithMessage("Дата должна быть в будущем");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Место проведения обязательно")
                .MaximumLength(200).WithMessage("Максимальная длина места проведения 200 символов");

            RuleFor(x => x.MaxParticipants)
                .GreaterThan(0).WithMessage("Максимальное количество участников должно быть больше 0");
        }
    }

    public class EventUpdateValidator : AbstractValidator<EventUpdateDto>
    {
        public EventUpdateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Максимальная длина названия 100 символов");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание обязательно")
                .MaximumLength(1000).WithMessage("Максимальная длина описания 1000 символов");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Дата обязательна")
                .GreaterThan(DateTime.Now).WithMessage("Дата должна быть в будущем");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Место проведения обязательно")
                .MaximumLength(200).WithMessage("Максимальная длина места проведения 200 символов");

            RuleFor(x => x.MaxParticipants)
                .GreaterThan(0).WithMessage("Максимальное количество участников должно быть больше 0");
        }
    }
}
