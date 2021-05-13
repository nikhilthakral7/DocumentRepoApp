using DocumentRepoAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Services.Interfaces
{
    public interface IUserService
    {
        //Get all Users
        List<Users> GetUsers();

        Users GetUsers(long id = 0);

        //Add Users
        Task<long> CreateUsers(Users newUsers);

        //Change Usersname/password
        Task<long>  UpdateUsers(long UsersId, Users updatedUsers);

        //Delete account - all files related to thata ccount deleted automatically
        // Takes UsersId of Users that needs to be deleted
        long DeleteUsers(long UsersId);

    }
}
