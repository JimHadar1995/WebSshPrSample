using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Dto
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
