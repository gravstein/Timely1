namespace Timely1.Extensions
{
    // authorize policies
    public static class AddPoliciesExtension
    {
        public static void AddPolicies(this IServiceCollection services) 
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperRights", policy =>
                {
                    policy.RequireRole("Admin", "Manager"); // OR logic
                });

                options.AddPolicy("SuperMegaRights", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });
        }
    }
}
