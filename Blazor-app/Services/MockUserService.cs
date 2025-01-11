using System.Net.Http.Json;
using Blazor_app.Interfaces;
using Blazor_app.Models;

namespace Blazor_app.Services
{
    public class MockUserService : IUserService
    {
        private static List<User> _users = new()
{
    new User
    {
        Id = 1,
        Name = "Erik Svensson",
        Email = "erik@example.com",
        Address = new Address
        {
            Street = "Storgatan 1",
            City = "Stockholm",
            ZipCode = "12345"
        },
        Company = new Company
        {
            Name = "Tech AB",
            CatchPhrase = "Vi gör teknik enkelt"
        }
    },
    new User
    {
        Id = 2,
        Name = "Anna Lindberg",
        Email = "anna@example.com",
        Address = new Address
        {
            Street = "Kungsgatan 10",
            City = "Göteborg",
            ZipCode = "41111"
        },
        Company = new Company
        {
            Name = "Design Studio",
            CatchPhrase = "Kreativa lösningar"
        }
    },
    new User
    {
        Id = 3,
        Name = "Lars Nilsson",
        Email = "lars@example.com",
        Address = new Address
        {
            Street = "Järnvägsgatan 5",
            City = "Malmö",
            ZipCode = "21134"
        },
        Company = new Company
        {
            Name = "Bygg & Co",
            CatchPhrase = "Vi bygger framtiden"
        }
    },
    new User
    {
        Id = 4,
        Name = "Maria Karlsson",
        Email = "maria@example.com",
        Address = new Address
        {
            Street = "Drottninggatan 15",
            City = "Uppsala",
            ZipCode = "75320"
        },
        Company = new Company
        {
            Name = "IT Solutions",
            CatchPhrase = "Digitala lösningar för moderna behov"
        }
    },
    new User
    {
        Id = 5,
        Name = "Johan Berg",
        Email = "johan@example.com",
        Address = new Address
        {
            Street = "Hamngatan 8",
            City = "Sundsvall",
            ZipCode = "85234"
        },
        Company = new Company
        {
            Name = "Nordic Data",
            CatchPhrase = "Framtidens teknologi idag"
        }
    }
};
        public async Task<IEnumerable<User>> GetUsers()
        {
            await Task.Delay(500); // Simulate network delay
            return _users.Take(5); // retunera bara fem users
        }

        public async Task<IEnumerable<User>> SearchUsers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetUsers();

            return _users.Where(u =>
                u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );
        }

        public async Task<IEnumerable<TodoItem>> GetUserTodos(int userId)
        {
            // retunera mock data
            return new List<TodoItem>
            {
                new TodoItem { UserId = userId, Id = 1, Title = "Mock Todo 1", Completed = false },
                new TodoItem { UserId = userId, Id = 2, Title = "Mock Todo 2", Completed = true }
            };
        }

        public async Task AddUserAsync(User newUser)
        {
            newUser.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(newUser);
        }
    }
}