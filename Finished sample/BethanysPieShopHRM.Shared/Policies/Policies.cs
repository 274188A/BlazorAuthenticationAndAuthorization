using Microsoft.AspNetCore.Authorization;

namespace BethanysPieShopHRM.Shared.Policies
{
    public static class Policies
    {
        public const string CanManageEmployees = "CanManageEmployees";

        public static AuthorizationPolicy CanManageEmployeesPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("country", "BE")
                .Build();
        }
    }
}
