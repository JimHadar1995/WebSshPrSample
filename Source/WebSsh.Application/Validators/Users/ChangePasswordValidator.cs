using System.Threading.Tasks;
using FluentValidation;
using Library.Common.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebSsh.Application.Dto.Users;
using WebSsh.Application.Validators.ValidatorHelpers;
using WebSsh.Core.Entities;
using WebSsh.Core.Services;
using WebSsh.ResourceManager.Validation;

namespace WebSsh.Application.Validators.Users
{
    /// <summary>
    /// Валидатор модели смены пароля
    /// </summary>
    /// <seealso cref="AbstractValidator{ChangePasswordDto}" />
    public sealed class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        private readonly UserManager<User> _userManager;
        /// <inheritdoc />
        public ChangePasswordValidator(
            IValidationLocalizer localizer,
            IHttpContextAccessor accessor,
            UserManager<User> userManager,
            IIdentityAppSettings settings,
            IConfiguration configuration)
        {
            _userManager = userManager;

            RuleFor(_ => _.NewPassword)
                .NotEmpty()
                .WithMessage(localizer.Message(ValidationConstants.NotEmptyMessageTemplate))
                .WithName(localizer.Name(FieldNameConstants.NewPassword));

            RuleFor(_ => _.NewPassword)
                .MustAsync((_, x, y) => CheckNewPassword(accessor.HttpContext!.User!.Identity!.Name!, _))
                .WithMessage(localizer.Message(ValidationConstants.OldAndNewPassMatch))
                .WithName(localizer.Name(FieldNameConstants.NewPassword));

            RuleFor(_ => _.ConfirmNewPassword)
                .Must((x, y) => x.ConfirmNewPassword == x.NewPassword)
                .WithMessage(localizer.Message(ValidationConstants.NewPassAndConfirmMustBeEqual))
                .WhenAsync((_, x) => CheckOldPass(accessor.HttpContext!.User.Identity!.Name!, _.OldPassword));

            RuleFor(_ => _.OldPassword)
                .MustAsync((_, x) => CheckOldPass(accessor.HttpContext!.User.Identity!.Name!, _))
                .WithMessage(localizer.Message(ValidationConstants.CurrentPasswordIncorrectTemplate));

            PasswordRulesHelper.PasswordBaseRules(
                this,
                settings.PasswordPolicy,
                _ => _.NewPassword,
                localizer,
                configuration,
                FieldNameConstants.NewPassword);

            RuleFor(_ => _.NewPassword)
                .Length(settings.PasswordPolicy.MinimumLength, settings.PasswordPolicy.MaximumLength)
                .WithMessage(
                    localizer.Message(ValidationConstants.PasswordRequiredLength, settings.PasswordPolicy.MinimumLength, settings.PasswordPolicy.MaximumLength))
                .When(_ => !string.IsNullOrWhiteSpace(_.NewPassword))
                .WithName(localizer.Name(FieldNameConstants.NewPassword));
        }

        private async Task<bool> CheckOldPass(string userName, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                var tmp = await _userManager.CheckPasswordAsync(user, password);
                return tmp;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> CheckNewPassword(string userName, ChangePasswordDto command)
        {
            try
            {
                if (!await CheckOldPass(userName, command.OldPassword))
                {
                    return true;
                }
                var user = await _userManager.FindByNameAsync(userName);
                return !await _userManager.CheckPasswordAsync(user, command.NewPassword);
            }
            catch
            {
                return false;
            }
        }
    }
}
