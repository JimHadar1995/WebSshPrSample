using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Dto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Модель создания роли
    /// </summary>
    public record RoleAddDto
    {
        /// <summary>
        /// Имя роли
        /// </summary>
        [Required]
        public string Name { get; init; }

        /// <summary>
        /// Отображаемое название роли
        /// </summary>
        [Required]
        public string Description { get; init; }

        /// <summary>
        /// Привилегии для роли.
        /// </summary>
        [Required]
        public List<PrivilegeForRoleDto> Privileges { get; init; }
            = new List<PrivilegeForRoleDto>();

        /// <summary>
        /// 
        /// </summary>
        public sealed record PrivilegeForRoleDto
        {
            /// <summary>
            /// Идентификатор привилегии.
            /// </summary>
            [Required]
            public int PrivilegeId { get; init; }
        }
    }
}
