using System;
using System.Collections.Generic;
using System.Linq;
using Identity.Application.Dto.Roles;
using Identity.Core.Entities;
using Identity.Tests.Inner;
using Moq;
using Tests.Common;
using Xunit;

namespace SC.Identity.Tests.Validators
{
    public class RoleValidatorsTest
    {
        //[Fact(DisplayName = "Проверка работы валидатора обновления роли с корректными параметрами")]
        //public void PositiveUpdateValidatorTest()
        //{
        //    var ufwMock = GetMockedUfw();
        //    var role = GetRoleForUpdate();

        //    var validator = new UpdateRoleValidator(ufwMock.Object, Helper.GetValidationLocalizer());
        //    validator.AssertTrueValidator(role);
        //}

        //[Fact(DisplayName = "Проверка работы валидатора создания роли с корректными параметрами")]
        //public void PositiveCreateValidatorTest()
        //{
        //    var ufwMock = GetMockedUfw();
        //    var role = GetNewRole();

        //    var validator = new CreateRoleValidator(ufwMock.Object, Helper.GetValidationLocalizer());
        //    validator.AssertTrueValidator(role);
        //}

        //[Fact(DisplayName = "Проверка запрета обновления роли по умолчанию для валидатора обновления")]
        //public void CheckDefaultRoleUpdateValidatorTest()
        //{
        //    var ufwMock = GetMockedUfw();
        //    var role = GetRoles().Last();
        //    var updateRole = new UpdateRoleCommand
        //    {
        //        Id = role.Id,
        //        Name = role.Name,
        //        Description = role.Description,
        //        Privileges = new List<RoleAddDto.PrivilegeForRoleDto>
        //        {
        //            new RoleAddDto.PrivilegeForRoleDto
        //            {
        //                PrivilegeId = role.Privileges.First().PrivilegeId
        //            }
        //        }
        //    };

        //    var validator = new UpdateRoleValidator(ufwMock.Object, Helper.GetValidationLocalizer());
        //    validator.AssertFalseValidator(updateRole);
        //}

        //[Fact(DisplayName = "Проверка на пустое название роли для валидатора создания")]
        //public void EmptyNameCreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ => _.Name = "");
        //}

        //[Fact(DisplayName = "Проверка на пустое название роли для валидатора обновления")]
        //public void EmptyNameUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ => _.Name = "");
        //}

        //[Fact(DisplayName = "Проверка максимальной длины в 50 символов для имени роли валидатора создания")]
        //public void Max50LengthNameCreateValidatorTest()
        //{
        //    //51 символ
        //    TestFailureCreateValidator(_ => _.Name = "testtesttesttesttesttesttesttesttesttesttesttesttes");
        //}

        //[Fact(DisplayName = "Проверка максимальной длины в 50 символов для имени роли валидатора обновления")]
        //public void Max50LengthNameUpdateValidatorTest()
        //{
        //    //51 символ
        //    TestFailureUpdateValidator(_ => _.Name = "testtesttesttesttesttesttesttesttesttesttesttesttes");
        //}

        //[Fact(DisplayName = "Проверка уникальности имени роли валидатора создания")]
        //public void UniqueNameCreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ => _.Name = "role1");
        //}

        //[Fact(DisplayName = "Проверка уникальности имени роли валидатора обновления")]
        //public void UniquehNameUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ => _.Name = "role2");
        //}

        //[Fact(DisplayName = "Проверка максимальной длины в 250 символов для имени роли валидатора создания")]
        //public void Max250LengthDescriptionCreateValidatorTest()
        //{
        //    //251 символ
        //    TestFailureCreateValidator(_ => _.Description = "testtesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttes");
        //}

        //[Fact(DisplayName = "Проверка максимальной длины в 250 символов для имени роли валидатора обновления")]
        //public void Max250LengthDescriptionUpdateValidatorTest()
        //{
        //    //251 символ
        //    TestFailureUpdateValidator(_ => _.Description = "testtesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttestesttesttesttesttesttesttesttesttesttesttesttesttes");
        //}

        //[Fact(DisplayName = "Проверка на передачу пустого списка привилегий для валидатора создания")]
        //public void EmptyPrivilegesCreateValidatorTest()
        //{
        //    TestFailureCreateValidator(_ => _.Privileges = new List<RoleAddDto.PrivilegeForRoleDto>());
        //}

        //[Fact(DisplayName = "Проверка на передачу пустого списка привилегий для валидатора обновления")]
        //public void EmptyPrivilegesUpdateValidatorTest()
        //{
        //    TestFailureUpdateValidator(_ => _.Privileges = new List<RoleAddDto.PrivilegeForRoleDto>());
        //}

        //#region [ Help methods ]

