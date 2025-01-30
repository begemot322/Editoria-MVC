using Course_Work_Editoria.Services.Auth;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;

namespace Course_Work_Editoria.TagHelpers
{
    public class UserTagHelper: TagHelper
    {
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;

        public UserTagHelper(UserService userService, IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public string DefaultImageUrl { get; set; } = "/images/users/free-icon-user-1077114.png";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst("userId")?.Value;

                var cacheKey = $"User_{userId}";

                if(!_cache.TryGetValue(cacheKey, out (string ImageUrl, string UserName) cachedUser))
                {
                    var userFromDb = _userService.GetUserById(Guid.Parse(userId));

                    cachedUser = (userFromDb.ImageUrl ?? DefaultImageUrl, userFromDb.UserName);
                    _cache.Set(cacheKey, cachedUser, TimeSpan.FromMinutes(10));
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
