using Blazor_app.Models;

namespace Blazor_app.Interfaces
{
    public interface IUserService
    {
        // Hämtar en lista över användare (antingen från mock eller API)
        Task<IEnumerable<User>> GetUsers();

        // Söker efter användare baserat på sökterm
        Task<IEnumerable<User>> SearchUsers(string searchTerm);

        // Hämtar alla todos för specifik användare
        Task<IEnumerable<TodoItem>> GetUserTodos(int userId);

        // Lägger till en ny användare
        Task AddUserAsync(User newUser);
    }
}