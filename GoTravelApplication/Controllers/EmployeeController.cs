using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTravelApplication.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReceptionistLogin()
        {
            return RedirectToAction("Index", "Receptionists", new { msg = "Fine" });
        }

        public IActionResult ModLogin()
        {
            return RedirectToAction("Index", "Moderators", new { msg = "Fine" });
        }

        public IActionResult AdminLogin()
        {
            return RedirectToAction("Index", "Administrators", new { msg = "Fine" });
        }
    }
}
