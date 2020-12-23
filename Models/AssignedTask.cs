namespace KanbanApi.Models
{
    public class AssignedTask
    {
        public long Id { get; set; }    
         // gestionnaire, invite
        public string Status { get; set; }
        public long UserId  { get; set; }
        public virtual User User { get; set; }
        public long ColumnTaskId { get; set; }
        public virtual ColumnTask ColumnTask { get; set; }
    }
}