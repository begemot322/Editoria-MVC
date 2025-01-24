using Course_Work_Editoria.Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Controllers.Users
{
    public class ProfileController : Controller
    {
        private readonly UserService _userService;

        public ProfileController(UserService userService)
        {
            _userService = userService;
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
                _userService.ChangePassword(Guid.Parse(userId), currentPassword, newPassword, confirmPassword);
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
    }
}
