using System.Collections.Generic;
using KanbanApi.Models;
using System.Threading.Tasks;

namespace KanbanApi.Sercices
{
    public interface IKanbanRepository
    {
         Task<ServiceResponse<IEnumerable<User>>> GetAll();
         Task<ServiceResponse<User>> GetUser(long Id);
         Task<ServiceResponse<User>> Add(User NewUser);

        User Authenticate(LoginModel loginModel);
    }
}