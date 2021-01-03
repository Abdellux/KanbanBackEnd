using System.Threading.Tasks;
using KanbanApi.Data;
using KanbanApi.Models;
using KanbanApi.Dtos;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;



namespace KanbanApi.Sercices
{
    public class KanbanRepository : IKanbanRepository
    {
        private readonly KanbanDbContext _Context;
        private readonly IUserProvider _userProvider;
        public KanbanRepository(KanbanDbContext context, IUserProvider userProvider)
        {
            this._userProvider = userProvider;
            this._Context = context;
        }
        public async Task<ServiceResponse<KanbanDto>> Add(KanbanDto newKanban)
        {
            ServiceResponse<KanbanDto> serviceResponse = new ServiceResponse<KanbanDto>();

            // recupérer Id de l'utilisateur depuis le token
            var userId = _userProvider.GetUserId();
           
           //mapper kanbanDto à Kanban
            Kanban kanbanToAdd = new Kanban(newKanban.Name, newKanban.State, newKanban.ColumnsNumber);
            var kanban = await _Context.Kanbans.AddAsync(kanbanToAdd);
            await _Context.SaveChangesAsync();

            // ajouter un ligne dans la table UserKanban
            UserKanban userKanban = new UserKanban(userId, kanban.Entity.Id, "gestionnaire");
            await _Context.UserKanbans.AddAsync(userKanban);
            await _Context.SaveChangesAsync();

            //ajouter les colonnes stories et Terminés 
            await _Context.KanbanColumns.AddAsync(new KanbanColumn("Stories", kanban.Entity.Id));
            await _Context.KanbanColumns.AddAsync(new KanbanColumn("Terminés", kanban.Entity.Id));
            await _Context.SaveChangesAsync();

            serviceResponse.Data = new KanbanDto(kanban.Entity.Id, kanban.Entity.Name, kanban.Entity.State, kanban.Entity.ColumnsNumber);
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<KanbanDto>>> getAll()
        {
            var userId = _userProvider.GetUserId();
            
            var kanbans = await _Context.Kanbans
                        .FromSqlRaw("select* from kanbans where Id in (select KanbanId from UserKanbans where UserId = {0})", userId)
                        .ToListAsync();
            var KanbansDto = new List<KanbanDto>();
            foreach (var kanban in kanbans)
            {
                KanbansDto.Add(new KanbanDto(kanban.Id, kanban.Name, kanban.State, kanban.ColumnsNumber));
            }

            ServiceResponse<IEnumerable<KanbanDto>> serviceResponse = new ServiceResponse<IEnumerable<KanbanDto>>();
            serviceResponse.Data = KanbansDto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<KanbanDto>>> getPublicAll()
        {
            ServiceResponse<IEnumerable<KanbanDto>> serviceResponse = new ServiceResponse<IEnumerable<KanbanDto>>();

            var publicKanbans = await _Context.Kanbans.Where(Kanban => Kanban.State == "public")
                                                .ToListAsync();
            var KanbansDto = new List<KanbanDto>();
            foreach (var kanban in publicKanbans)
            {
                KanbansDto.Add(new KanbanDto(kanban.Id, kanban.Name, kanban.State, kanban.ColumnsNumber));
            }
            serviceResponse.Data = KanbansDto;

            return serviceResponse;
        }

        public ServiceResponse<IEnumerable<KanbanColumn>> getKanbanTasks( long kanbanId)
        {
            List<KanbanColumn> kanbanTasks = new List<KanbanColumn>();
            var kanbanColumns = _Context.KanbanColumns.Where(row => row.KanbanId == kanbanId).ToList();

            //récupérer les tâches de chaque colonne
            foreach (var item in kanbanColumns)
            {
                KanbanColumn kanbanColumn = new KanbanColumn();
                kanbanColumn = item;
                kanbanColumn.ColumnTasks = _Context.ColumnTasks.Where(row => row.KanbanColumnId == kanbanColumn.Id).ToList();
                kanbanTasks.Add(kanbanColumn);
            }

            ServiceResponse<IEnumerable<KanbanColumn>> serviceResponse = new ServiceResponse<IEnumerable<KanbanColumn>>();
            serviceResponse.Data = kanbanTasks;
            return serviceResponse;
        }

        public ServiceResponse<ColumnTaskDto> AddTask(ColumnTaskDto task)
        {
            ColumnTask newTask = new ColumnTask();
            newTask.Description = task.Description;
            newTask.affected = task.affected;
            newTask.DeadLine = task.DeadLine;
            newTask.KanbanColumnId = task.KanbanColumnId;

            var storedTask = _Context.ColumnTasks.Add(newTask);
            _Context.SaveChanges();

            task.Id = storedTask.Entity.Id;
            ServiceResponse<ColumnTaskDto> serviceResponse = new ServiceResponse<ColumnTaskDto>();
            serviceResponse.Data = task;
            return serviceResponse;
        }

        public ServiceResponse<KanbanColumnDto> AddColumn(KanbanColumnDto newColumn)
        {
            KanbanColumn kanbanColumn = new KanbanColumn();
            kanbanColumn.KanbanId = newColumn.KanbanId;
            kanbanColumn.Titre = newColumn.Titre;

            var storedColumn = _Context.KanbanColumns.Add(kanbanColumn);
            _Context.SaveChanges();
            newColumn.Id = storedColumn.Entity.Id;

            ServiceResponse<KanbanColumnDto> serviceResponse = new ServiceResponse<KanbanColumnDto>();
            serviceResponse.Data = newColumn;
            return serviceResponse;
        }
    }
}