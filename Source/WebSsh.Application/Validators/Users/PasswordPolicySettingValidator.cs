using FluentValidation;
using Library.Common.Localization;
using WebSsh.Application.Dto.Users;
using WebSsh.ResourceManager.Validation;

namespace WebSsh.Application.Validators.Users
{
    /// <summary>
    /// Валидатор для настроек парольной политики.
    /// </summary>
    /// <seealso cref="UpdatePasswordPolicyCommand" />
    public sealed class PasswordPolicySettingValidator : AbstractValidator<PasswordPolicyDto>
    {
        public PasswordPolicySettingValidator(
            IValidationLocalizer localizer)
        {
            RuleFor(_ => _.MinimumLength)
                .GreaterThanOrEqualTo(6)
                .WithMessage(localizer.Message(ValidationConstants.MoreOrEqualTemplate) + " 6")
                .LessThanOrEqualTo(30)
                .WithMessage(localizer.Message(ValidationConstants.MaxOrEqualTemplate) + " 30")
                .WithName(localizer.Name(FieldNameConstants.MinPasswordLength));

            RuleFor(_ => _.RequiredMaxExpireTime)
                .GreaterThan(0)
                .WithMessage(localizer.Message(ValidationConstants.MoreZeroTemplate))
                .LessThanOrEqualTo(90)
                .WithMessage(
                    localizer.Message(ValidationConstants.MaxOrEqualTemplate) + " 90")
                .WithName(localizer.Name(FieldNameConstants.MaxPassExpireTime));

            RuleFor(_ => _.MaxFailedAccessAttempts)
                .GreaterThanOrEqualTo(3)
                .WithMessage(
                    localizer.Message(ValidationConstants.MoreOrEqualTemplate) + " 3")
                .LessThanOrEqualTo(8)
                .WithMessage(
                    localizer.Message(ValidationConstants.MaxOrEqualTemplate) + " 8")
                .WithName(localizer.Name(FieldNameConstants.MaxNumberOfPasswordAttempts));

            RuleFor(_ => _.DefaultLockoutTimeSpan)
                .GreaterThanOrEqualTo(10)
                .WithMessage(
                    localizer.Message(ValidationConstants.MoreOrEqualTemplate) + " 10")
                .LessThanOrEqualTo(30)
                .WithMessage(
                    localizer.Message(ValidationConstants.MaxOrEqualTemplate) + " 30")
                .WithName(localizer.Name(FieldNameConstants.UserLockTime));
        }
    }
}
