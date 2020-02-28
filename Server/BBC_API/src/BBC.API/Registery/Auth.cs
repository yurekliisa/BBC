using BBC.Core.Configuration.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC.API.Registery
{
    public static class Auth
    {
        public static void UseAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            ///--- IMPORTANT : IdentityService AuthorizeServiceden önce olmalı jwt token için
            services.AddTransient(typeof(TokenValidationParameters));


            SymmetricSecurityKey signingKey =
                   new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration[ConfigurationKeys.Jwt + ":Secret"].ToString()));

            string Issuer = Configuration[ConfigurationKeys.Jwt + ":Issuer"].ToString();
            string Audience = Configuration[ConfigurationKeys.Jwt + ":Audience"].ToString();
            SigningCredentials SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Issuer,

                ValidateAudience = true,
                ValidAudience = Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = Issuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

        }

        public static void AuthorizeBuilder(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
