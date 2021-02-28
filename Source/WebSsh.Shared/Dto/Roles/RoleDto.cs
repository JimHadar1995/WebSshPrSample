namespace WebSsh.Shared.Dto.Roles
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// 
    /// </summary>
    public sealed record RoleDto
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; init; }
    }
}
