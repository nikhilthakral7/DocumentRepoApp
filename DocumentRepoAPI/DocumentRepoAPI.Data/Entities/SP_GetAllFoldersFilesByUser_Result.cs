//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocumentRepoAPI.Data.Entities
{
    using System;
    
    public partial class SP_GetAllFoldersFilesByUser_Result
    {
        public long Id { get; set; }
        public Nullable<long> ContainingFolderId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public bool ReadAccess { get; set; }
        public bool ModifyAccess { get; set; }
        public bool DeleteAccess { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}