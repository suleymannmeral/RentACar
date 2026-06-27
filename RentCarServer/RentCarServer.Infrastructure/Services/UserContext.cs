using Microsoft.AspNetCore.Http;
using RentCarServer.Application.Services;
using System.Security.Claims;

namespace RentCarServer.Infrastructure.Services;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor):IUserContext
{
    public Guid GetUserId()
    {

        var httpContext = httpContextAccessor.HttpContext;
        var claims= httpContext.User.Claims;
        string? userId=claims.FirstOrDefault(i=>i.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
            throw new ArgumentNullException("Kullanıcı bilgisi bulunamadı!");

        try
        {
            Guid id = Guid.Parse(userId);
            return id;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Kullanıcı bilgisi geçersiz formatta!", ex);
        }

        // Implementation to retrieve the user ID from the context
        // For example, you might get it from the current HTTP context or a JWT token
        // Here, we will return a dummy value for demonstration purposes
    }
}
