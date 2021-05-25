using DocumentRepoAPI.Data.Entities;
using DocumentRepoAPI.Services.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEncryptionService encObj;
        public AuthenticationService(IEncryptionService encObj)
        {
            this.encObj = encObj;
        }
        public async Task<string> GenerateToken(long UserId)
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<string> Login(LoginDTO user)
        {
            using (var context = new filerepodbEntities())
            {
                //Encrypting password before checking
                user.PasswordHash = encObj.EncryptPassword(user.PasswordHash);
                //var u = await context.Users.FirstOrDefaultAsync(x => x.EmailId == user.EmailId && x.PasswordHash == EncryptPassword(user.PasswordHash));
                var u = await context.Users.FirstOrDefaultAsync(x => x.EmailId == user.EmailId && x.PasswordHash == user.PasswordHash);
                if (u != null)
                {
                    var tu = await context.UserActiveTokens.FirstOrDefaultAsync(x => x.UserId == u.UserId);
                    var token = "";
                    if (tu != null)//User token already exist
                    {
                        if (tu.Active == true)//token is valid
                        {
                            return tu.Token;
                        }
                        else
                        {
                            //token = await GenerateToken(u.UserId);
                            tu.Active = true;
                            await context.SaveChangesAsync();
                            return tu.Token;
                        }
                    }
                    else//Token don't exist with this user id
                    {
                        token = await GenerateToken(u.UserId);

                        //Adding tokens to UserActiveToken
                        context.UserActiveTokens.Add(new UserActiveTokens
                        {
                            UserId = u.UserId,
                            Token = token,
                            Active = true,
                            CreatedDate = DateTime.UtcNow
                        });
                        await context.SaveChangesAsync();

                        return token;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public Users GetUserFromToken(string token)
        {
            using (var context = new filerepodbEntities())
            {
                var t = context.UserActiveTokens.FirstOrDefault(x => x.Token == token);
                if (t != null)
                {
                    var u = context.Users.FirstOrDefault(x=>x.UserId == t.UserId);
                    return u;
                }
                return null;
            }
        }

        public async Task<long> LogOut(long userId)
        {
            using (var context = new filerepodbEntities())
            {
                var t = await context.UserActiveTokens.FirstOrDefaultAsync(x => x.UserId == userId);
                if (t != null)
                {
                    if (t.Active)
                    {
                        t.Active = false;
                        await context.SaveChangesAsync();
                        return t.UserId;
                    }
                    else
                    {
                        return -1;
                    }
                }
                return -1;

            }
        }
    }
}
