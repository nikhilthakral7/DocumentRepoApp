using DocumentRepoAPI.Data.DTO;
using DocumentRepoAPI.Data.Entities;
using DocumentRepoAPI.Services.Interfaces;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

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

        public UserDTO ValidateToken(string token)
        {
            var conn = WebConfigurationManager.ConnectionStrings["documentrepoconnectionstring"].ToString();
            //Establishing connection
            using (var SQLConnection = new SqlConnection(conn))
            {
                using (var sqlcomm = new SqlCommand("dbo.ValidateTokenRole", SQLConnection))
                {
                    sqlcomm.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlcomm.Parameters.AddWithValue("@token", token);

                    DataSet outputdata = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlcomm);
                    adapter.Fill(outputdata);
                    return new UserDTO
                    {
                        status = Convert.ToBoolean(outputdata.Tables[0].Rows[0][0]),
                        message = Convert.ToString(outputdata.Tables[0].Rows[0][1]),
                        userId = Convert.ToInt64(outputdata.Tables[0].Rows[0][2]),
                        userRole = Convert.ToString(outputdata.Tables[0].Rows[0][3])
                    };
                }
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
