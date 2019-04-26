using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cookbook.Data.Dto;
using Cookbook.Data.Models;
using Cookbook.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDataRepository<User, User> _dataRepository;

        public AccountController(IDataRepository<User, User> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            //Check the user name and password              
            var validUser = _dataRepository.GetAll()
                .SingleOrDefault(u => u.UserName == userName && u.Password == password);

            if (validUser != null && userName == validUser.UserName && password == validUser.Password)
            {
                //Create the identity for the user  
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, validUser.Id.ToString())
               }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

//        [HttpPost]  
        public IActionResult Logout()  
        {  
              var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  
              return RedirectToAction("Login", "Account");  
        }  

    }
}