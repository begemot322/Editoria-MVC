using Course_Work_Editoria.Services.Auth;
using Editoria.Application.Common.Interfaces.Services;
using Editoria.Application.Services.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;

namespace Editoria.Web.TagHelpers
{
    public class UserTagHelper : TagHelper
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheService _cacheService;
        public UserTagHelper(IUserService userService, IHttpContextAccessor httpContextAccessor,
             ICacheService cacheService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _cacheService = cacheService;
        }

        public string DefaultImageUrl { get; set; } = "/images/users/default_user.png";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst("userId")?.Value;

                var cacheKey = $"User_{userId}";

                var cachedUser = _cacheService.Get<(string ImageUrl, string UserName)>(cacheKey);

                if (cachedUser == default) // Если в кэше нет данных
                {
                    var userFromDb = await _userService.GetUserByIdAsync(Guid.Parse(userId));
                    cachedUser = (userFromDb.ImageUrl ?? DefaultImageUrl, userFromDb.UserName);

                    // Кэшируем на 10 минут
                    _cacheService.Set(cacheKey, cachedUser, TimeSpan.FromMinutes(10));
                }

                var imageUrl = cachedUser.ImageUrl;
                var userName = cachedUser.UserName;

                output.TagName = "div";
                output.Attributes.SetAttribute("class", "d-flex align-items-center");
                output.Content.SetHtmlContent($@"
                    <img src='{imageUrl}' class='rounded-circle' style='width: 25px; height: 25px; object-fit: cover; margin-right: 8px;' />
                    <span>{userName}</span>
                ");

            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
