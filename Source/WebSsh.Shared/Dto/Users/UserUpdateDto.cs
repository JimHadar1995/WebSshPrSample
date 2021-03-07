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
        public int Id { get; init; }
    }
}
