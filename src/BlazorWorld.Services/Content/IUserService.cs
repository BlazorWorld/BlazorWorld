using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Services.Content
{
    public interface IUserService
    {
        Task<string> GetUserNameAsync(string appUserId);
        Task<string> GetUserIdAsync(string username);
        Task<string> GetAvatarHashAsync(string appUserId);
    }
}
