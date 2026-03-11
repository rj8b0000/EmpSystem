using Microsoft.AspNetCore.Mvc;

namespace EmpSystem.Controllers;

public class DepartmentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}