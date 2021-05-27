using DocumentRepoAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentRepoAPI.Services.Interfaces
{
    public interface IDocumentService
    {
        //Add Files
        // Takes file as input stores file at server and return path where file is stored on serverS
        string AddFile(long userId, string folderName, HttpPostedFile file);

        //Delete files
        // Takes file Id as input deletes that file and return true/false based on success or failure
        Task<bool> DeleteFile(long userId, long fileId);

        //Modify files
        // Takes fileId and updated file as input and updates that file returns id of updated file
        long UpdateFile(long userId, long fileId, byte[] updatedFile);

        //Make file public/private/access to particular users only
        // Takes fileId, array of userId to whoom access need to be given and accessType...returns true/false based on the sucess/failure
        bool ModifyFileAccess(long userId, long fileId, long[] userIds, long accessType);


        //Revoke particular user only
        // Takes fileId, userId from whoom access need to be revoked and accessType...returns true/false based on the sucess/failure
        bool ModifyFileAccess(long userId, long fileId, long accessType);




        //Add Directories
        // Takes folder name as argument and returns the id of the folder added
        Task<long> AddDirectory(long userId, Folders newFolder);

        //Delete Directories
        // Takes Folder Id as input deletes that Folder and return true/false based on success or failure
        bool DeleteDirectory(long userId, string folderPath);

        //Modify Directories
        // Takes fileId and updated folder name
        long UpdateDirectory(long userId, long folderId, string updatedFolderName);

        //Make file public/private/access to particular users only
        // Takes folderId, array of userId to whoom access need to be given and accessType...returns true/false based on the sucess/failure
        bool ModifyDirectoryAccess(long userId, long folderId, long[] userIds, long accessType);

        //Revoke particular user only
        // Takes folderId, userId from whoom access need to be revoked and accessType...returns true/false based on the sucess/failure
        bool ModifyDirectoryAccess(long userId, long folderId, long accessType);

        //Move to trash - empty trash after 30 days
        //Using Windows Services with Logic or call a API of our application in Window Service

        //Grant and Revoke permissions to and from the users and documents

        Task GrantFilePermission(SharedFiles shFile);

        void RevokeFilePermission(SharedFiles shFile);

        Task GrantFolderPermission(SharedFolders shFolder);

        void RevokeFolderPermission(SharedFolders shFolder);

    }
}
