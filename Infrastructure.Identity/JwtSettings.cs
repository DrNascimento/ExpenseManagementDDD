using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = "SecretKey";
        public string Issuer { get; set; } = "Issuer";
        public string Audience { get; set; } = "Audience";
        public int ExpirationInMinutes { get; set; } = 120;
    }
}
