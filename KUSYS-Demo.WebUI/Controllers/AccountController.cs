using KUSYS_Demo.Business.Abstract;
using KUSYS_Demo.Entities;
using KUSYS_Demo.WebUI.Identity;
using KUSYS_Demo.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IStudentService _studentService;
        private SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;



        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IStudentService studentService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentService = studentService;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };
            var entity = new Student()
            {
                //FirstName = model.FullName,
                FirstName=model.FullName.Split(' ')[0],
                LastName = model.FullName.Split(' ')[1],
                Email =model.Email
               

            };
            var role = "user";
            var result = await _userManager.CreateAsync(user, model.Password);
            await _roleManager.CreateAsync(new IdentityRole(role));

            if (result.Succeeded)
            {
                // generate token
                // send email

                 result=await _userManager.AddToRoleAsync(user, role);
                _studentService.Create(entity);
                return RedirectToAction("login", "account");
            }


            ModelState.AddModelError("", "Bilinmeyen hata oluştu lütfen tekrar deneyiniz.");
            return View(model);
        }


        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu email ile daha önce hesap oluşturulmamış.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            //var test = await _userManager.IsInRoleAsync(user , "admin");

            if (result.Succeeded)
            {
               var isAdmin = await _userManager.IsInRoleAsync(user, "admin");
                if (isAdmin)
                { 
                    return Redirect(model.ReturnUrl ?? "admin/students"); 
                }
                else
                {
                    return Redirect(model.ReturnUrl ?? "/user/students"); 
                }
               

            }

            ModelState.AddModelError("", "Email veya parola yanlış");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }



    }
}