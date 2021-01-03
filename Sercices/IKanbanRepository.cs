using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanApi.Dtos;
using KanbanApi.Models;

namespace KanbanApi.Sercices
{
    public interface IKanbanRepository
    {
        Task<ServiceResponse<KanbanDto>> Add(KanbanDto newKanban);
        ServiceResponse<KanbanColumnDto> AddColumn(KanbanColumnDto newColumn);
        ServiceResponse<ColumnTaskDto> AddTask(ColumnTaskDto Task);
        Task<ServiceResponse<IEnumerable<KanbanDto>>> getAll();
        Task<ServiceResponse<IEnumerable<KanbanDto>>> getPublicAll();
        ServiceResponse<IEnumerable<KanbanColumn>> getKanbanTasks( long kanbanId);
    }
}