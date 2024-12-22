namespace WebAPI.MIddlewares
{
    public class UserOwnershipMiddleware
    {
        private readonly RequestDelegate _next;

        public UserOwnershipMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (userIdClaim != null)
                {
                    context.Items["UserId"] = userIdClaim.Value;
                }
            }

            await _next(context);
        }
    }
}