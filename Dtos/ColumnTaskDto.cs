using System;

namespace KanbanApi.Dtos
{
    public class ColumnTaskDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool affected { get; set; } = false;
        public DateTime DeadLine { get; set; }
        public long KanbanColumnId { get; set; }
    }
}