using Course_Work_Editoria.Authentication.Services;
using Editoria.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Controllers.Users
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserRequest request, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                _userService.Register(request.UserName, request.Email, request.Password, request.PhoneNumber, imageFile);
                TempData["success"] = "Вы успешно зарегистрировались";
                return RedirectToAction("Index", "Home");
            }

            return View(request);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = _userService.Login(request.Email, request.Password);
                    Response.Cookies.Append("SecurityCookies", token);
                    TempData["success"] = "Вы успешно вошли в аккаунт";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            
            return View(request);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("SecurityCookies");

            return RedirectToAction("Login", "Auth");
        }

    }
}
