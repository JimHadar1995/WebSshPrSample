using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using Identity.Core.Entities.Settings;
using Identity.ResourceManager.Validation;
using Library.Common.Localization;
using Library.Common.ValidationHelpers;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Validators.ValidatorHelpers
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PasswordRulesHelper
    {
        /// <summary>
        /// Устанавливает правила валидации для пароля:
        /// - необходимы ли цифры;
        /// - символы в верхнем регистре;
        /// - символы в нижнем регистре;
        /// - нечисловые и нецифровые символы.
        /// </summary>
        /// <param name="passwordSettings"></param>
        /// <param name="expressionField">Поле, для которого необходимо установить правила валиацдии пароля</param>
        /// <param name="configuration"></param>
        /// <param name="fieldName">Название поля для ошибок</param>
        /// <param name="validator"></param>
        /// <param name="localizer"></param>
        /// <returns></returns>
        public static void PasswordBaseRules<T>(
            AbstractValidator<T> validator,
            PasswordPolicy passwordSettings,
            Expression<Func<T, string?>> expressionField,
            IValidationLocalizer localizer,
            IConfiguration? configuration,
            string fieldName = "Пароль")
        {
            var weakPasswordsSection = configuration?.GetSection("WeakPasswords")?.Get<List<string>?>();

            validator.RuleFor(expressionField)
                .Must(_ => !ContainsWeakPassword(_!, weakPasswordsSection))
                .WithMessage(localizer.Message(ValidationConstants.PasswordContainsPopularPassword))
                .WithName(localizer.Name(fieldName));

            validator.RuleFor(expressionField)
                .Must(Validation.ContainsDigits)
                .WithMessage(localizer.Message(ValidationConstants.ContainsDigitsTemplate))
                .When(dto => passwordSettings.RequireDigit, ApplyConditionTo.CurrentValidator)
                .WithName(localizer.Name(fieldName));

            validator.RuleFor(expressionField)
                .Must(Validation.ContainsLowerCase)
                .WithMessage(localizer.Message(ValidationConstants.ContainsLowerCaseTemplate))
                .When(dto => passwordSettings.RequireLowercase, ApplyConditionTo.CurrentValidator)
                .WithName(localizer.Name(fieldName));

            validator.RuleFor(expressionField)
                .Must(Validation.ContainsUpperCase)
                .WithMessage(localizer.Message(ValidationConstants.ContainsUpperCaseTemplate))
                .When(dto => passwordSettings.RequireUppercase, ApplyConditionTo.CurrentValidator)
                .WithName(localizer.Name(fieldName));

            validator.RuleFor(expressionField)
                .Must(Validation.ContainsNonAlphaNumeric)
                .WithMessage(localizer.Message(ValidationConstants.ContainsNonAlphaNumericTemplate))
                .When(dto => passwordSettings.RequireNonAlphanumeric, ApplyConditionTo.CurrentValidator)
                .WithName(localizer.Name(fieldName));
        }

        private static bool ContainsWeakPassword(string str, List<string>? weakPasswords)
        {
            if (weakPasswords == null || !weakPasswords.Any())
                return false;

            str = str.ToLowerInvariant();

            foreach (var weakPassword in weakPasswords)
            {
                if (str.Contains(weakPassword.ToLowerInvariant()))
                    return true;
            }

            return false;
        }
    }
}
