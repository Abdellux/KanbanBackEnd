using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace KanbanApi.Sercices
{
     public class UserProvider : IUserProvider
    {
         private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProvider (IHttpContextAccessor httpContextAccessor)
        {
             _httpContextAccessor = httpContextAccessor;
        }

        public long GetUserId()
        {
            
             var userdId =  _httpContextAccessor.HttpContext.User.Claims
                .First(i => i.Type == ClaimTypes.NameIdentifier).Value;
                
            return Convert.ToInt64(userdId);
            
        }
    }
}