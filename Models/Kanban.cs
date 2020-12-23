using System.Collections.Generic;

namespace KanbanApi.Models
{
    public class Kanban
    {
        public long Id { get; set; }   
        //private or public 
        public string state { get; set;} = "private";
        public int ColumnsNumber  { get; set; }
        public IEnumerable<UserKanban> UserKanbans { get; set; }
        public IEnumerable<KanbanColumn> KanbanColumns { get; set; }

    }
}