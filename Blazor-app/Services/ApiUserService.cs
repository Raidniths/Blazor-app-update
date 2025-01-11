using System.Net.Http.Json;
using Blazor_app.Interfaces;
using Blazor_app.Models;

namespace Blazor_app.Services
{
    public class ApiUserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private List<User> _cachedUsers = new();

        public ApiUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                if (!_cachedUsers.Any())
                {
                    var users = await _httpClient.GetFromJsonAsync<List<User>>("/users");
                    if (users != null)
                    {
                        _cachedUsers = users;
                    }
                }
                return _cachedUsers.Take(5); // Only return first 5 users
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> SearchUsers(string searchTerm)
        {
            var users = await GetUsers();
            if (string.IsNullOrWhiteSpace(searchTerm))
                return users;

            return users.Where(u =>
                u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );
        }

        public async Task<IEnumerable<TodoItem>> GetUserTodos(int userId)
        {
            try
            {
                var todos = await _httpClient.GetFromJsonAsync<List<TodoItem>>($"/todos?userId={userId}");
                return todos ?? new List<TodoItem>();
            }
            catch (Exception)
            {
                return new List<TodoItem>();
            }
        }

        public async Task AddUserAsync(User newUser)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/users", newUser);
                if (response.IsSuccessStatusCode)
                {
                    var addedUser = await response.Content.ReadFromJsonAsync<User>();
                    if (addedUser != null)
                    {
                        _cachedUsers.Add(addedUser);
                    }
                }
            }
            catch (Exception)
            {
                // Handle error appropriately
            }
        }
    }
}