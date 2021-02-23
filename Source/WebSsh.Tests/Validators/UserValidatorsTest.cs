using System;
using System.Collections.Generic;
using System.Linq;
using Library.Common.Database;
using Library.Common.Database.Specifications;
using Moq;
using Tests.Common;
using WebSsh.Application.Dto.Users;
using WebSsh.Application.Validators.Users;
using WebSsh.Core.Entities;
using WebSsh.Core.Entities.Settings;
using WebSsh.Core.Services;
using WebSsh.Tests.Inner;
using Xunit;

namespace WebSsh.Tests.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class UserValidatorsTest
    {
        #region [ CreateUserValidator ]

        [Fact(DisplayName = "Проверка работы валидатора при создании пользователя с корректными данными")]
        public void PositiveCreateUserValidatorTest()
        {
            var ufwMock = GetMockedUfw();
            var user = GetNewUser();
            var settings = GetMockedAppSettings();

            var validator = new CreateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer(), settings, null);
            var validationResult = validator.Validate(user);
            Assert.True(validationResult.IsValid);
        }

        [Fact(DisplayName = "Проверка максимальной длины описания в 250 символов для валидатора создания")]
        public void DescriptionMax250CreateUserValidatorTest()
        {
            TestFailureCreateValidator(_ =>
                _ with
                {
                    Description =
                    "testtesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttes"
                });
        }

        [Fact(DisplayName = "Проверка максимальной длины имени пользователя в 50 символов для валидатора создания")]
        public void UserNameMax50CreateValidatorTest()
        {
            TestFailureCreateValidator(_ =>
                _ with
                {
                    UserName = "testtesttesttesttesttesttesttesttesttesttesttesttest"
                });
        }

        [Fact(DisplayName = "Проверка на непустоту имени пользователя для валидатора создания")]
        public void UserNameNotEmptyCreateValidatorTest()
        {
            TestFailureCreateValidator(_ =>
                _ with { UserName = "" });
        }

        [Fact(DisplayName = "Проверка на то, что имя пользователя не должно содержать кириллицу для валидатора создания")]
        public void UserNameSpecSymbolsCreateValidatorTest()
        {
            TestFailureCreateValidator(_ =>
                _ with { UserName = "выфвфывфы" });
            TestFailureCreateValidator(_ =>
                _ with { UserName = "!@#$" });
        }

        [Fact(DisplayName = "Проверка на то, что имя пользователя должно быть уникальным для валидатора создания")]
        public void UserNameUniqueCreateValidatorTest()
        {
            TestFailureCreateValidator(_ =>
                _ with { UserName = "test_user1" });
            TestFailureCreateValidator(_ =>
                _ with { UserName = "test_user2" });
        }

        [Fact(DisplayName = "Проверка на непустой список ролей для пользователя для валидатора создания")]
        public void RolesCreateValidatorTest()
        {
            TestFailureCreateValidator(_ => _ with { RoleId = 100 });
        }

        [Fact(DisplayName = "Проверка парольных политик для валидатора создания")]
        public void PasswordUserCreateValidatorTests()
        {
            //пароль не может быть пустым
            TestFailurePassword(
                _ => _ with { Password = "" });
            //маленькая длина (должна быть 8-50)
            TestFailurePassword(
                _ => _ with { Password = "1" });
            //длина пароля верная, но нет символов в верхем регистре
            TestFailurePassword(_ => _ with { Password = "qwerty789" });
            //нет цифр
            TestFailurePassword(_ => _ with { Password = "Qwertydasdklasdlasdk" });
            //нет нечисловых и небуквенных символов
            TestFailurePassword(_ => _ with { Password = "Qwerty789" });
            //нет символов в нижем регистре
            TestFailurePassword(_ => _ with { Password = "qwerty789!" });
        }

        #endregion

        #region [ UpdateUserValidato ]

        [Fact(DisplayName = "Проверка работы валидатора при обновлении пользователя с корректными данными для валидатора обновления")]
        public void PositiveUpdateUserValidatorTest()
        {
            var ufwMock = GetMockedUfw();
            var user = GetUserForUpdate();

            var validator = new UpdateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer());
            var validationResult = validator.Validate(user);
            Assert.True(validationResult.IsValid);
        }

        [Fact(DisplayName = "Проверка на попытку изменения пользователя по умолчанию для валидатора обновления")]
        public void DefaultUserDeniedUpdateUpdateValidatorTest()
        {
            TestFailureUpdateValidator(_ =>
            _ with
            {
                Id = GetExistedUsers().Last().Id,
                UserName = "test_user111"
            });
        }

        [Fact(DisplayName = "Проверка максимальной длины описания в 250 символов для валидатора обновления")]
        public void DescriptionMax250UpdateValidatorTest()
        {
            TestFailureUpdateValidator(_ =>
                _ with
                {
                    Description =
                    "testtesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttes"
                });
        }

        [Fact(DisplayName = "Проверка максимальной длины имени пользователя в 50 символов для валидатора обновления")]
        public void UserNameMax50UpdateValidatorTest()
        {
            TestFailureUpdateValidator(_ =>
                _ with
                {
                    UserName = "testtesttesttesttesttesttesttesttesttesttesttesttest"
                });
        }

        [Fact(DisplayName = "Проверка на непустоту имени пользователя для валидатора обновления")]
        public void UserNameNotEmptyUpdateValidatorTest()
        {
            TestFailureUpdateValidator(_ =>
                _ with
                {
                    UserName = ""
                });
        }

        [Fact(DisplayName = "Проверка на то, что имя пользователя не должно содержать кириллицу для валидатора обновления")]
        public void UserNameSpecSymbolsUpdateValidatorTest()
        {
            TestFailureUpdateValidator(_ =>
                _ with
                {
                    UserName =
                    "выфвфывфы"
                });
            TestFailureUpdateValidator(_ =>
                _ with
                {
                    UserName =
                    "!@#$"
                });
        }

        [Fact(DisplayName = "Проверка на то, что имя пользователя должно быть уникальным для валидатора обновления")]
        public void UserNameUniqueUpdateValidatorTest()
        {
            TestFailureUpdateValidator(_ =>
                _ with
                {
                    UserName =
                    GetExistedUsers().Last().UserName
                });
            TestFailureUpdateValidator(_ =>
                _ with
                {
                    UserName =
                    "test_user2"
                });
        }

        [Fact(DisplayName = "Проверка на непустой список ролей для пользователя для валидатора обновления")]
        public void RolesUpdateValidatorTest()
        {
            TestFailureUpdateValidator(_ => _ with { RoleId = 100 });
        }

        #endregion

        #region [ Help methods ]

        private UserAddDto GetNewUser()
        {
            return new UserAddDto
            {
                Description = "test_user",
                UserName = "test_user",
                RoleId = 1,
                Password = "$Qwerty123456"
            };
        }

        private UserUpdateDto GetUserForUpdate()
        {
            var user = GetExistedUsers().First();
            return new UserUpdateDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Description = user.Description,
                RoleId = 1
            };
        }

        private IQueryable<Role> GetRolesQ()
        {
            return new TestAsyncEnumerable<Role>(new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Description = "role1",
                    Name = "role1",
                },
                new Role
                {
                    Id = 2,
                    Description = "role2",
                    Name = "role2",
                },
            });
        }

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

        private Mock<IUnitOfWork> GetMockedUfw()
        {
            var userRoleMock = new Mock<IRepository<Role>>();
            userRoleMock.Setup(_ => _.GetAll())
                .Returns(GetRolesQ().ToList());

            var userRepoMock = new Mock<IRepository<User>>();
            userRepoMock.Setup(_ => _.Any(It.IsAny<ISpecification<User>>()))
                .Returns<ISpecification<User>>(spec => GetExistedUsers().Any(spec.ToExpression()));

            var ufwMock = new Mock<IUnitOfWork>();
            ufwMock.Setup(_ => _.Repository<Role>())
                .Returns(userRoleMock.Object);
            ufwMock.Setup(_ => _.Repository<User>())
                .Returns(userRepoMock.Object);

            return ufwMock;
        }

        private IIdentityAppSettings GetMockedAppSettings()
        {
            var identityAppSettingsRepoMock = new Mock<IIdentityAppSettings>();
            identityAppSettingsRepoMock.Setup(_ => _.PasswordPolicy)
                .Returns(new PasswordPolicy()
                {
                    RequireNonAlphanumeric = true
                });

            return identityAppSettingsRepoMock.Object;
        }

        private void TestFailureCreateValidator(Func<UserAddDto, UserAddDto> func)
        {
            var ufwMock = GetMockedUfw();
            var user = GetNewUser();
            user = func(user);

            var validator = new CreateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer(), GetMockedAppSettings(), null);
            validator.AssertFalseValidator(user);
        }

        private void TestFailureUpdateValidator(Func<UserUpdateDto, UserUpdateDto> func)
        {
            var ufwMock = GetMockedUfw();
            var user = GetUserForUpdate();
            user = func(user);

            var validator = new UpdateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer());
            validator.AssertFalseValidator(user);
        }

        private void TestFailurePassword(Func<UserAddDto, UserAddDto> func)
        {
            var ufwMock = GetMockedUfw();
            var user = GetNewUser();
            user = func(user);

            var validator = new CreateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer(), GetMockedAppSettings(), null);
            validator.AssertFalseValidatorWithoutCheckCountErrors(user);
        }

        #endregion
    }
}
