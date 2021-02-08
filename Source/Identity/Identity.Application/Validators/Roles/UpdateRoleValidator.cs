using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Identity.Application.Dto.Roles;
using Identity.Application.Specifications.Roles;
using Identity.Core.Entities;
using Identity.ResourceManager.Validation;
using Library.Common.Database;
using Library.Common.Database.Specifications;
using Library.Common.Localization;

namespace Identity.Application.Validators.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UpdateRoleValidator : AbstractValidator<RoleUpdateDto>
    {
        private readonly IUnitOfWork _ufw;

        /// <inheritdoc />
        public UpdateRoleValidator(
            IUnitOfWork ufw,
            IValidationLocalizer localizer)
        {
            _ufw = ufw;

            RuleFor(_ => _.Id)
                .Must((_, x) => CheckDefaultRole(_))
                .WithMessage(localizer.Message(ValidationConstants.ChangingDefaultRoleIsProhibited));

            RuleFor(_ => _.Name)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .Length(0, 50)
                .WithMessage(localizer.Message(ValidationConstants.Max50LengthMessageTemplate))
                .Must((_, x) => UniqueName(_))
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

        private bool UniqueName(RoleUpdateDto model)
        {
            try
            {
                ICompositeSpecification<Role> idSpec = new RoleWithNameWithIdExistsSpec(model.Id);
                return !_ufw.Repository<Role>()
                    .Any(idSpec.And(new RoleWithNameWithNameExistsSpec(model.Name)));
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CheckDefaultRole(RoleUpdateDto model)
        {
            try
            {
                var spec = new DefaultRoleSpec();
                return !_ufw.Repository<Role>()
                    .Any(spec.And(new RoleWithNameWithIdExistsSpec(model.Id)));
            }
            catch
            {
                return false;
            }
        }
    }
}
