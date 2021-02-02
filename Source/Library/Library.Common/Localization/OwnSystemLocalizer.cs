using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Library.Common.Localization
{
    /// <inheritdoc cref="IScSystemLocalizer"/>
    public sealed class OwnSystemLocalizer<T> : OwnLocalizer<T>, IOwnSystemLocalizer<T>
        where T : class
    {
        private readonly string _systemLocale;
        /// <inheritdoc />
        public OwnSystemLocalizer(
            IStringLocalizerFactory factory,
            IConfiguration config) : base(factory)
        {
            _systemLocale = config.GetValue<string>("Locale") ?? "ru";
        }

        //https://github.com/dotnet/aspnetcore/issues/7756
        //изменение культуры для потока
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public override LocalizedString this[string name]
        {
            get
            {
                try
                {
                    SetCurrentCulture(_systemLocale);
                    return base[name];
                }
                finally
                {
                    ResetCurrentCulture();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        public override LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                try
                {
                    SetCurrentCulture(_systemLocale);
                    return base[name, arguments];
                }
                finally
                {
                    ResetCurrentCulture();
                }
            }
        }

        private CultureInfo? _originalCulture;

        private void SetCurrentCulture(string culture)
        {
            _originalCulture = CultureInfo.CurrentCulture;
            var cultureInfo = String.IsNullOrEmpty(culture) ? CultureInfo.CurrentCulture : new CultureInfo(culture);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }

        private void ResetCurrentCulture()
        {
            CultureInfo.CurrentCulture = _originalCulture!;
            CultureInfo.CurrentUICulture = _originalCulture!;
        }
    }
}
