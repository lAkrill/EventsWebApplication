using CSharpFunctionalExtensions;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EventsWebApplication.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateOnly Birthday { get; set; }
        public UserRole Role { get; set; } = UserRole.RegularUser;
        public List<Participant> Participants { get; set; } = new();
        public List<Event> Events { get; set; } = new();
    }
}
