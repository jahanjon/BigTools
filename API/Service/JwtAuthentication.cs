using System.Net;
using System.Security.Claims;
using System.Text;
using Common.Enums;
using Common.Exceptions;
using Common.Settings;
using Common.Utilities;
using Domain.Entity.Identity;
using Domain.Repository.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Service;

public static partial class ServiceExtensions
{
    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>().JwtSettings;
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(settings.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(settings.EncryptKey);

            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero, // default: 5 min
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = true, //default : false
                ValidAudience = settings.Audience,

                ValidateIssuer = true, //default : false
                ValidIssuer = settings.Issuer,

                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                    //logger.LogError("Authentication failed.", context.Exception);

                    return context.Exception != null
                        ? throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.",
                            HttpStatusCode.Unauthorized, context.Exception, null)
                        : Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                    var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    if (claimsIdentity.Claims?.Any() != true)
                    {
                        context.Fail("This token has no claims.");
                    }

                    var securityStamp =
                        claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                    if (!securityStamp.HasValue())
                    {
                        context.Fail("This token has no security stamp");
                    }

                    //Find user and token from database and perform your custom validation
                    var userId = claimsIdentity.GetUserId<int>();
                    var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

                    //if (user.SecurityStamp != Guid.Parse(securityStamp))
                    //    context.Fail("Token security stamp is not valid.");

                    var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                    if (validatedUser == null)
                    {
                        context.Fail("Token security stamp is not valid.");
                    }

                    // if (!user.IsActive)
                    //     context.Fail("User is not active.");

                    await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                },
                OnChallenge = context =>
                {
                    //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                    //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

                    if (context.AuthenticateFailure != null)
                    {
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.",
                            HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                    }

                    throw new AppException(ApiResultStatusCode.UnAuthorized,
                        "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);

                    //return Task.CompletedTask;
                }
            };
        });
    }
}