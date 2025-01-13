using System.Net.Http.Json;
using Blazor_app.Interfaces;
using Blazor_app.Models;

namespace Blazor_app.Services
{
    public class ApiUserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private List<User> _cachedUsers = new(); // Håller en lokal kopia av användare för att minska anrop till api

        public ApiUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        }

        // Hämtar användare från API:et eller från cachen
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                // Om vi inte har några cachade användare, hämta från API:et

                if (!_cachedUsers.Any())
                {
                    var users = await _httpClient.GetFromJsonAsync<List<User>>("/users");
                    if (users != null)
                    {
                        _cachedUsers = users;
                    }
                }
                return _cachedUsers.Take(5); // retunera bara 5 användare
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

        // Hämtar todos från api
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

        // Lägger till en ny användare till api
        public async Task AddUserAsync(User newUser)
        {
            try
            {
                // Skicka den nya användare till api
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
                // Om något går fel vid sparande
            }
        }
    }
}