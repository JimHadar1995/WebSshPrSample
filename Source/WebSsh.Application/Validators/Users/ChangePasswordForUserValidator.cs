using System.Linq;
using FluentValidation;
using Library.Common.Database;
using Library.Common.Localization;
using Microsoft.Extensions.Configuration;
using WebSsh.Application.Dto.Users;
using WebSsh.Application.Specifications.Users;
using WebSsh.Application.Validators.ValidatorHelpers;
using WebSsh.Core.Entities;
using WebSsh.Core.Services;
using WebSsh.ResourceManager.Validation;

namespace WebSsh.Application.Validators.Users
{
    /// <summary>
    /// Валидатор модели сброса пароля указанному пользователю
    /// </summary>
    public sealed class ChangePasswordForUserValidator : AbstractValidator<ChangePasswordForUserDto>
    {
        private readonly IUnitOfWork _ufw;
        /// <inheritdoc />
        public ChangePasswordForUserValidator(
            IValidationLocalizer localizer,
            IIdentityAppSettings settings,
            IUnitOfWork ufw,
            IConfiguration? configuration)
        {
            _ufw = ufw;
            RuleFor(_ => _.UserId)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .WithName(localizer.Name(FieldNameConstants.User));

            RuleFor(_ => _.UserId)
                .Must(UserExists)
                .WithMessage(localizer.Message(ValidationConstants.UserNotFound));

            RuleFor(_ => _.NewPassword)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .WithName(localizer.Name(FieldNameConstants.Password));

            PasswordRulesHelper.PasswordBaseRules(
                this,
                settings.PasswordPolicy,
                _ => _.NewPassword,
                localizer,
                configuration,
                FieldNameConstants.Password);

            RuleFor(_ => _.NewPassword)
                .Length(settings.PasswordPolicy.MinimumLength, settings.PasswordPolicy.MaximumLength)
                .WithMessage(
                    localizer.Message(ValidationConstants.PasswordRequiredLength, settings.PasswordPolicy.MinimumLength, settings.PasswordPolicy.MaximumLength))
                .When(_ => !string.IsNullOrWhiteSpace(_.NewPassword))
                .WithName(localizer.Name(FieldNameConstants.NewPassword));
        }

        private bool UserExists(int id)
        {
            try
            {
                return _ufw.Repository<User>().Any(new UserWithIdExistsSpec(id));
            }
            catch
            {
                return false;
            }
        }
    }
}
