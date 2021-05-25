using DocumentRepoAPI.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Data.Validators
{
    public class LoginDTOValidator: AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(x => x.EmailId).NotEmpty().EmailAddress();
            RuleFor(x => x.PasswordHash).NotEmpty();
        }
    }
}
