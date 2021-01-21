using Microsoft.Extensions.Localization;

namespace Library.Common.Localization
{
    /// <summary>
    /// Общий интерфейс локализатора для Security center
    /// </summary>
    public interface IOwnLocalizer<T> : IStringLocalizer
        where T : class
    {
        
    }
}
