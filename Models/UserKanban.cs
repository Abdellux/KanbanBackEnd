
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanApi.Models
{
    public class UserKanban
    {
        [Key]       
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long KanbanId { get; set; }
        public virtual Kanban Kanban { get; set; }
        // gestionnaire, invite
        public string Status { get; set; }

    }
}