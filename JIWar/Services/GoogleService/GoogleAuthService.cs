using Google.Apis.Auth;

namespace Jiwar.Services.GoogleService
{
    public class GoogleAuthService
    {
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                return payload;
            }
            catch
            {
                return null;
            }
        }
    }
}
