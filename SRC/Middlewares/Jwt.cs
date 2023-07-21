using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace server.SRC.Middlewares
{
    public class JWTMiddleware
    {
        private readonly string _secretKey;

        public JWTMiddleware()
        {
            _secretKey = "my-AI-key-asdfajsdjfhasjhfjashfkljhsajdfhasjfhd";
        }
        public string GenerateToken(string accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var expirationTime = DateTime.UtcNow.AddMonths(3);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", accountId),

                }),
                Expires = DateTime.UtcNow.AddMonths(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string accessToken)
        {
            if (accessToken.StartsWith("Bearer ") == false)
            {
                return null;
            }
            var token = accessToken.Split(" ")[1];
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return principal;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public String ExtractAccountId(string token)
        {
            var principal = this.GetPrincipalFromToken(token);
            var accountIdClaim = principal.FindFirst("id");
            return accountIdClaim.Value;
        }
    }
}