using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KanbanApi.Security;
using KanbanApi.Models;
using KanbanApi.Sercices;
using Microsoft.AspNetCore.Authorization;

namespace KanbanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IKanbanRepository _kanbanRepository { get; }
        public IJwtAuthManager _jwtAuthManager { get; }
        public UserController(IKanbanRepository kanbanRepository, IJwtAuthManager jwtAuthManager)
        {
            this._jwtAuthManager = jwtAuthManager;
            this._kanbanRepository = kanbanRepository;

        }
        [HttpGet]
        [Authorize]
        public async Task<ServiceResponse<IEnumerable<User>>> GetUsers()
        {
            return await _kanbanRepository.GetAll();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ServiceResponse<User>> GetUser(long id)
        {
            var serviceResponse = await _kanbanRepository.GetUser(id);

            if (serviceResponse.Data == null)
            {
                serviceResponse.Status = false;
                serviceResponse.StatusText = "the resource does not exist";
            }

            return serviceResponse;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ServiceResponse<bool>> Register(LoginModel user)
        {
            return await _kanbanRepository.Add(user);
        }


        [HttpPost("authentifiaction")]
        [AllowAnonymous]
        public ServiceResponse<string> Authentifiaction(LoginModel loginModel)
        {
            ServiceResponse<string> serviceResponse = new  ServiceResponse<string>();
            // authentifier l'utilisateur
            User user = _kanbanRepository.Authenticate(loginModel);
            if(user == null)
            {
                serviceResponse.Status = false;
                serviceResponse.StatusText = "username ou mot de passe incorrect";
                return serviceResponse;
            }
           
           // générer le token avec jwt
            string tokens = _jwtAuthManager.GenerateTokens(user);
            serviceResponse.Data = tokens;
            return serviceResponse;
        }

    }
}
