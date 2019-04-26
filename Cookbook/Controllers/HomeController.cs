using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cookbook.Data.Models;
using Cookbook.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Cookbook.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cookbook.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDataRepository<User, User> _dataRepository;

        public HomeController(IDataRepository<User, User> dataRepository)
        {
            _dataRepository = dataRepository;

        }

        public IActionResult Index()
        {
            var userId = int.Parse(User.Identity.Name);

            var user = _dataRepository.Get(userId);

            ViewData["FullName"] = user.FullName;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
