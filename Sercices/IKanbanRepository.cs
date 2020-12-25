using System.Collections.Generic;
using KanbanApi.Models;
using System.Threading.Tasks;

namespace KanbanApi.Sercices
{
    public interface IKanbanRepository
    {
         Task<ServiceResponse<IEnumerable<User>>> GetAll();
         Task<ServiceResponse<User>> GetUser(long Id);
         Task<ServiceResponse<bool>> Add(LoginModel loginModel);

        User Authenticate(LoginModel loginModel);
    }
}