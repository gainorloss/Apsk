namespace Apsk.AspNetCore.Authentication
{
    /// <summary>
    /// Authentication manager.
    /// </summary>
    public interface IAuthenticationManager
    {
        /// <summary>
        /// Authenticate.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        AuthenticationToken Authenticate(string appKey, string appSecret);
    }
}
