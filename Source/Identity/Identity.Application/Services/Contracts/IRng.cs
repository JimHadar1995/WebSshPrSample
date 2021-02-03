namespace Identity.Application.Services.Contracts
{
    /// <summary>
    /// Генератор случайной полседовательности
    /// </summary>
    public interface IRng
    {
        /// <summary>
        /// Generates the specified length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="removeSpecialChars">if set to <c>true</c> [remove special chars].</param>
        /// <returns></returns>
        string Generate(int length = 50, bool removeSpecialChars = false);
    }
}
