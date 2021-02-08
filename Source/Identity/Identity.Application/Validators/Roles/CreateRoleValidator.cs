using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Identity.Application.Dto.Roles;
using Identity.Application.Specifications.Roles;
using Identity.Core.Entities;
using Identity.ResourceManager.Validation;
using Library.Common.Database;
using Library.Common.Localization;

namespace Identity.Application.Validators.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CreateRoleValidator : AbstractValidator<RoleAddDto>
    {
        private readonly IUnitOfWork _ufw;
        /// <inheritdoc />
        public CreateRoleValidator(
            IUnitOfWork ufw,
            IValidationLocalizer localizer)
        {
            _ufw = ufw;

            RuleFor(_ => _.Name)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .Length(0, 50)
                .WithMessage(localizer.Message(ValidationConstants.Max50LengthMessageTemplate))
                .Must(UniqueName)
                .WithMessage(localizer.Message(ValidationConstants.EntityMustBeUniqueTemplate))
                .WithName(localizer.Name(FieldNameConstants.Name));

            RuleFor(_ => _.Description)
                .Length(0, 250)
                .WithMessage(localizer.Message(ValidationConstants.Max250LengthMessageTemplate))
                .WithName(localizer.Name(FieldNameConstants.Description));

            RuleFor(_ => _.Privileges)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .Must(CheckPrivileges)
                .WithMessage(localizer.Message(ValidationConstants.NoPrivilegesSpecifiedForRole))
                .WithName(localizer.Name(FieldNameConstants.Privileges));
        }

        private bool UniqueName(string name)
        {
            try
            {
                var spec = new RoleWithNameWithNameExistsSpec(name);
                return !_ufw.Repository<Role>().Any(spec);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CheckPrivileges(List<RoleAddDto.PrivilegeForRoleDto> privileges)
        {
            try
            {
                if (!privileges.Any())
                    return true;
                var privRepo = _ufw.Repository<Privilege>();
                var allPrivs = privRepo.GetAll();
                var privsExist = false;
                foreach (var privilege in privileges)
                {
                    if (allPrivs.Any(_ => _.Id == privilege.PrivilegeId))
                    {
                        privsExist = true;
                        break;
                    }
                }

                return privsExist;
            }
            catch
            {
                return false;
            }
        }
    }
}
