using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Application.Responses;
using Application.Localization;

namespace Presentation.Attributes;

[AttributeUsage(AttributeTargets.All)]
public sealed class ValidateTokenAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (hasAllowAnonymous)
        {
            return;
        }

        var tokenService = (context.HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService)!;
        var tokenResolverService = (context.HttpContext.RequestServices.GetService(typeof(ITokenResolverService)) as ITokenResolverService)!;

        string accessToken = tokenResolverService.GetAccessToken();

        var isValidToken = tokenService.IsValidAccessTokenAsync(accessToken).Result;

        if (!isValidToken)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResult(EMessages.UnAuthorizedAccess.Translate()));
        }
    }
}
