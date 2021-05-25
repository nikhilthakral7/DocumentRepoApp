using DocumentRepoAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Services.Interfaces
{
    public interface IAuthenticationService
    {
        //login
        Task<string> Login(LoginDTO user);
        //{
        //  Also check for user active
        //  GenerateToken() on succesfull username password
        //}

        //genrate Token - Token will be string 
        Task<string> GenerateToken(long UserId);

        Users GetUserFromToken(string token);

        //logout
        Task<long> LogOut(long userId);
    }
}