        //private CreateRoleCommand GetNewRole()
        //{
        //    return new CreateRoleCommand
        //    {
        //        Name = "test_role",
        //        Description = "test_role",
        //        Privileges = new List<RoleAddDto.PrivilegeForRoleDto>
        //        {
        //            new RoleAddDto.PrivilegeForRoleDto
        //            {
        //                PrivilegeId = 1
        //            }
        //        }
        //    };
        //}

        //private UpdateRoleCommand GetRoleForUpdate()
        //{
        //    var role = GetRoles().First();
        //    return new UpdateRoleCommand
        //    {
        //        Id = role.Id,
        //        Name = role.Name,
        //        Description = role.Description,
        //        Privileges = new List<RoleAddDto.PrivilegeForRoleDto>
        //        {
        //            new RoleAddDto.PrivilegeForRoleDto
        //            {
        //                PrivilegeId = role.Privileges.First().PrivilegeId
        //            }
        //        }
        //    };
        //}

        //private List<UserRole> GetRoles()
        //{
        //    return new List<UserRole>
        //    {
        //        new UserRole
        //        {
        //            Id = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1).ToString(),
        //            Description = "role1",
        //            IsDefaultRole = false,
        //            Name = "role1",
        //            Privileges = new List<RolePrivilege>
        //            {
        //                new RolePrivilege
        //                {
        //                    PrivilegeId = GetPrivileges().First().Id,
        //                    Privilege = GetPrivileges().First()
        //                }
        //            }
        //        },
        //        new UserRole
        //        {
        //            Id = new Guid(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2).ToString(),
        //            Description = "role2",
        //            IsDefaultRole = false,
        //            Name = "role2",
        //            Privileges = new List<RolePrivilege>
        //            {
        //                new RolePrivilege
        //                {
        //                    PrivilegeId = GetPrivileges().First().Id,
        //                    Privilege = GetPrivileges().First()
        //                }
        //            }
        //        },
        //        new UserRole
        //        {
        //            Id = new Guid(3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3).ToString(),
        //            Description = "role3",
        //            IsDefaultRole = true,
        //            Name = "role3",
        //            Privileges = new List<RolePrivilege>
        //            {
        //                new RolePrivilege
        //                {
        //                    PrivilegeId = GetPrivileges().First().Id,
        //                    Privilege = GetPrivileges().First()
        //                }
        //            }
        //        },
        //    };
        //}

        //private IQueryable<Role> GetRolesQ()
        //{
        //    var roles = GetRoles();
        //    return new TestAsyncEnumerable<Role>(roles);
        //}

        //private IQueryable<Privilege> GetPrivileges()
        //{
        //    return new TestAsyncEnumerable<Privilege>(new List<Privilege>
        //    {
        //        new Privilege
        //        {
        //            Id = 1,
        //            Name = "test_pr1",
        //            Description = "test_pr1",
        //        },
        //        new Privilege
        //        {
        //            Id = 2,
        //            Name = "test_pr2",
        //            Description = "test_pr2",
        //        },
        //        new Privilege
        //        {
        //            Id = 3,
        //            Name = "test_pr3",
        //            Description = "test_pr3",
        //        }
        //    });
        //}

        //private Mock<IUnitOfWork> GetMockedUfw()
        //{
        //    var userRoleMock = new Mock<IRepository<UserRole>>();
        //    userRoleMock.Setup(_ => _.Query)
        //        .Returns(GetRolesQ());

        //    var privilegesRepoMock = new Mock<IRepository<Privilege>>();
        //    privilegesRepoMock.Setup(_ => _.Query)
        //        .Returns(GetPrivileges());

        //    var ufwMock = new Mock<IUnitOfWork>();
        //    ufwMock.Setup(_ => _.Repository<UserRole>())
        //        .Returns(userRoleMock.Object);
        //    ufwMock.Setup(_ => _.Repository<Privilege>())
        //        .Returns(privilegesRepoMock.Object);

        //    return ufwMock;
        //}

        //private void TestFailureCreateValidator(Func<RoleAddDto, RoleAddDto> func)
        //{
        //    var ufwMock = GetMockedUfw();
        //    var role = GetNewRole();
        //    role = func(role);

        //    var validator = new CreateRoleValidator(ufwMock.Object, Helper.GetValidationLocalizer());
        //    validator.AssertFalseValidator(role);
        //}

        //private void TestFailureUpdateValidator(Func<RoleUpdateDto, RoleUpdateDto> func)
        //{
        //    var ufwMock = GetMockedUfw();
        //    var role = GetRoleForUpdate();
        //    role = func(role);

        //    var validator = new UpdateRoleValidator(ufwMock.Object, Helper.GetValidationLocalizer());
        //    validator.AssertFalseValidator(role);
        //}
        //#endregion
    }
}
