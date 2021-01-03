using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KanbanApi.Security;
using KanbanApi.Models;
using KanbanApi.Sercices;
using Microsoft.AspNetCore.Authorization;
using System;

namespace KanbanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository _userRepository { get; }
        public IJwtAuthManager _jwtAuthManager { get; }
        public UserController(IUserRepository userRepository, IJwtAuthManager jwtAuthManager)
        {
            this._jwtAuthManager = jwtAuthManager;
            this._userRepository = userRepository;

        }
        [HttpGet]
        [Authorize]
        public async Task<ServiceResponse<IEnumerable<User>>> GetUsers()
        {
            return await _userRepository.GetAll();
        }

        // [HttpGet("{id}")]
        // [Authorize]
        // public async Task<ServiceResponse<User>> GetUser(long id)
        // {
        //     var serviceResponse = await _userRepository.GetUser(id);

        //     if (serviceResponse.Data == null)
        //     {
        //         serviceResponse.Status = false;
        //         serviceResponse.StatusText = "the resource does not exist";
        //     }

        //     return serviceResponse;
        // }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ServiceResponse<bool>> Register(LoginModel user)
        {
            return await _userRepository.Add(user);
        }


        [HttpPost("authentifiaction")]
        [AllowAnonymous]
        public ServiceResponse<string> Authentifiaction(LoginModel loginModel)
        {
            ServiceResponse<string> serviceResponse = new  ServiceResponse<string>();
            // authentifier l'utilisateur
            User user = _userRepository.Authenticate(loginModel);
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

        [HttpGet("checkvalidname/{name}")]
        [AllowAnonymous]
        public ServiceResponse<bool> checkValidName(string name)
        {
            ServiceResponse<bool> serviceResponse = new  ServiceResponse<bool>();
            if(String.IsNullOrEmpty(name))
            {
                serviceResponse.Data = false;
                return serviceResponse;
            }

            if(!_userRepository.IsExistname(name))
            {
                serviceResponse.Data = false;
                serviceResponse.StatusText = "Le username n'existe pas";
                return serviceResponse;
            }
            serviceResponse.Data = true;
            serviceResponse.StatusText = "le username existe";

            return serviceResponse;
        }
    }
}
