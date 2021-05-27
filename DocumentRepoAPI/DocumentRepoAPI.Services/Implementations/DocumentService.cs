using DocumentRepoAPI.Data.Entities;
using DocumentRepoAPI.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public async Task<long> AddDirectory(long userId, Folders newFolder)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    context.Folders.Add(newFolder);
                    await context.SaveChangesAsync();
                    //If the new folder don't have any parent folder, create new folder in the root directory
                    if (newFolder.ParentId == null)
                    {
                        var rootPath = WebConfigurationManager.AppSettings["DocPath"] + $"\\{newFolder.FolderName}\\";
                        if (!Directory.Exists(rootPath))
                        {
                            Directory.CreateDirectory(rootPath);
                            Log.Information($"{newFolder.FolderName} folder is created at {rootPath} by {userId}");
                        }
                    }
                    //If new folder has parent folder, we need to create new folder inside that parent folder
                    else
                    {
                        var rootPath = WebConfigurationManager.AppSettings["DocPath"] + $"\\";
                        var dirs = Directory.GetDirectories(rootPath);
                        //Getting name of parent folder
                        var pFolderName = context.Folders.FirstOrDefault(f => f.FolderId == newFolder.ParentId);
                        var dirPath = dirs.FirstOrDefault(d => d.Equals(pFolderName));
                        dirPath += $"\\{newFolder.FolderName}";
                        if (dirPath != null)
                        {
                            if (!Directory.Exists(dirPath))
                            {
                                Directory.CreateDirectory(dirPath);
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }

                    return newFolder.FolderId;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }

        public string AddFile(long userId, string folderName, HttpPostedFile file)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    var rootPath = WebConfigurationManager.AppSettings["DocPath"] + $"\\{userId}\\{folderName}\\";
                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    file.SaveAs(rootPath + file.FileName);
                    context.Files.Add(new Files
                    {
                        FileName = file.FileName,
                        FileUrl = rootPath + file.FileName,
                        FileType = file.ContentType,
                        ReadAccess = true,
                        ModifyAccess = true,
                        DeleteAccess = true,
                        CreatedBy = userId,
                        ModifiedBy = userId

                    });
                    context.SaveChanges();
                    return file.FileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }

            }
        }

        public bool DeleteDirectory(long userId, string folderPath)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    //var rootPath = WebConfigurationManager.AppSettings["DocPath"] + $"\\{userId}\\{folderName}\\";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.Delete(folderPath, true);
                    }
                    Log.Information($"{folderPath} is deleted by {userId}");
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                    return false;
                }
            }
        }
        public async Task<bool> DeleteFile(long userId, long fileId)
        {
            using (var context = new filerepodbEntities())
            {
                var f = await context.Files.FirstOrDefaultAsync(x => x.FileId == fileId);
                if (f != null)
                {
                    try
                    {
                        Directory.Delete(f.FileUrl);
                        context.Files.Remove(f);
                        await context.SaveChangesAsync();
                        Log.Information($"{f.FileUrl} is deleted by {userId}");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"{ex.Message}");
                        Log.Error($"{ex.StackTrace}");
                        return false;
                    }
                }
            }
            return false;
        }

        public async Task GrantFilePermission(SharedFiles shFile)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    context.SharedFiles.Add(shFile);
                    await context.SaveChangesAsync();
                    Log.Information($"{shFile.SharedBy} granted Read: {shFile.ReadAccess}, Delete: {shFile.DeleteAccess}, Modify: {shFile.ModifyAccess} permission to {shFile.SharedTo} for {shFile.SharedFile} file");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                }

            }
        }

        public async Task GrantFolderPermission(SharedFolders shFolder)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    context.SharedFolders.Add(shFolder);
                    await context.SaveChangesAsync();
                    Log.Information($"{shFolder.SharedBy} granted Read: {shFolder.ReadAccess}, Delete: {shFolder.DeleteAccess}, Modify: {shFolder.ModifyAccess} permission to {shFolder.SharedTo} for {shFolder.SharedFolder} folder");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                }

            }
        }
        public bool ModifyDirectoryAccess(long fromUserId, long folderId, long[] toUserIds, long accessType)
        {
            throw new NotImplementedException();
        }

        public bool ModifyDirectoryAccess(long userId, long folderId, long accessType)
        {
            throw new NotImplementedException();
        }

        public bool ModifyFileAccess(long fromUserId, long fileId, long[] toUserIds, long accessType)
        {
            throw new NotImplementedException();
        }

        public bool ModifyFileAccess(long userId, long fileId, long accessType)
        {
            throw new NotImplementedException();
        }

        public void RevokeFilePermission(SharedFiles shFile)
        {
            throw new NotImplementedException();
        }

        public void RevokeFolderPermission(SharedFolders shFolder)
        {
            throw new NotImplementedException();
        }

        public long UpdateDirectory(long userId, long folderId, string updatedFolderName)
        {
            throw new NotImplementedException();
        }

        public long UpdateFile(long userId, long fileId, byte[] updatedFile)
        {
            throw new NotImplementedException();
        }
    }
}
