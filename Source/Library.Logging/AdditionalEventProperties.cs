namespace Library.Logging
{
    sealed class AdditionalEventProperties
    {
        public AdditionalEventProperties(string? user)
        {
            User = user ?? "";
        }
        /// <summary>
        /// User name for logging
        /// </summary>
        public string User { get; }
    }
}
