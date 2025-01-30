using Course_Work_Editoria.Services.Auth;
using Course_Work_Editoria.Services.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Course_Work_Editoria.Controllers.Users
{
    public class ProfileController : Controller
    {
        private readonly UserService _userService;
        private readonly ProfileService _profileService;
        private readonly FileService _fileService;

        public ProfileController(UserService userService, ProfileService profileService, FileService fileService)
        {
            _userService = userService;
            _profileService = profileService;
            _fileService = fileService;  
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = _userService.GetUserById(Guid.Parse(userId));

            return View(user);
        }
        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var userId = User.FindFirst("userId")?.Value;

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                _profileService.ChangePassword(Guid.Parse(userId), currentPassword, newPassword, confirmPassword);
                TempData["success"] = "Пароль успешно изменён.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var user = _userService.GetUserById(Guid.Parse(userId));
            return View("Index", user);
        }

        [HttpPost]
        public IActionResult ChangeImage(IFormFile image)
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

            _profileService.UpdateUserImage(Guid.Parse(userId), imageUrl);
            TempData["success"] = "Аватар успешно обновлён.";

            return RedirectToAction("Index");
        }

    }
}
