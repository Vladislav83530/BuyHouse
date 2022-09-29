using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyHouse.BLL.Services.Providers.JwtTokenProvider
{
    public interface IJwtTokenProvider
    {
        public Task<string> ProvideJwtToken(string? currentUserId);
    }
}
