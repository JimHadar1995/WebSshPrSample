using Identity.ResourceManager.Validation;
using Library.Common.Localization;

namespace Identity.ResourceManager.Implementations
{
    /// <inheritdoc />
    public sealed class ValidationLocalizer : IValidationLocalizer
    {
        private readonly IOwnLocalizer<ValidationConstants> _localizer;
        private readonly IOwnLocalizer<FieldNameConstants> _fieldNameLocalizer;
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationLocalizer"/> class.
        /// </summary>
        /// <param name="localizer">The localizer.</param>
        /// <param name="fieldNameLocalizer">The field name localizer.</param>
        public ValidationLocalizer(
            IOwnLocalizer<ValidationConstants> localizer,
            IOwnLocalizer<FieldNameConstants> fieldNameLocalizer)
        {
            _localizer = localizer;
            _fieldNameLocalizer = fieldNameLocalizer;
        }

        /// <inheritdoc />
        public string Name(string fieldName)
            => _fieldNameLocalizer[fieldName].Value;

        /// <inheritdoc />
        public string Message(string messageTemplate, params object[] args)
            => _localizer[messageTemplate, args].Value;

        /// <inheritdoc />
        public string Message(string messageTemplate)
            => _localizer[messageTemplate].Value;
    }
}
