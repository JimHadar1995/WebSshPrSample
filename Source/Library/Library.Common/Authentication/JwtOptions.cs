using System;
using System.Threading.Tasks;

namespace Library.Common.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        /// <value>
        /// The secret key.
        /// </value>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>
        /// The issuer.
        /// </value>
        public string Issuer { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the expiry minutes.
        /// </summary>
        /// <value>
        /// The expiry minutes.
        /// </value>
        public int ExpiryMinutes { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [validate lifetime].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [validate lifetime]; otherwise, <c>false</c>.
        /// </value>
        public bool ValidateLifetime { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [validate audience].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [validate audience]; otherwise, <c>false</c>.
        /// </value>
        public bool ValidateAudience { get; set; }

        /// <summary>
        /// Gets or sets the valid audience.
        /// </summary>
        /// <value>
        /// The valid audience.
        /// </value>
        public string ValidAudience { get; set; } = string.Empty;

        /// <summary>
        /// 4.1.5.  "nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
        /// </summary>
        public DateTime NotBefore => DateTime.UtcNow;

        /// <summary>
        /// 4.1.6.  "iat" (Issued At) Claim - The "iat" (issued at) claim identifies the time at which the JWT was issued.
        /// </summary>
        public DateTime IssuedAt => DateTime.UtcNow;

        /// <summary>
        /// "jti" (JWT ID) Claim (default ID is a GUID)
        /// </summary>
        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());
    }
}
