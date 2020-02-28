using AutoMapper;
using BBC.Core.Configuration;
using BBC.Core.Dependency;
using BBC.Core.Domain.Identity;
using BBC.Core.IoC;
using BBC.Core.Permission;
using BBC.Services.Helper;
using BBC.Services.Identity.Dto;
using BBC.Services.Types.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBC.Services.Identity.Auth
{
    public class AuthenticationManager :IAuthenticationService, ITransientDI
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ConfigJWT _jwtSettings;
        private readonly IMapper _mapper;
        public AuthenticationManager()
        {
            _mapper = IoCManager.GetResolve<IMapper>();
            _userManager = IoCManager.GetResolve<UserManager<User>>();
            _roleManager = IoCManager.GetResolve<RoleManager<Role>>();
            _signInManager = IoCManager.GetResolve<SignInManager<User>>();
            _jwtSettings = IoCManager.GetResolve<ConfigJWT>();
            _tokenValidationParameters = IoCManager.GetResolve<TokenValidationParameters>();
        }

        #region Register
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual async Task<ResponseBase> Register(UserRegistrationRequest input, string password)
        {
            User existingUser = await _userManager.FindByEmailAsync(input.Email);

            if (existingUser != null)
            {
                return new AuthFailedResponse { ResultCode = HttpStatusCode.BadRequest, Success = false, Errors = new[] { "User with this email address already exists" } };
            }
            User user = _mapper.Map<User>(input);

            IdentityResult createdUser = await _userManager.CreateAsync(user, password);

            if (!createdUser.Succeeded)
            {
                return new AuthFailedResponse { ResultCode = HttpStatusCode.BadRequest, Success = false, Errors = createdUser.Errors.Select(x => x.Description) };
            }

            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            await _userManager.SetAuthenticationTokenAsync(user, "JWT", "RefreshToken", Guid.NewGuid().ToString());

            AuthenticationResult authenticationResult = new AuthenticationResult()
            {
                Success = true,
                ResultCode = HttpStatusCode.OK
            };
            return authenticationResult;
        }
        #endregion

        #region Login
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual async Task<ResponseBase> Login(string email, string password)
        {
            AuthenticationResult result = new AuthenticationResult();

            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthFailedResponse { ResultCode = HttpStatusCode.BadRequest, Success = false, Errors = new[] { "User does not exist" } };
            }

            bool userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthFailedResponse { ResultCode = HttpStatusCode.BadRequest, Success = false, Errors = new[] { "User/password combination is wrong" } };
            }

            string token = await GenerateAuthenticationResultForUserAsync(user);
            if (token != null)
            {
                result.Success = true;
                result.Token = token;
            }
            else
            {
                return new AuthFailedResponse { ResultCode = HttpStatusCode.BadRequest, Success = false, Errors = new List<string>() { "Not generate Token" } };
            }

            string getRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "JWT", "RefreshToken");
            if (getRefreshToken == null)
            {
                await _userManager.SetAuthenticationTokenAsync(user, "JWT", "RefreshToken", Guid.NewGuid().ToString());
            }



            await _userManager.AddLoginAsync(user, new UserLoginInfo("JWT", "API", user.UserName));

            await _signInManager.CreateUserPrincipalAsync(user);

            result.ResultCode = HttpStatusCode.OK;
            result.RefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "JWT", "RefreshToken");
            result.ExpiryDate = Convert.ToDateTime(_jwtSettings.TokenLifetime.ToString());
            result.UserInfo = new UserInfo()
            {
                UserName = user.UserName,
                Permissions = await GetUserPermissions(user)
            };
            return result;
        }
        #endregion

        #region RefreshToken   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual async Task<ResponseBase> RefreshToken(RefreshTokenRequest request)
        {
            AuthenticationResult result = new AuthenticationResult();

            System.Security.Claims.ClaimsPrincipal validatedToken = GetPrincipalFromToken(request.Token);

            if (validatedToken == null)
            {
                return new AuthFailedResponse { ResultCode = HttpStatusCode.BadRequest, Success = false, Errors = new[] { "Invalid Token" } };
            }

            long expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            DateTime expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthFailedResponse { ResultCode = HttpStatusCode.BadRequest, Success = false, Errors = new[] { "This token hasn't expired yet" } };
            }

            string jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            User user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);

            string token = await GenerateAuthenticationResultForUserAsync(user);

            string refreshToken = await _userManager.GetAuthenticationTokenAsync(user, "JWT", "RefreshToken");



            if (refreshToken != null)
            {
                result.Success = true;
                result.RefreshToken = refreshToken;
                result.ExpiryDate = Convert.ToDateTime("00:10:00");//_jwtSettings.GetSection("TokenLifetime")
                result.Token = token;
                result.UserInfo = new UserInfo()
                {
                    UserName = user.UserName,
                    Permissions = await GetUserPermissions(user)
                };
            }
            else
            {
                return new AuthFailedResponse { ResultCode = HttpStatusCode.BadRequest, Success = false, Errors = new[] { "This refresh token does not exist" } };
            }

            return result;
        }
        #endregion

        #region Helper
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                TokenValidationParameters tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<string> GenerateAuthenticationResultForUserAsync(User user)
        {
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, Utils.ToUnixEpochDate(DateTime.UtcNow).ToString(),
                  ClaimValueTypes.Integer64),
            };

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Count > 0)
            {

                foreach (string userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    Role role = await _roleManager.FindByNameAsync(userRole);
                    if (role == null)
                    {
                        continue;
                    }

                    IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(role);
                    if (roleClaims.Count > 0)
                    {
                        foreach (Claim roleClaim in roleClaims)
                        {
                            if (claims.Contains(roleClaim))
                            {
                                continue;
                            }

                            claims.Add(new Claim(roleClaim.Type, roleClaim.Value));
                        }
                    }
                }
            }
            string Audience = _jwtSettings.Audience;
            string Issuer = _jwtSettings.Issuer;
            JwtSecurityToken jwt = new JwtSecurityToken(
                 Issuer,
                 Audience,
                 claims,
                 DateTime.UtcNow,
                 DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),//_jwtSettings.TokenLifetime
                 new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));


            await _userManager.CreateSecurityTokenAsync(user);

            return tokenHandler.WriteToken(jwt);
        }
      
        private async Task<List<UserPermission>> GetUserPermissions(User user)
        {
            List<UserPermission> userPermissions = new List<UserPermission>();
            IList<string> userRole = await _userManager.GetRolesAsync(user);
            foreach (string role in userRole)
            {
                Role geRole = await _roleManager.FindByNameAsync(role);
                IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(geRole);
                foreach (Claim claim in roleClaims)
                {
                    if (!userPermissions.Any(x => x.PermissionType == claim.Type.ToString() && x.PermissionValue == claim.Value.ToString()))
                    {
                        userPermissions.Add(new UserPermission(PermissionBase.Permission, claim.Value.ToString()));
                    }
                }

            }

            return userPermissions;
        }

        #endregion
    }
}
