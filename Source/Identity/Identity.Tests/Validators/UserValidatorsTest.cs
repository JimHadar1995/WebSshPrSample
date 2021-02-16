using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace SC.Identity.Tests.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class UserValidatorsTest
    {
        //#region [ CreateUserValidator ]

        //[Fact(DisplayName = "Проверка работы валидатора при создании пользователя с корректными данными")]
        //public void PositiveCreateUserValidatorTest()
        //{
        //    var ufwMock = GetMockedUfw();
        //    var user = GetNewUser();
        //    var settings = GetMockedAppSettings();

        //    var validator = new CreateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer(), settings, null);
        //    var validationResult = validator.Validate(user);
        //    Assert.True(validationResult.IsValid);
        //}

        //[Fact(DisplayName = "Проверка максимальной длины описания в 250 символов для валидатора создания")]
        //public void DescriptionMax250CreateUserValidatorTest()
        //{
        //    TestFailureCreateValidator(_ =>
        //        _.Description =
        //            "testtesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttes");
        //}

        //[Fact(DisplayName = "Проверка максимальной длины имени пользователя в 50 символов для валидатора создания")]
        //public void UserNameMax50CreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ =>
        //        _.UserName =
        //            "testtesttesttesttesttesttesttesttesttesttesttesttest");
        //}

        //[Fact(DisplayName = "Проверка на непустоту имени пользователя для валидатора создания")]
        //public void UserNameNotEmptyCreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ =>
        //        _.UserName =
        //            "");
        //}

        //[Fact(DisplayName = "Проверка на то, что имя пользователя не должно содержать кириллицу для валидатора создания")]
        //public void UserNameSpecSymbolsCreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ =>
        //        _.UserName =
        //            "выфвфывфы");
        //    TestFailureCreateValidator(_ =>
        //        _.UserName =
        //            "!@#$");
        //}

        //[Fact(DisplayName = "Проверка на то, что имя пользователя должно быть уникальным для валидатора создания")]
        //public void UserNameUniqueCreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ =>
        //        _.UserName =
        //            "test_user1");
        //    TestFailureCreateValidator(_ =>
        //        _.UserName =
        //            "test_user2");
        //}

        //[Fact(DisplayName = "Проверка на непустой список ролей для пользователя для валидатора создания")]
        //public void RolesCreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ => _.RoleIds.Clear());
        //}

        //[Fact(DisplayName = "Проверка парольных политик для валидатора создания")]
        //public void PasswordUserCreateValidatorTests()
        //{
        //    //пароль не может быть пустым
        //    TestFailurePassword(
        //        _ => _.Password = "");
        //    //маленькая длина (должна быть 8-50)
        //    TestFailurePassword(
        //        _ => _.Password = "1");
        //    //длина пароля верная, но нет символов в верхем регистре
        //    TestFailurePassword(_ => _.Password = "qwerty789");
        //    //нет цифр
        //    TestFailurePassword(_ => _.Password = "Qwertydasdklasdlasdk");
        //    //нет нечисловых и небуквенных символов
        //    TestFailurePassword(_ => _.Password = "Qwerty789");
        //    //нет символов в нижем регистре
        //    TestFailurePassword(_ => _.Password = "qwerty789!");
        //}

        //[Fact(DisplayName = "Проверка на попытку присовить пользователю несуществующую группу пользователей для валидатора создания")]
        //public void InvalidGroupIdCreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ => _.GroupId = 12);
        //}

        //#endregion

        //#region [ UpdateUserValidato ]

        //[Fact(DisplayName = "Проверка работы валидатора при обновлении пользователя с корректными данными для валидатора обновления")]
        //public void PositiveUpdateUserValidatorTest()
        //{
        //    var ufwMock = GetMockedUfw();
        //    var user = GetUserForUpdate();

        //    var validator = new UpdateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer());
        //    var validationResult = validator.Validate(user);
        //    Assert.True(validationResult.IsValid);
        //}

        //[Fact(DisplayName = "Проверка на попытку изменения пользователя по умолчанию для валидатора обновления")]
        //public void DefaultUserDeniedUpdateUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ =>
        //    {
        //        _.Id = GetExistedUsers().Last().Id;
        //        _.UserName = "test_user111";
        //    });
        //}

        //[Fact(DisplayName = "Проверка максимальной длины описания в 250 символов для валидатора обновления")]
        //public void DescriptionMax250UpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ =>
        //        _.Description =
        //            "testtesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttes");
        //}

        //[Fact(DisplayName = "Проверка максимальной длины имени пользователя в 50 символов для валидатора обновления")]
        //public void UserNameMax50UpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ =>
        //        _.UserName =
        //            "testtesttesttesttesttesttesttesttesttesttesttesttest");
        //}

        //[Fact(DisplayName = "Проверка на непустоту имени пользователя для валидатора обновления")]
        //public void UserNameNotEmptyUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ =>
        //        _.UserName =
        //            "");
        //}

        //[Fact(DisplayName = "Проверка на то, что имя пользователя не должно содержать кириллицу для валидатора обновления")]
        //public void UserNameSpecSymbolsUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ =>
        //        _.UserName =
        //            "выфвфывфы");
        //    TestFailureUpdateValidator(_ =>
        //        _.UserName =
        //            "!@#$");
        //}

        //[Fact(DisplayName = "Проверка на то, что имя пользователя должно быть уникальным для валидатора обновления")]
        //public void UserNameUniqueUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ =>
        //        _.UserName =
        //            GetExistedUsers().Last().UserName);
        //    TestFailureUpdateValidator(_ =>
        //        _.UserName =
        //            "test_user2");
        //}

        //[Fact(DisplayName = "Проверка на непустой список ролей для пользователя для валидатора обновления")]
        //public void RolesUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ => _.RoleIds.Clear());
        //}

        //[Fact(DisplayName = "Проверка на попытку присовить пользователю несуществующую группу пользователей для валидатора обновления")]
        //public void InvalidGroupIdUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ => _.GroupId = 12);
        //}

        //#endregion
        
        //#region [ Help methods ]

        //private CreateUserCommand GetNewUser()
        //{
        //    return new CreateUserCommand
        //    {
        //        Description = "test_user",
        //        UserName = "test_user",
        //        RoleIds = new List<string> { GetRolesQ().First().Id },
        //        Password = "$Qwerty123456"
        //    };
        //}

        //private UpdateUserCommand GetUserForUpdate()
        //{
        //    var user = GetExistedUsers().First();
        //    return new UpdateUserCommand
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        Description = user.Description,
        //        RoleIds = new List<string> { GetRolesQ().First().Id }
        //    };
        //}

        //private IQueryable<UserRole> GetRolesQ()
        //{
        //    return new TestAsyncEnumerable<UserRole>(new List<UserRole>
        //    {
        //        new UserRole
        //        {
        //            Id = new Guid(1,1,1,1,1,1,1,1,1,1,1).ToString(),
        //            Description = "role1",
        //            IsDefaultRole = false,
        //            Name = "role1",
        //        },
        //        new UserRole
        //        {
        //            Id = new Guid(2,2,2,2,2,2,2,2,2,2,2).ToString(),
        //            Description = "role2",
        //            IsDefaultRole = false,
        //            Name = "role2",
        //        },
        //    });
        //}

        //private IQueryable<User> GetExistedUsers()
        //{
        //    return new TestAsyncEnumerable<User>(new List<User>
        //    {
        //        new User
        //        {
        //             UserName = "test_user1",
        //             CreatedAt = DateTimeOffset.Now,
        //             Description = "test_user1",
        //             Id = new Guid(1,1,1,1,1,11,1,1,1,1,1).ToString(),
        //             UpdatedAt = DateTimeOffset.Now
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

        //private List<SettingEntity> GetPasswordPolicySettings()
        //{
        //    var defaultPasswordPolicy = new PasswordPolicy
        //    {
        //        RequireNonAlphanumeric = true
        //    };

        //    var properties = typeof(PasswordPolicy).GetProperties();

        //    return properties.Select(propertyInfo =>
        //        new SettingEntity
        //        {
        //            Type = nameof(PasswordPolicy),
        //            Name = propertyInfo.Name,
        //            Value = propertyInfo.GetValue(defaultPasswordPolicy)?.ToString()
        //        }).ToList();
        //}

        //private Mock<IUnitOfWork> GetMockedUfw()
        //{
        //    var userRoleMock = new Mock<IRepository<UserRole>>();
        //    userRoleMock.Setup(_ => _.Query)
        //        .Returns(GetRolesQ());

        //    var userRepoMock = new Mock<IRepository<User>>();
        //    userRepoMock.Setup(_ => _.Query)
        //        .Returns(GetExistedUsers());

        //    var userGroupMockRepo = new Mock<IRepository<Group>>();
        //    userGroupMockRepo.Setup(_ => _.Query)
        //        .Returns(new TestAsyncEnumerable<Group>(new List<Group>()));

        //    var settingsMockRepo = new Mock<IRepository<SettingEntity>>();
        //    settingsMockRepo.Setup(_ =>
        //            _.GetAsync(It.IsAny<Expression<Func<SettingEntity, bool>>>(), CancellationToken.None))
        //        .Returns(Task.FromResult(GetPasswordPolicySettings()));

        //    var ufwMock = new Mock<IUnitOfWork>();
        //    ufwMock.Setup(_ => _.Repository<UserRole>())
        //        .Returns(userRoleMock.Object);
        //    ufwMock.Setup(_ => _.Repository<User>())
        //        .Returns(userRepoMock.Object);
        //    ufwMock.Setup(_ => _.Repository<Group>())
        //        .Returns(userGroupMockRepo.Object);
        //    ufwMock.Setup(_ => _.Repository<SettingEntity>())
        //        .Returns(settingsMockRepo.Object);
        //    ufwMock.Setup(_ => _.IsTableExists(It.IsAny<string>()))
        //        .Returns(true);

        //    return ufwMock;
        //}

        //private IdentityAppSettings GetMockedAppSettings()
        //{
        //    var ufwMock = GetMockedUfw();

        //    return new IdentityAppSettings(ufwMock.Object);
        //}

        //private void TestFailureCreateValidator(Action<CreateUserCommand> action)
        //{
        //    var ufwMock = GetMockedUfw();
        //    var user = GetNewUser();
        //    action.Invoke(user);

        //    var validator = new CreateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer(), GetMockedAppSettings(), null);
        //    validator.AssertFalseValidator(user);
        //}

        //private void TestFailureUpdateValidator(Action<UpdateUserCommand> action)
        //{
        //    var ufwMock = GetMockedUfw();
        //    var user = GetUserForUpdate();
        //    action.Invoke(user);

        //    var validator = new UpdateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer());
        //    validator.AssertFalseValidator(user);
        //}

        //private void TestFailurePassword(Action<CreateUserCommand> action)
        //{
        //    var ufwMock = GetMockedUfw();
        //    var user = GetNewUser();
        //    action.Invoke(user);

        //    var validator = new CreateUserValidator(ufwMock.Object, Helper.GetValidationLocalizer(), GetMockedAppSettings(), null);
        //    validator.AssertFalseValidatorWithoutCheckCountErrors(user);
        //}

        //#endregion
    }
}
