using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Dto.Roles
{
    /// <summary>
    /// Модель обновления роли
    /// </summary>
    public record RoleUpdateDto : RoleAddDto
    {
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        [Required]
        public int Id { get; init; }
    }
}
