using DocumentRepoAPI.Data.Entities;
using DocumentRepoAPI.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IEncryptionService encObj;
        public UserService(IEncryptionService encObj)
        {
            this.encObj = encObj;
        }
        public List<Users> GetUsers()
        {
            using (var context = new filerepodbEntities())
            {
                return context.Users.ToList();
            }
        }
        public Users GetUsers(long id)
        {
            using (var context = new filerepodbEntities())
            {
                return context.Users.FirstOrDefault(r=>r.UserId == id);
            }
        }

        public async Task<long> CreateUsers(Users newUsers)
        {
            using (var context = new filerepodbEntities())
            {
                //Encrypting password
                newUsers.PasswordHash = encObj.EncryptPassword(newUsers.PasswordHash);

                //Adding data with encrypted password
                context.Users.Add(newUsers);
                try
                {
                    await context.SaveChangesAsync();
                    Log.Information("User created successfully!");
                    return newUsers.UserId;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                    Log.Information(ex.StackTrace);
                    Log.Warning("{@newUsers}",newUsers);

                    return -1;
                }
            }
        }


        public async Task<long> DeleteUsers(long UsersId)
        {
            using (var context = new filerepodbEntities())
            {
                var entity = context.Users.FirstOrDefault(r => r.UserId == UsersId);
                if (entity != null)
                {
                    context.Users.Remove(entity);
                    await context.SaveChangesAsync();
                    return UsersId;
                }
                else
                {
                    return -1;
                }
            }
        }

        public async Task<long> UpdateUsers(long UsersId, Users updatedUsers)
        {
            using (var context = new filerepodbEntities())
            {
                var entity = context.Users.FirstOrDefault(r => r.UserId == UsersId);
                if (entity != null)
                {
                    entity.EmailId = updatedUsers.EmailId;
                    entity.PasswordHash = updatedUsers.PasswordHash;
                    entity.UserTypeId = updatedUsers.UserTypeId;
                    entity.UserActive = updatedUsers.UserActive;
                    entity.FirstName = updatedUsers.FirstName;
                    entity.LastName = updatedUsers.LastName;
                    entity.RecoveryPhoneNum = updatedUsers.RecoveryPhoneNum;
                    entity.ModifiedBy = updatedUsers.ModifiedBy;
                    entity.ModifiedDate = updatedUsers.ModifiedDate;

                    await context.SaveChangesAsync();
                    return entity.UserId;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
