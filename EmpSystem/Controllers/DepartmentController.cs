using EmpSystem.Repository;
using EmpSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EmpSystem.Controllers;

public class DepartmentController : Controller
{
    // GET
    private readonly IDepartmentRepository _repository;
    public DepartmentController(IDepartmentRepository repository)
    {
        _repository = repository;
    }
    public async Task<IActionResult> Index()
    {
        var departments = await _repository.GetAllAsync();
        return View(departments);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(DepartmentViewModal model)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(model);
        }

        await _repository.AddDepartment(model);

        TempData["message"] = $"Successfully added {model.DepartmentName}";

        return RedirectToAction("Index");
    }
}