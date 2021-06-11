using DocumentRepoAPI.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Data.Validators
{
    public class SharedFileValidator: AbstractValidator<SharedFiles>
    {
        public SharedFileValidator()
        {
            RuleFor(x => x.SharedFile).NotEmpty();
            RuleFor(x => x.SharedBy).NotEmpty();
            RuleFor(x => x.SharedTo).NotEmpty();
            RuleFor(x => x.ReadAccess);
            RuleFor(x => x.ModifyAccess);
            RuleFor(x => x.DeleteAccess);
            RuleFor(x => x.CreatedBy).NotEmpty();
            RuleFor(x => x.CreateDate);
            RuleFor(x => x.ModifiedBy).NotEmpty();
            RuleFor(x => x.ModifiedDate);
    }
    }
}
