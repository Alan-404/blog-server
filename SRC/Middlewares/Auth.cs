using server.SRC.Utils;

namespace server.SRC.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTMiddleware _jwt;

        public AuthMiddleware(RequestDelegate next)
        {
            this._next = next;
            this._jwt = new JWTMiddleware();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(RequestHeader.AUTHORIZATION_HEADER, out var token))
            {
                string accountId = this._jwt.ExtractAccountId(token);

                if (accountId == null)
                {
                    await _next(context);
                }
                context.Request.Headers.Add(RequestHeader.AUTH_HEADER, accountId);
                await _next(context);
            }
            else
            {
                await _next(context);
            }
        }
    }
}