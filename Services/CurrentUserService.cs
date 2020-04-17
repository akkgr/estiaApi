using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace estiaApi.Services
{
    public class CurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Id = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue("name");
        }

        public string Id { get; }
        public string Name { get; }
    }
}
