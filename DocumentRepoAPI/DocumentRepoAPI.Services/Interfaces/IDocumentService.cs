using DocumentRepoAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace DocumentRepoAPI.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<byte[]> DownloadFilesByUser(long fileId);
        Task<long> AddDirectory(Folders newFolder);
        Task<bool> AddFile(long userId, string folderName, HttpPostedFile file);
        Task<bool> DeleteDirectory(long userId, long folderId);
        Task<bool> DeleteFile(long userId, long fileId);
        Task<List<Files>> GetFilesByUser(long userId);
        Task<List<Folders>> GetFolderByUser(long userId);
        Task<List<SharedFiles>> GetFilePermission(long userId);
        Task<List<SharedFolders>> GetFolderPermission(long userId);
        Task<bool> GrantFilePermission(SharedFiles shFile);
        Task<bool> GrantFolderPermission(SharedFolders shFile);
        Task<bool> ModifyDirectoryAccess(SharedFolders shFile);
        Task<bool> ModifyFileAccess(SharedFiles shFile);
        Task<long> UpdateDirectory(long userId, Folders updatedFolder);
        Task<long> UpdateFile(long userId, long fileId, HttpPostedFile file);
    }
}