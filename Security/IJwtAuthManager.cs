using KanbanApi.Models;
namespace KanbanApi.Security
{
    public interface IJwtAuthManager
    {
        string GenerateTokens(User user);
    }
}