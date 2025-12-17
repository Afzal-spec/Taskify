namespace Taskify.Api.Helpers
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            var claim = httpContext.User.Claims
                .FirstOrDefault(c => c.Type == "userid");

            return claim != null ? int.Parse(claim.Value) : 0;
        }
    }
}
