namespace Identity.Application.Dto.Users
{
    /// <summary>
    /// Парольные политики
    /// </summary>
    public sealed record PasswordPolicyDto
    {
        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        public int MinimumLength { get; init; }

        /// <summary>
        /// Максимальная длина пароля.
        /// </summary>
        public int MaximumLength { get; init; }

        /// <summary>
        /// Цифры обязательны
        /// </summary>
        public bool RequireDigit { get; init; }

        /// <summary>
        /// Нижний регистр обязателен
        /// </summary>
        public bool RequireLowercase { get; init; }

        /// <summary>
        /// Верхний регистр обязателен
        /// </summary>
        public bool RequireUppercase { get; init; }

        /// <summary>
        /// Нечисловые и нецифровые символы обязательны
        /// </summary>
        public bool RequireNonAlphanumeric { get; init; }

        /// <summary>
        /// Максимальное время действия пароля (в днях)
        /// </summary>
        public int RequiredMaxExpireTime { get; init; }

        /// <summary>
        /// После какого количества неуспешных попыток пользователь блокируется на DefaultLockoutTimeSpan <see cref="DefaultLockoutTimeSpan"/>
        /// минут
        /// </summary>
        public int MaxFailedAccessAttempts { get; init; }

        /// <summary>
        /// Указывает, на какое количество минут блокируется пользователь в случае неверного ввода пароля MaxFailedAccessAttempts <see cref="MaxFailedAccessAttempts"/> раз 
        /// </summary>
        public int DefaultLockoutTimeSpan { get; init; }
    }
}
