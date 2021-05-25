using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Data.Entities
{
    public class UserDTO
    {
        public bool status { get; set; }
        public long userId { get; set; }
        public string userRole { get; set; }
        public string message { get; set; }
    }
}
