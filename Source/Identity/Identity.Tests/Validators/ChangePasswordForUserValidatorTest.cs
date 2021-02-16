using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Identity.Core.Entities.Settings;
using Identity.Tests.Inner;
using Library.Common.Database;
using Library.Common.Database.AppSettingsEntity;
using Moq;
using Tests.Common;
using Xunit;

namespace Identity.Tests.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ChangePasswordForUserValidatorTest
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //[Fact(DisplayName = "Тест на пустоту идентификатора пользователя")]
        //public void NotEmptyUserIdTest()
        //{
        //    AssertFalseValidation(_ => _.UserId = string.Empty);
        //}

        //[Fact(DisplayName = "Тест на отсутствующего пользователя")]
        //public void UserIdNotFoundTest()
        //{
        //    AssertFalseValidation(_ => _.UserId = Guid.NewGuid().ToString());
        //}

        //[Fact(DisplayName = "Тест на пустой пароль")]
        //public void NewPasswordEmptyTest()
        //{
        //    AssertFalseValidation(_ => _.NewPassword = string.Empty);
        //}

        //[Fact(DisplayName = "Тест на минимальную / максимальную длину пароля")]
        //public void NewPasswordLengthTest()
        //{
        //    AssertFalseValidation(_ => _.NewPassword = "$Qwe1");
        //    AssertFalseValidation(_ => _.NewPassword = "$Qwerty123456777777777777777777777777777777777777777777777777777777777777777");
        //}

        //[Fact(DisplayName = "Тест на отсутствие цифр")]
        //public void NewPasswordRequireDigit()
        //{
        //    AssertFalseValidation(_ => _.NewPassword = "$Qwerty");
        //}

        //[Fact(DisplayName = "Тест на отсутствие символов в нижнем регистре")]
        //public void NewPasswordLowerCase()
        //{
        //    AssertFalseValidation(_ => _.NewPassword = "$QWERTY7");
        //}

        //[Fact(DisplayName = "Тест на отсутствие символов в верхнем регистре")]
        //public void NewPasswordUpperCase()
        //{
        //    AssertFalseValidation(_ => _.NewPassword = "$qwerty7");
        //}

        //[Fact(DisplayName = "Тест на отсутствие не буквенных и нечисловых символов")]
        //public void NewPasswordNonAlphabethic()
        //{
        //    AssertFalseValidation(_ => _.NewPassword = "Qwertyy7");
        //}

        //#region [ Help methods ]

        //private Mock<IUnitOfWork> MockUfw()
        //{
        //    var identityAppSettingsRepoMock = new Mock<IRepository<SettingEntity>>();
        //    identityAppSettingsRepoMock.Setup(_ => _.Query)
        //        .Returns(
        //            new TestAsyncEnumerable<SettingEntity>(
        //                Helper.GetAppSettings(new PasswordPolicy())));

        //    identityAppSettingsRepoMock.Setup(_ =>
        //            _.GetAsync(It.IsAny<Expression<Func<SettingEntity, bool>>>(),
        //                CancellationToken.None))
        //        .Returns(Task.FromResult(Helper.GetAppSettings(new PasswordPolicy()
        //        {
        //            RequireNonAlphanumeric = true
        //        })));

        //    var userRepo = new Mock<IRepository<User>>();
        //    userRepo.Setup(_ => _.Query)
        //        .Returns(GetExistedUsers());

        //    var ufwMock = new Mock<IUnitOfWork>();
        //    ufwMock.Setup(_ => _.Repository<SettingEntity>())
        //        .Returns(identityAppSettingsRepoMock.Object);
        //    ufwMock.Setup(_ => _.IsTableExists(It.IsAny<string>()))
        //        .Returns(true);
        //    ufwMock.Setup(_ => _.Repository<User>())
        //        .Returns(userRepo.Object);

        //    return ufwMock;
        //}

        //private IQueryable<User> GetExistedUsers()
        //{
        //    return new TestAsyncEnumerable<User>(new List<User>
        //    {
        //        new User
        //        {
        //            UserName = "test_user1",
        //            CreatedAt = DateTimeOffset.Now,
        //            Description = "test_user1",
        //            Id = new Guid(1,1,1,1,1,11,1,1,1,1,1).ToString(),
        //            UpdatedAt = DateTimeOffset.Now
        //        },
        //        new User
        //        {
        //            UserName = "test_user2",
        //            CreatedAt = DateTimeOffset.Now,
        //            Description = "test_user2",
        //            Id = new Guid(2,2,2,2,2,2,2,2,2,2,2).ToString(),
        //            UpdatedAt = DateTimeOffset.Now
        //        },
        //        new User
        //        {
        //            UserName = "test_user3",
        //            CreatedAt = DateTimeOffset.Now,
        //            Description = "test_user3",
        //            Id = new Guid(3,3,3,3,3,3,3,3,3,3,3).ToString(),
        //            UpdatedAt = DateTimeOffset.Now,
        //            IsDefaultUser = true
        //        }
        //    });
        //}

        //private void AssertFalseValidation(Action<ChangePasswordForUserCommand> action)
        //{
        //    var ufwMock = MockUfw();
        //    var validator = new ChangePasswordForUserValidator(
        //        Helper.GetValidationLocalizer(),
        //        new IdentityAppSettings(ufwMock.Object), ufwMock.Object,
        //        null);

        //    var command = new ChangePasswordForUserCommand
        //    {
        //        NewPassword = "$Qwerty123456",
        //        UserId = "bb5210a0-12a6-11eb-adc1-0242ac120002"
        //    };

        //    validator.AssertFalseValidator(command);
        //}

        //#endregion
    }
}
