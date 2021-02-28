using System.ComponentModel.DataAnnotations;

namespace WebSsh.Shared.Dto.Users
{
    /// <summary>
    /// 
    /// </summary>
    public record UserUpdateDto : UserAddDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [Required]
        public int Id { get; init; }
    }
}
