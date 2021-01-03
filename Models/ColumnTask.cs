using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KanbanApi.Models
{
    public class ColumnTask
    {
        [Key]
        public long Id { get; set; }
        public string Description { get; set; }
        public bool affected { get; set; } = false;
        public DateTime DeadLine { get; set; }
        public long KanbanColumnId { get; set; }
        public virtual KanbanColumn KanbanColumn { get; set; }
        public IEnumerable<AssignedTask> AssignedTasks { get; set; }
    }
}