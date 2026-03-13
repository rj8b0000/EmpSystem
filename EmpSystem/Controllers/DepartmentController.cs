using EmpSystem.Repository;
using EmpSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var department = await _repository.GetByIdAsync(id);
        return View(department);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DepartmentViewModal department)
    {
        if (ModelState.IsValid)
        {
            //Update the database with modified details
            await _repository.UpdateAsync(department);
            return RedirectToAction("Index", "Department");
        }
        return View(department);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction("Index",  "Department");
    }
}