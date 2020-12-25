using System.Collections.Generic;

namespace KanbanApi.Models
{
    public class User
    {      
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<UserKanban> UserKanbans { get; set; }
        public IEnumerable<AssignedTask> AssignedTasks { get; set; }

        // public User(string userName, string password)
        // {
        //     this.Username = userName;
        //     this.Password = password;
        // }

    }
}