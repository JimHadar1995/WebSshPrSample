using System;
using System.Collections.Generic;
using System.Linq;
using Library.Common.Database;
using Library.Common.Database.Specifications;
using Moq;
using Tests.Common;
using WebSsh.Application.Validators.Users;
using WebSsh.Core.Entities;
using WebSsh.Core.Entities.Settings;
using WebSsh.Core.Services;
using WebSsh.Shared.Dto.Users;
using WebSsh.Tests.Inner;
using Xunit;

namespace Identity.Tests.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ChangePasswordForUserValidatorTest
    {

        [Fact(DisplayName = "Тест на отсутствующего пользователя")]
        public void UserIdNotFoundTest()
        {
            AssertFalseValidation(_ => _ with { UserId = 4 });
        }

        [Fact(DisplayName = "Тест на пустой пароль")]
        public void NewPasswordEmptyTest()
        {
            AssertFalseValidation(_ => _ with { NewPassword = string.Empty });
        }

        [Fact(DisplayName = "Тест на минимальную / максимальную длину пароля")]
        public void NewPasswordLengthTest()
        {
            AssertFalseValidation(_ => _ with { NewPassword = "$Qwe1" });
            AssertFalseValidation(_ => _ with { NewPassword = "$Qwerty123456777777777777777777777777777777777777777777777777777777777777777" });
        }

        [Fact(DisplayName = "Тест на отсутствие цифр")]
        public void NewPasswordRequireDigit()
        {
            AssertFalseValidation(_ => _ with { NewPassword = "$Qwertyqqqqqqqq" });
        }

        [Fact(DisplayName = "Тест на отсутствие символов в нижнем регистре")]
        public void NewPasswordLowerCase()
        {
            AssertFalseValidation(_ => _ with { NewPassword = "$QWERTY77777" });
        }

        [Fact(DisplayName = "Тест на отсутствие символов в верхнем регистре")]
        public void NewPasswordUpperCase()
        {
            AssertFalseValidation(_ => _ with { NewPassword = "$qwerty77777" });
        }

        [Fact(DisplayName = "Тест на отсутствие не буквенных и нечисловых символов")]
        public void NewPasswordNonAlphabethic()
        {
            AssertFalseValidation(_ => _ with { NewPassword = "Qwertyy77777" });
        }

        #region [ Help methods ]

        private IQueryable<User> GetExistedUsers()
        {
            return new TestAsyncEnumerable<User>(new List<User>
            {
                new User
                {
                    UserName = "test_user1",
                    CreatedAt = DateTimeOffset.Now,
                    Description = "test_user1",
                    Id = 1,
                    UpdatedAt = DateTimeOffset.Now
                },
                new User
                {
                    UserName = "test_user2",
                    CreatedAt = DateTimeOffset.Now,
                    Description = "test_user2",
                    Id = 2,
                    UpdatedAt = DateTimeOffset.Now
                },
                new User
                {
                    UserName = "test_user3",
                    CreatedAt = DateTimeOffset.Now,
                    Description = "test_user3",
                    Id = 3,
                    UpdatedAt = DateTimeOffset.Now,
                    IsDefaultUser = true
                }
            });
        }

        private void AssertFalseValidation(Func<ChangePasswordForUserDto, ChangePasswordForUserDto> func)
        {
            var identityAppSettingsRepoMock = new Mock<IIdentityAppSettings>();
            identityAppSettingsRepoMock.Setup(_ => _.PasswordPolicy)
                .Returns(new PasswordPolicy()
                {
                    RequireNonAlphanumeric = true
                });

            var userRepo = new Mock<IRepository<User>>();

            userRepo.Setup(_ => _.Any(It.IsAny<ISpecification<User>>()))
                .Returns<ISpecification<User>>((spec) =>
                {
                    var users = GetExistedUsers();
                    return users.Any(spec.ToExpression());
                });

            var ufwMock = new Mock<IUnitOfWork>();
            ufwMock.Setup(_ => _.Repository<User>())
                .Returns(userRepo.Object);

            var validator = new ChangePasswordForUserValidator(
                Helper.GetValidationLocalizer(),
                identityAppSettingsRepoMock.Object, 
                ufwMock.Object,
                null);

            var model = new ChangePasswordForUserDto
            {
                NewPassword = "$Qwerty123456",
                UserId = 1
            };

            model = func(model);

            validator.AssertFalseValidator(model);
        }

        #endregion
    }
}
