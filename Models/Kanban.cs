using System.Collections.Generic;

namespace KanbanApi.Models
{
    public class Kanban
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        //private or public 
        public string State { get; set;} = "private";
        public int ColumnsNumber  { get; set; }
        public IEnumerable<UserKanban> UserKanbans { get; set; }
        public IEnumerable<KanbanColumn> KanbanColumns { get; set; }

        public Kanban(string name, string state, int columnsNumber)
        {
            this.Name = name;
            this.State = state;
            this.ColumnsNumber = columnsNumber;
        }

    }
}