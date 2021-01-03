using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanApi.Dtos;
using KanbanApi.Models;
using KanbanApi.Sercices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KanbanController : ControllerBase
    {
        private readonly IKanbanRepository _kanbanRepository;
        public KanbanController(IKanbanRepository kanbanRepository)
        {
            this._kanbanRepository = kanbanRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ServiceResponse<KanbanDto>> PostKanban(KanbanDto newKanban)
        {
           return await _kanbanRepository.Add(newKanban);
        }

        [HttpPost("{kanbanId}/kanbancolumn")]
        [Authorize]
        public ServiceResponse<KanbanColumnDto> PostColumn(long kanbanId, KanbanColumnDto newColumn)
        {
            return _kanbanRepository.AddColumn(newColumn);
        }

        [HttpGet]
        [Authorize]
        public  Task<ServiceResponse<IEnumerable<KanbanDto>>> getKanbans()
        {
           return  _kanbanRepository.getAll();
        }

        [HttpGet("PublicKanbans")]
        [AllowAnonymous]
        public async Task<ServiceResponse<IEnumerable<KanbanDto>>> getPublicKanbans()
        {
           return await _kanbanRepository.getPublicAll();
        }

        [HttpGet("tasks/{kanbanId}")]
        [Authorize]
        public ServiceResponse<IEnumerable<KanbanColumn>> getTasks(long kanbanId)
        {
            return _kanbanRepository.getKanbanTasks(kanbanId);
        }

        [HttpPost("task")]
        [Authorize]
        public ServiceResponse<ColumnTaskDto> PostTask(ColumnTaskDto task)
        {
            return _kanbanRepository.AddTask(task);
        }
    }
}