namespace Identity.Application.Dto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Привилегия
    /// </summary>
    public sealed record PrivilegeDto
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
