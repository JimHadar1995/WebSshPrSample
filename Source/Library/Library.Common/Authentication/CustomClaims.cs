namespace Library.Common.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CustomClaims
    {
        /// <summary>
        /// The roles claims
        /// </summary>
        public const string RolesClaim = "roles";

        /// <summary>
        /// The privileges claim
        /// </summary>
        public const string PrivilegesClaim = "privileges";

        /// <summary>
        /// The token identifier claim
        /// </summary>
        public const string TokenIdClaim = "token_id";
    }
}
