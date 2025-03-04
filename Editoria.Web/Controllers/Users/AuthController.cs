using Course_Work_Editoria.Services.Auth;
using Editoria.Application.DTOs.Authentication;
using Editoria.Application.Services.Implementation;
using Editoria.Application.Services.Interfaces;
using Editoria.Web.Services;
using Editoria.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Editoria.Web.Controllers.Users
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly DropdownDataService _dropdownService;

        public AuthController(IUserService userService,
            DropdownDataService dropdownService)
        {
            _dropdownService = dropdownService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = _dropdownService.GetRolesSelectList();

            var registerRequest = new RegisterUserRequestVM
            {
                RegisterUserRequest = new RegisterUserRequest(),
                Roles = roles,
            };
            return View(registerRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequestVM viewModel, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var request = viewModel.RegisterUserRequest;
                await _userService.RegisterAsync(request.UserName, request.Email, request.Password, request.PhoneNumber, request.Role, imageFile);

                TempData["success"] = "Вы успешно зарегистрировались";
                return RedirectToAction("Index", "Home");
            }

            viewModel.Roles = _dropdownService.GetRolesSelectList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = await _userService.LoginAsync(request.Email, request.Password);
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
