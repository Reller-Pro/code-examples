/// <summary>
/// Startup class that configures services and the application's request pipeline
/// </summary>
public class Startup
{
    // Method that configures the services used by the application
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        // Add JWT authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Require HTTPS metadata and save the token
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                // Configure token validation parameters
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    // Set the valid issuer and audience from appsettings.json
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    // Set the symmetric security key from appsettings.json
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

        // Other service configurations

        // Return the service provider
        return services.BuildServiceProvider();
    }

    // Method that configures the application's request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Enable authentication middleware
        app.UseAuthentication();

        // Other app configurations
    }
}