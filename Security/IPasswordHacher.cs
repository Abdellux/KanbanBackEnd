namespace KanbanApi.Security
{
    public interface IPasswordHacher
    {
        string GetHashedPassword(string password);
        bool checkedPassword(string hashedPassword, string password);
    }
}