using System;
using System.Linq;
using FluentValidation;
using Identity.Application.Dto.Users;
using Identity.Application.Specifications.Users;
using Identity.Application.Validators.ValidatorHelpers;
using Identity.Core.Entities;
using Identity.Core.Services;
using Identity.ResourceManager.Validation;
using Library.Common.Database;
using Library.Common.Localization;
using Library.Common.ValidationHelpers;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Validators.Users
{
    /// <summary>
    /// Валидатор создания пользователя
    /// </summary>
    public sealed class CreateUserValidator : AbstractValidator<UserAddDto>
    {
        private readonly IUnitOfWork _ufw;
        /// <inheritdoc />
        public CreateUserValidator(
            IUnitOfWork ufw,
            IValidationLocalizer localizer,
            IIdentityAppSettings settings,
            IConfiguration? configuration)
        {
            _ufw = ufw;

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
                .Must(UniqueName)
                .WithMessage(localizer.Message(ValidationConstants.EntityMustBeUniqueTemplate))
                .WithName(localizer.Name(FieldNameConstants.UserName));

            RuleFor(_ => _.RoleId)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .Must(RoleExists)
                .WithMessage(localizer.Message(ValidationConstants.RoleNotFoundTemplate))
                .WithName(localizer.Name(FieldNameConstants.Role));

            RuleFor(_ => _.Password)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .WithName(localizer.Name(FieldNameConstants.Password));

            PasswordRulesHelper.PasswordBaseRules(
                this,
                settings.PasswordPolicy,
                _ => _.Password,
                localizer,
                configuration,
                FieldNameConstants.Password);

            RuleFor(_ => _.Password)
                .Length(settings.PasswordPolicy.MinimumLength, settings.PasswordPolicy.MaximumLength)
                .WithMessage(
                    localizer.Message(ValidationConstants.PasswordRequiredLength, settings.PasswordPolicy.MinimumLength, settings.PasswordPolicy.MaximumLength))
                .When(_ => !string.IsNullOrWhiteSpace(_.Password))
                .WithName(localizer.Name(FieldNameConstants.Password));
        }

        private bool UniqueName(string userName)
        {
            try
            {
                var spec = new UserWithNameExistsSpec(userName);
                return !_ufw.Repository<User>()
                    .Any(spec);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool RoleExists(int roleId)
        {
            try
            {
                var roles = _ufw.Repository<Role>().GetAll();
                return roles.Any(_ => _.Id == roleId);
            }
            catch
            {
                return false;
            }
        }
    }
}
