using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KanbanApi.Models
{
    public class KanbanColumn
    {
        [Key]
        public long Id { get; set; }
        public string Titre { get; set; }
        public long KanbanId  { get; set; }
        public virtual Kanban Kanban { get; set; }
        public IEnumerable<ColumnTask> ColumnTasks { get; set; }
    }
}