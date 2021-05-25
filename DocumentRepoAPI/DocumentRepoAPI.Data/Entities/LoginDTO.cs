using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Data.Entities
{
    public class LoginDTO
    {
        public string EmailId { get; set; }
        public string PasswordHash { get; set; }
    }
}
