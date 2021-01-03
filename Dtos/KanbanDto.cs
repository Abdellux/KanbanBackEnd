namespace KanbanApi.Dtos
{
    public class KanbanDto
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public string State { get; set;} = "private";
        public int ColumnsNumber  { get; set; }

        public KanbanDto()
        {
        }
        public KanbanDto(long id, string name, string state, int columnNumber)
        {
            this.Id = id;
            this.Name = name;
            this.ColumnsNumber = columnNumber;
            this.State = state;
        }
    }
}