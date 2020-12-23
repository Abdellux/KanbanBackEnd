using System;
using System.Collections.Generic;
using KanbanApi.Data;
using KanbanApi.Models;
using KanbanApi.Security;
using System.Threading.Tasks;
using System.Linq;

namespace KanbanApi.Sercices
{
    public class KanbanRepository : IKanbanRepository
    {
        public KanbanDbContext _content { get; }
        public IPassworHacher _passworHacher { get; }

        public KanbanRepository(KanbanDbContext context, IPassworHacher passworHacher)
        {
            this._passworHacher = passworHacher;
            this._content = context;

        }
        public async Task<ServiceResponse<User>> Add(User newUser)
        {
            ServiceResponse<User> serviceResponse = new ServiceResponse<User>();
            
            // vérifier que le username n'exite pas
            var user = _content.Users
                    .Where( user => user.Username == newUser.Username)
                    .FirstOrDefault();

            if( user != null)
            {
                serviceResponse.Data = null;
                serviceResponse.Status = false;
                serviceResponse.StatusText = " le username existe déjà";
                return serviceResponse;
            }

            // hacher le password de l'utilisateur
            newUser.Password = _passworHacher.GetHashedPassword(newUser.Password);
            _content.Users.Add(newUser);
            await _content.SaveChangesAsync();

            serviceResponse.Data = newUser;
            return serviceResponse;
        }

        
        public async Task<ServiceResponse<IEnumerable<User>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<User>>();

            serviceResponse.Data =  _content.Users;
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> GetUser(long Id)
        {
            var serviceResponse = new ServiceResponse<User>();
            serviceResponse.Data = await _content.Users.FindAsync(Id);
            return serviceResponse;
        }
        public User Authenticate(LoginModel loginModel)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();
            // vérifier que le username existe
            var userd = _content.Users
                    .Where(u => u.Username == loginModel.Username)
                    .FirstOrDefault();
            if( userd == null)
            {
               return null;
            }
            // vérifier le mot de passe
            bool passwordIsValid =_passworHacher.checkedPassword(userd.Password, loginModel.Password);

            if(passwordIsValid == false)
            {
                return null;
            }
           
            return userd;

        }
    }
}