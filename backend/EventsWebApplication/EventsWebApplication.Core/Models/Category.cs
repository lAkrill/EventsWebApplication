using CSharpFunctionalExtensions;

namespace EventsWebApplication.Core.Models
{
    public class Category 
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }

        private Category()
        {
            
        }
        private Category(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

        public static Result<Category> Create(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Result.Failure<Category>("Название категории не может быть пустым");

            return Result.Success(new Category(Guid.NewGuid(), title));
        }

        public Result Update(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Result.Failure<Category>("Название категории не может быть пустым");

            Title = title;

            return Result.Success();
        }
    }
}
