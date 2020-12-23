
using Microsoft.EntityFrameworkCore;
using KanbanApi.Models;

namespace KanbanApi.Data
{
    public class KanbanDbContext : DbContext
    {
        public KanbanDbContext(DbContextOptions<KanbanDbContext> options)
         : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Kanban> Kanbans  { get; set; }
        public DbSet<KanbanColumn> KanbanColumns { get; set; }
        public DbSet<UserKanban> UserKanbans { get; set; }
        public DbSet<ColumnTask> ColumnTasks { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }
       

    }
}