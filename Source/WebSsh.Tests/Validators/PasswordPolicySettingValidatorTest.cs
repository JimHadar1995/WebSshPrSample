using System;
using WebSsh.Application.Dto.Users;
using WebSsh.Application.Validators.Users;
using WebSsh.Tests.Inner;
using Xunit;

namespace WebSsh.Tests.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PasswordPolicySettingValidatorTest
    {

        [Fact(DisplayName = "Тест на успешное прохождение проверок валидатора")]
        public void PositiveValidatorTest()
        {
            AssertTrueValidator(_ => _);
        }

        [Fact(DisplayName = "Тест на поле 'Минимальная длина пароля'")]
        public void MinLengthFieldTest()
        {
            AssertFalseValidator(_ => _ with { MinimumLength = 1 });
            AssertFalseValidator(_ => _ with { MinimumLength = 31 });
        }

        [Fact(DisplayName = "Тест на поле 'Максимальное время действия пароля")]
        public void RequiredMaxExpireTimeFieldTest()
        {
            AssertFalseValidator(_ => _ with { RequiredMaxExpireTime = -1 });
            AssertFalseValidator(_ => _ with { RequiredMaxExpireTime = 100 });
        }

        [Fact(DisplayName = "Тест на поле 'Максимальное количество неудачных попыток'")]
        public void MaxFailedAccessAttemptsFieldTest()
        {
            AssertFalseValidator(_ => _ with { MaxFailedAccessAttempts = 2 });
            AssertFalseValidator(_ => _ with { MaxFailedAccessAttempts = 9 });
        }

        [Fact(DisplayName = "Тест на поле 'Время блокировки пользователя'")]
        public void DefaultLockoutTimeSpanFieldTest()
        {
            AssertFalseValidator(_ => _ with { DefaultLockoutTimeSpan = 8 });
            AssertFalseValidator(_ => _ with { DefaultLockoutTimeSpan = 31 });
        }

        #region [ Help methods ]

        private void AssertFalseValidator(Func<PasswordPolicyDto, PasswordPolicyDto> func)
        {
            var validator = new PasswordPolicySettingValidator(Helper.GetValidationLocalizer());

            var dto = new PasswordPolicyDto();

            dto = func(dto);

            validator.AssertFalseValidatorWithoutCheckCountErrors(dto);
        }

        private void AssertTrueValidator(Func<PasswordPolicyDto, PasswordPolicyDto> func)
        {
            var validator = new PasswordPolicySettingValidator(Helper.GetValidationLocalizer());

            var dto = new PasswordPolicyDto
            {
                MinimumLength = 6,
                RequiredMaxExpireTime = 90,
                MaxFailedAccessAttempts = 3,
                DefaultLockoutTimeSpan = 15
            };

            dto = func(dto);

            validator.AssertTrueValidator(dto);
        }

        #endregion
    }
}
