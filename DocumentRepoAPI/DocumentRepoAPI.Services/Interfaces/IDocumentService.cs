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
        string AddFile(long userId, HttpPostedFile file);

        //Delete files
        // Takes file Id as input deletes that file and return true/false based on success or failure
        bool DeleteFile(int userId, int fileId);

        //Modify files
        // Takes fileId and updated file as input and updates that file returns id of updated file
        int UpdateFile(int userId, int fileId, byte[] updatedFile);

        //Make file public/private/access to particular users only
        // Takes fileId, array of userId to whoom access need to be given and accessType...returns true/false based on the sucess/failure
        bool ModifyFileAccess(int userId, int fileId, int[] userIds, int accessType);


        //Revoke particular user only
        // Takes fileId, userId from whoom access need to be revoked and accessType...returns true/false based on the sucess/failure
        bool ModifyFileAccess(int userId, int fileId, int accessType);




        //Add Directories
        // Takes folder name as argument and returns the id of the folder added
        Task<long> AddDirectory(Folders newFolder);

        //Delete Directories
        // Takes Folder Id as input deletes that Folder and return true/false based on success or failure
        bool DeleteDirectory(int userId, int folderId);

        //Modify Directories
        // Takes fileId and updated folder name
        int UpdateDirectory(int userId, int folderId, string updatedFolderName);

        //Make file public/private/access to particular users only
        // Takes folderId, array of userId to whoom access need to be given and accessType...returns true/false based on the sucess/failure
        bool ModifyDirectoryAccess(int userId, int folderId, int[] userIds, int accessType);

        //Revoke particular user only
        // Takes folderId, userId from whoom access need to be revoked and accessType...returns true/false based on the sucess/failure
        bool ModifyDirectoryAccess(int userId, int folderId, int accessType);

        //Move to trash - empty trash after 30 days
        //Using Windows Services with Logic or call a API of our application in Window Service

        //Grant and Revoke permissions to and from the users and documents
        void GrantPermission(int byUserId, int toUserId, int permissionType, int fileId, int folderId);
        void RevokePermission(int byUserId, int toUserId, int permissionType, int fileId, int folderId);

    }
}
