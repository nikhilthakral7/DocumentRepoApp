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
        private readonly string docPath;
        //How to write fucntion to get file structure for a user for get request
        public DocumentService(string docPath)
        {
            this.docPath = docPath;
        }

        public async Task<byte[]> DownloadFilesByUser(long fileId)
        {
            using (var context = new filerepodbEntities())
            {
                var f = context.Files.Where(x => x.FileId == fileId);
                foreach (var file in f)
                {
                    return File.ReadAllBytes(file.FileUrl);
                }
            }
            return new byte[] { };
        }

        public async Task<List<Files>> GetFilesByUser(long userId)
        {
            using (var context = new filerepodbEntities())
            {
                return await context.Files.Where(x => x.CreatedBy == userId).ToListAsync();
            }
        }
        public async Task<List<Folders>> GetFolderByUser(long userId)
        {
            using (var context = new filerepodbEntities())
            {
                return await context.Folders.Where(x => x.CreatedBy == userId).ToListAsync();
            }
        }
        public async Task<long> AddDirectory(Folders newFolder)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    //If the new folder don't have any parent folder, create new folder in the root directory
                    if (newFolder.ParentId == null)
                    {
                        var rootPath = docPath + $"\\{newFolder.CreatedBy}\\{newFolder.FolderName}";
                        newFolder.FolderUrl = rootPath;

                        if (!Directory.Exists(rootPath))
                        {
                            Directory.CreateDirectory(rootPath);
                            Log.Information($"{newFolder.FolderName} folder is created at {rootPath} by {newFolder.CreatedBy}");
                            newFolder.CreateDate = newFolder.ModifiedDate = DateTime.UtcNow;

                            newFolder.ReadAccess = true;
                            newFolder.ModifyAccess = true;
                            newFolder.DeleteAccess = true;
                            newFolder.CreateDate = DateTime.UtcNow;
                            newFolder.ModifiedDate = DateTime.UtcNow;

                            context.Folders.Add(newFolder);
                            await context.SaveChangesAsync();
                            return newFolder.FolderId;
                        }
                        return -1;
                    }
                    //If new folder has parent folder, we need to create new folder inside that parent folder
                    else
                    {
                        var rootPath = docPath + $"\\";
                        var dirs = Directory.GetDirectories(rootPath);
                        //Getting name of parent folder
                        var pFolderName = context.Folders.FirstOrDefault(f => f.FolderId == newFolder.ParentId);
                        var dirPath = dirs.FirstOrDefault(d => d.Equals(pFolderName));
                        if (dirPath != null)
                        {
                            dirPath += $"\\{newFolder.CreatedBy}\\{newFolder.FolderName}";
                            if (!Directory.Exists(dirPath))
                            {
                                newFolder.FolderUrl = dirPath;
                                Directory.CreateDirectory(dirPath);

                                newFolder.ReadAccess = true;
                                newFolder.ModifyAccess = true;
                                newFolder.DeleteAccess = true;
                                newFolder.CreateDate = DateTime.UtcNow;
                                newFolder.ModifiedDate = DateTime.UtcNow;

                                context.Folders.Add(newFolder);
                                await context.SaveChangesAsync();
                                return newFolder.FolderId;
                            }
                        }
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }

        public async Task<bool> AddFile(long userId, string folderName, HttpPostedFile file)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    var rootPath = docPath + $"\\{userId}\\{folderName}\\";
                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    file.SaveAs(rootPath + file.FileName);
                    context.Files.Add(new Files
                    {
                        FileName = file.FileName,
                        //FolderId = folderName,
                        FileUrl = rootPath + file.FileName,
                        FileType = file.ContentType,
                        ReadAccess = true,
                        ModifyAccess = true,
                        DeleteAccess = true,
                        CreatedBy = userId,
                        CreateDate = DateTime.UtcNow,
                        ModifiedBy = userId,
                        ModifiedDate = DateTime.UtcNow

                    }); ;
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }

            }
        }

        public async Task<bool> DeleteDirectory(long userId, long folderId)
        {
            using (var context = new filerepodbEntities())
            {
                var f = await context.Folders.FirstOrDefaultAsync(x => x.FolderId == folderId);
                if (f != null)
                {
                    var folderPath = f.FolderUrl;
                    try
                    {
                        if (Directory.Exists(folderPath))
                        {
                            Directory.Delete(folderPath, true);
                        }
                        Log.Information($"{folderPath} is deleted by {userId}");
                        context.Folders.Remove(f);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        Log.Error(ex.StackTrace);
                        return false;
                    }
                }
                return false;
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
                        //Directory.Delete(Path.GetFileNameWithoutExtension(f.FileUrl));
                        //need path without file extension
                        File.Delete(f.FileUrl);
                        //Directory.Delete(f.FileUrl);
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

        public async Task<List<SharedFiles>> GetFilePermission(long userId)
        {
            using (var context = new filerepodbEntities())
            {
                return await context.SharedFiles.Where(x => x.SharedTo == userId).ToListAsync();
            }

        }

        public async Task<List<SharedFolders>> GetFolderPermission(long userId)
        {
            using (var context = new filerepodbEntities())
            {
                return await context.SharedFolders.Where(x => x.SharedTo == userId).ToListAsync();
            }

        }


        public async Task<bool> GrantFilePermission(SharedFiles shFile)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    context.SharedFiles.Add(shFile);
                    await context.SaveChangesAsync();
                    Log.Information($"{shFile.SharedBy} granted Read: {shFile.ReadAccess}, Delete: {shFile.DeleteAccess}, Modify: {shFile.ModifyAccess} permission to {shFile.SharedTo} for {shFile.SharedFile} file");
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

        public async Task<bool> GrantFolderPermission(SharedFolders shFile)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    context.SharedFolders.Add(shFile);
                    await context.SaveChangesAsync();
                    Log.Information($"{shFile.SharedBy} granted Read: {shFile.ReadAccess}, Delete: {shFile.DeleteAccess}, Modify: {shFile.ModifyAccess} permission to {shFile.SharedTo} for {shFile.SharedFolder} folder");
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
        public async Task<bool> ModifyDirectoryAccess(SharedFolders shFile)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    var folder = await context.SharedFolders.FirstOrDefaultAsync(f => f.SharedFoldersId == shFile.SharedFoldersId);
                    if (folder != null)
                    {
                        folder.ReadAccess = shFile.ReadAccess;
                        folder.DeleteAccess = shFile.DeleteAccess;
                        folder.ModifyAccess = shFile.ModifyAccess;
                        folder.ModifiedBy = shFile.ModifiedBy;
                        folder.ModifiedDate = DateTime.UtcNow;
                        await context.SaveChangesAsync();
                        Log.Information($"{shFile.SharedBy} changed Read: {shFile.ReadAccess}, Delete: {shFile.DeleteAccess}, Modify: {shFile.ModifyAccess} permission to {shFile.SharedTo} for {shFile.SharedFolder} folder");
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                    return false;
                }

            }
        }


        public async Task<bool> ModifyFileAccess(SharedFiles shFile)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    var file = await context.SharedFiles.FirstOrDefaultAsync(f => f.SharedFilesId == shFile.SharedFilesId);
                    if (file != null)
                    {
                        file.ReadAccess = shFile.ReadAccess;
                        file.DeleteAccess = shFile.DeleteAccess;
                        file.ModifyAccess = shFile.ModifyAccess;
                        file.ModifiedBy = shFile.ModifiedBy;
                        file.ModifiedDate = DateTime.UtcNow;
                        await context.SaveChangesAsync();
                        Log.Information($"{shFile.SharedBy} changed Read: {shFile.ReadAccess}, Delete: {shFile.DeleteAccess}, Modify: {shFile.ModifyAccess} permission to {shFile.SharedTo} for {shFile.SharedFile} file");
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                    return false;
                }

            }
        }

        public async Task<long> UpdateDirectory(long userId, Folders updatedFolder)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    var f = await context.Folders.FirstOrDefaultAsync(x => x.FolderId == updatedFolder.FolderId);
                    if (f != null)
                    {
                        if (f.FolderName != updatedFolder.FolderName)
                        {
                            var oldPath = f.FolderUrl;
                            var pathSplitList = f.FolderUrl.Split('\\');
                            var x = pathSplitList[pathSplitList.Length - 1];
                            pathSplitList[pathSplitList.Length - 1] = pathSplitList[pathSplitList.Length - 1].Replace(f.FolderName, updatedFolder.FolderName);
                            var newPath = String.Join("\\", pathSplitList);

                            Directory.Move(oldPath, newPath);

                            f.FolderName = updatedFolder.FolderName;
                            f.FolderUrl = newPath;
                            f.ModifiedBy = userId;
                            f.ModifiedDate = DateTime.UtcNow;
                            await context.SaveChangesAsync();
                        }
                        return f.FolderId;
                        Log.Information($"{f.FolderId} updated by {userId}");
                    }
                    return -1;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                    return -1;
                }

            }
        }

        //TODO
        public async Task<long> UpdateFile(long userId, long fileId, HttpPostedFile file)
        {
            using (var context = new filerepodbEntities())
            {
                try
                {
                    var f = await context.Files.FirstOrDefaultAsync(x => x.FileId == fileId);
                    if (f != null)
                    {
                        file.SaveAs(f.FileUrl);
                        f.ModifiedBy = userId;
                        f.ModifiedDate = DateTime.UtcNow;
                        await context.SaveChangesAsync();
                        return fileId;
                    }
                    return -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return -1;
                }

            }
        }
    }
}
