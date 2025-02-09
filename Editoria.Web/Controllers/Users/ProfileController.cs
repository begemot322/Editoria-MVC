using Course_Work_Editoria.Services.Auth;
using Course_Work_Editoria.Services.File;
using Editoria.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Editoria.Web.Controllers.Users
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
        private readonly IFileService _fileService;

        public ProfileController(IUserService userService, IProfileService profileService,
            IFileService fileService)
        {
            _userService = userService;
            _profileService = profileService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                await _profileService.ChangePasswordAsync(Guid.Parse(userId), currentPassword, newPassword, confirmPassword);
                TempData["success"] = "Пароль успешно изменён.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
            return View("Index", user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeImage(IFormFile image)
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            string? imageUrl = null;

            if (image != null)
            {
                imageUrl = _fileService.SaveFile(image, "images/users");
            };

            await _profileService.UpdateUserImageAsync(Guid.Parse(userId), imageUrl);
            TempData["success"] = "Аватар успешно обновлён.";

            return RedirectToAction("Index");
        }

    }
}
