using DocumentRepoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Services.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        public byte[] EncryptFile(byte[] file)
        {
            throw new NotImplementedException();
        }

        public string EncryptPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(password));
                return new Guid(hash).ToString();
            }
        }
    }
}
