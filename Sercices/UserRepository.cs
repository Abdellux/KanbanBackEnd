using System;
using System.Collections.Generic;
using KanbanApi.Data;
using KanbanApi.Models;
using KanbanApi.Security;
using System.Threading.Tasks;
using System.Linq;

namespace KanbanApi.Sercices
{
    public class UserRepository : IUserRepository
    {
        private readonly KanbanDbContext _content;
        private readonly IPasswordHacher _passworHacher;

        public UserRepository(KanbanDbContext content, IPasswordHacher passworHacher)
        {
            this._passworHacher = passworHacher;
            this._content = content;
        }
        public async Task<ServiceResponse<bool>> Add(LoginModel registerModel)
        {
            ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>();

            // vérifier que le username n'exite pas
            var user = _content.Users
                    .Where(user => user.Username == registerModel.Username)
                    .FirstOrDefault();

            if (user != null)
            {
                serviceResponse.Data = false;
                serviceResponse.Status = false;
                serviceResponse.StatusText = "le username exite";
                return serviceResponse;
            }

            // hacher le password de l'utilisateur
            registerModel.Password = _passworHacher.GetHashedPassword(registerModel.Password);
            User newUser = new User();
            newUser.Username = registerModel.Username;
            newUser.Password = registerModel.Password;

            _content.Users.Add(newUser);
            await _content.SaveChangesAsync();

            serviceResponse.Data = true;
            return serviceResponse;
        }
        public async Task<ServiceResponse<IEnumerable<User>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<User>>();

            serviceResponse.Data = _content.Users;
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
            if (userd == null)
            {
                return null;
            }
            // vérifier le mot de passe
            bool passwordIsValid = _passworHacher.checkedPassword(userd.Password, loginModel.Password);

            if (passwordIsValid == false)
            {
                return null;
            }

            return userd;

        }
        public bool IsExistname(string name)
        {
            var userd = _content.Users
                    .Where(u => u.Username == name)
                    .FirstOrDefault();
            if (userd == null)
            {
                return false;
            }
            return true;
        }
    }
}