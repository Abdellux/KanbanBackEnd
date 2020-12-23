namespace KanbanApi.Security
{
    public interface IPassworHacher
    {
        string GetHashedPassword(string password);
        bool checkedPassword(string hashedPassword, string password);
    }
}