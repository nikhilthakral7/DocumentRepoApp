using DocumentRepoAPI.Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoAPI.Data.Validators
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserId);
            RuleFor(x => x.EmailId).NotEmpty().EmailAddress().MaximumLength(254);
            RuleFor(x => x.PasswordHash).NotEmpty();
            RuleFor(x => x.UserTypeId).NotEmpty();
            RuleFor(x => x.UserActive);
            // When is like if condition
            When(x => x.FirstName != null && x.FirstName != "", () =>
            {
                RuleFor(x => x.FirstName).NotEqual(x => x.LastName).MaximumLength(100);
                //RuleFor(x => x.CreditCardNumber).NotNull();
            });
            RuleFor(x => x.LastName);
            When(x => x.RecoveryPhoneNum != null && x.RecoveryPhoneNum != "", () =>
            {
                RuleFor(x => x.RecoveryPhoneNum).MaximumLength(12);
                RuleFor(x => x.RecoveryPhoneNum).Must(BeAValidPhNum).WithMessage("Invalid phone number format!");
                //RuleFor(x => x.CreditCardNumber).NotNull();
            });
            RuleFor(x => x.CreatedBy).NotEmpty();
            RuleFor(x => x.CreateDate);
            RuleFor(x => x.ModifiedBy).NotEmpty();
            RuleFor(x => x.ModifiedDate);
        }

        //Custom method for ph num validation
        private bool BeAValidPhNum(string phnum)
        {

            return true;
        }
    }
}
