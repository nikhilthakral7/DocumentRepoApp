using DocumentRepoAPI.Data.Entities;
using DocumentRepoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace DocumentRepoAPI.Services.Implementations
{
    public class DocumentService : IDocumentService
    {
        public async Task<long> AddDirectory(Folders newFolder)
        {
            using (var context = new filerepodbEntities())
            {
                context.Folders.Add(newFolder);
                try
                {
                    await context.SaveChangesAsync();
                    return newFolder.FolderId;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }

        public string AddFile(long userId, HttpPostedFile file)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    var rootPath = WebConfigurationManager.AppSettings["DocPath"]+$"\\{userId}\\";
                    if (!Directory.Exists(rootPath)) {
                        Directory.CreateDirectory(rootPath);
                    }

                    file.SaveAs(rootPath + file.FileName);
                    context.Files.Add(new Files {
                        FileName = file.FileName,
                        FileUrl = rootPath + file.FileName,
                        FileType = "json",
                        ReadAccess = true,
                        ModifyAccess = true,
                        DeleteAccess = true,
                        CreatedBy = userId,
                        ModifiedBy = userId

                    });
                    context.SaveChanges();
                    return file.FileName;
                }
                catch(Exception ex) {
                    Console.WriteLine(ex);
                    return null;
                }
                
            }
        }

        public bool DeleteDirectory(int userId, int folderId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(int userId, int fileId)
        {
            throw new NotImplementedException();
        }

        public void GrantPermission(int byUserId, int toUserId, int permissionType, int fileId, int folderId)
        {
            throw new NotImplementedException();
        }

        public bool ModifyDirectoryAccess(int userId, int folderId, int[] userIds, int accessType)
        {
            throw new NotImplementedException();
        }

        public bool ModifyDirectoryAccess(int userId, int folderId, int accessType)
        {
            throw new NotImplementedException();
        }

        public bool ModifyFileAccess(int userId, int fileId, int[] userIds, int accessType)
        {
            throw new NotImplementedException();
        }

        public bool ModifyFileAccess(int userId, int fileId, int accessType)
        {
            throw new NotImplementedException();
        }

        public void RevokePermission(int byUserId, int toUserId, int permissionType, int fileId, int folderId)
        {
            throw new NotImplementedException();
        }

        public int UpdateDirectory(int userId, int folderId, string updatedFolderName)
        {
            throw new NotImplementedException();
        }

        public int UpdateFile(int userId, int fileId, byte[] updatedFile)
        {
            throw new NotImplementedException();
        }
    }
}
