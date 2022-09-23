using BuyHouse.DAL.EF;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace BuyHouse.BLL.Services.Providers.JwtTokenProvider
{
    public class JwtTokenProvider : IJwtTokenProvider
    {

        //TODO: secret key 
        private const string SECRET_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJNeUFwcGxpY2F0aW9uQnV5SG91c2UiLCJuYW1lIjoiMjM0MzIzMjQzMiIsImlhdCI6MTUxNjIzOTAyMn0.5sXQL4Z-K5HPQYIRU7B4ZEL5XS7H1-lx-c9ylbJlXps";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

        private readonly ApplicationDbContext _context;
        public JwtTokenProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        //currentUser == null exception 
        public async Task<string> GenerateJwtToken(string? currentUserId)
        {
            //if(currentUserId == null)

            var currentUser = await _context.Users.FindAsync(currentUserId);

            //if (currentUser == null)
            //    return null;

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
