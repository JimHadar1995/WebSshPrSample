using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Identity.Application.Dto.Users;
using Identity.Application.Specifications.Users;
using Identity.Core.Entities;
using Identity.ResourceManager.Validation;
using Library.Common.Database;
using Library.Common.Localization;
using Library.Common.ValidationHelpers;

namespace Identity.Application.Validators.Users
{
    /// <summary>
    /// Вадидатор модели обновления пользователя
    /// </summary>
    public sealed class UpdateUserValidator : AbstractValidator<UserUpdateDto>
    {
        private readonly IUnitOfWork _ufw;
        /// <inheritdoc />
        public UpdateUserValidator(
            IUnitOfWork ufw,
            IValidationLocalizer localizer)
        {
            _ufw = ufw;

            RuleFor(_ => _.Id)
                .Must((_, x) => CheckDefaultUser(_))
                .WithMessage(localizer.Message(ValidationConstants.ChangingDefaultUserIsProhibited));

            RuleFor(_ => _.Description)
                .Length(0, 250)
                .WithMessage(localizer.Message(ValidationConstants.Max250LengthMessageTemplate))
                .WithName(localizer.Name(FieldNameConstants.Description));

            RuleFor(_ => _.UserName)
                .Length(0, 50)
                .WithMessage(localizer.Message(ValidationConstants.Max50LengthMessageTemplate))
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .Must(Validation.UserNameIsValid)
                .WithMessage(localizer.Message(ValidationConstants.OnlyLatinAndSpecSymbolsTemplate))
                .WithName(localizer.Name(FieldNameConstants.UserName));

            RuleFor(_ => _.UserName)
                .Must((x, y) => UniqueName(x))
                .WithMessage(localizer.Message(ValidationConstants.EntityMustBeUniqueTemplate))
                .WithName(localizer.Name(FieldNameConstants.UserName));

            RuleFor(_ => _.RoleIds)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .Must(RoleExists)
                .WithMessage(localizer.Message(ValidationConstants.RoleNotFoundTemplate))
                .WithName(localizer.Name(FieldNameConstants.Role));
        }

        private bool UniqueName(UserUpdateDto model)
        {
            try
            {
                var userNameSpec = new UserWithNameExistsSpec(model.UserName);
                return !_ufw.Repository<User>()
                    .Any(userNameSpec.Not(new UserWithIdExistsSpec(model.Id)));
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool RoleExists(List<int> roleIds)
        {
            try
            {
                if (!roleIds.Any())
                    return true;

                var roles = _ufw.Repository<Role>().GetAll();
                var existsAnyRole = false;
                foreach (var role in roles)
                {
                    if (roleIds.Contains(role.Id))
                    {
                        existsAnyRole = true;
                    }
                }

                return existsAnyRole;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckDefaultUser(UserUpdateDto model)
        {
            try
            {
                var defaultUserSpec = new DefaultUserSpec();
                var userWithIdExistsSpec = new UserWithIdExistsSpec(model.Id);
                return !_ufw.Repository<User>()
                    .Any(defaultUserSpec.And(userWithIdExistsSpec));
            }
            catch
            {
                return false;
            }
        }
    }
}
