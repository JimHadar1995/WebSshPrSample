namespace Identity.Application.Dto.Roles
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// 
    /// </summary>
    public sealed record PrivilegesLoadDto
    {
        /// <summary>
        /// Название на латинице
        /// </summary>

        public string Name { get; init; }

        /// <summary>
        /// Отображаемое описание
        /// </summary>
        public string Description { get; init; }
    }
}
