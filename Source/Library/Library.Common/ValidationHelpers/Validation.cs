using System.Linq;
using System.Text.RegularExpressions;

namespace Library.Common.ValidationHelpers
{
    /// <summary>
    /// методы, используемые в fluent валидации
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Проверка названия сущности на корректность:
        /// - содержит только буквы или цифры латинского алфавита;
        /// - содержит знаки "-", "_"
        /// </summary>
        /// <param name="objectName">Строка для проверки</param>
        /// <returns>Результат проверки</returns>
        public static bool ObjectNameIsValid(string? objectName) =>
            !string.IsNullOrEmpty(objectName) &&
            Regex.IsMatch(objectName, "^([a-zA-Z0-9_-]*)$") ||
            string.IsNullOrWhiteSpace(objectName);

        /// <summary>
        /// Проверка названия сущности, что она содержит только латинские буквы или цифры
        /// </summary>
        /// <param name="objectName">Строка для проверки</param>
        /// <returns>Результат проверки</returns>
        public static bool ContainsOnlyLatin(string? objectName) =>
            !string.IsNullOrEmpty(objectName) &&
            Regex.IsMatch(objectName, "^([a-zA-Z0-9]*)$");

        /// <summary>
        /// Проверка наличия символов в верхнем регистре
        /// </summary>
        /// <param name="str">Строка для проверки</param>
        /// <returns>Флаг наличия</returns>
        public static bool ContainsUpperCase(string? str) => string.IsNullOrEmpty(str) || str.Any(char.IsUpper);

        /// <summary>
        /// Проверка наличия символов в верхнем регистре
        /// </summary>
        /// <param name="str">Строка для проверки</param>
        /// <returns>Флаг наличия</returns>
        public static bool ContainsLowerCase(string? str) => string.IsNullOrEmpty(str) || str.Any(char.IsLower);

        /// <summary>
        /// Проверка наличия нечисловых и нецифровых символов в верхнем регистре
        /// </summary>
        /// <param name="str">Строка для проверки</param>
        /// <returns>Флаг наличия</returns>
        public static bool ContainsNonAlphaNumeric(string? str) => string.IsNullOrEmpty(str) || !str.All(char.IsLetterOrDigit);

        /// <summary>
        /// Проверка наличия цифр
        /// </summary>
        /// <param name="str">Строка для проверки</param>
        /// <returns>Флаг наличия</returns>
        public static bool ContainsDigits(string? str) => string.IsNullOrEmpty(str) || str.Any(char.IsDigit);

        /// <summary>
        /// Валидация Логина пользователя:
        /// </summary>
        /// <remarks>
        /// Верным считается имя пользователя, которое содержит только латинские буквы и цифры
        /// </remarks>
        /// <param name="userName"></param>
        /// <returns>Результат проверки корректности имени пользователя</returns>
        public static bool UserNameIsValid(string? userName) =>
            ObjectNameIsValid(userName);

        /// <summary>
        /// Проверка строки на то, что она содержит кириллические буквы
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static bool NotContainsCyrillic(string? template)
        {
            if (string.IsNullOrEmpty(template))
            {
                return true;
            }
            string pattern = "[а-яА-Я]";
            return !Regex.IsMatch(template, pattern);
        }

    }
}
