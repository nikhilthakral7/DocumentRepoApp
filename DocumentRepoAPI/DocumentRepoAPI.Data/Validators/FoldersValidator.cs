using DocumentRepoAPI.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Data.Validators
{
    public class FoldersValidator : AbstractValidator<Folders>
    {
        public FoldersValidator()
        {
            RuleFor(x => x.FolderName).NotEmpty();
            RuleFor(x => x.ReadAccess);
            RuleFor(x => x.ModifyAccess);
            RuleFor(x => x.DeleteAccess);
            RuleFor(x => x.CreatedBy).NotEmpty(); ;
            RuleFor(x => x.CreateDate);
            RuleFor(x => x.ModifiedBy).NotEmpty(); ;
            RuleFor(x => x.ModifiedDate);

        }
    }
}
