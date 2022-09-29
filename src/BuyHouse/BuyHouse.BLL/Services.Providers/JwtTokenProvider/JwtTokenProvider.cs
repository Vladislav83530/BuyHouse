using BuyHouse.DAL.EF;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace BuyHouse.BLL.Services.Providers.JwtTokenProvider
{
    //TODO: configuration
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly ApplicationDbContext _context;
        public JwtTokenProvider(ApplicationDbContext context)
        {
            _context = context;
        }
       
        private static string SECRET_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBsaWNhdGlvblNlY3JldEtleSI6Ik15QXBwbGljYXRpb25TZWNyZXRLZXkifQ.HkGrLPt2lTMpywpbFf1mqAq8Hl5qgenFxA337xEUno4";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

        public async Task<string> ProvideJwtToken(string? currentUserId)
        {
            if (String.IsNullOrEmpty(currentUserId))
                throw new Exception("User id can't be null");

            var currentUser = await _context.Users.FindAsync(currentUserId);

            if (currentUser == null)
                throw new Exception("Not found current user");

            var credentials = new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);

            DateTime Expiry = DateTime.UtcNow.AddMinutes(1);
            int ts = (int)(Expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new JwtPayload
            {
                {"name", currentUser.Name },
                {"email", currentUser.Email },
                {"exp", ts },
                { "iss","https://localhost:7122" },
                { "aud","https://localhost:7021"}
            };

            var secToken = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }
    }
}
