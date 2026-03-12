using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmpSystem.Models;
using EmpSystem.Repository;
using EmpSystem.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace EmpSystem.Controllers;

public class EmployeeController: Controller 
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public EmployeeController(IEmployeeRepository employeeRepository) 
    {
        _employeeRepository = employeeRepository;
    }
    public async Task<IActionResult> Index(string searchString, string sortOrder)
    {
        var employees = await _employeeRepository.GetAllAsync();
        if (!string.IsNullOrEmpty(searchString))
        {
            employees = new List<EmployeeViewModal>(employees.Where(n => n.FirstName.Contains(searchString) || n.LastName.Contains(searchString)));
        }
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateOfBirthSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
        ViewData["IsActiveSortParam"] = sortOrder == "isactive_asc" ? "isactive_desc" : "isactive_asc";

        switch (sortOrder)
        {
            case "name_desc": 
                employees = new List<EmployeeViewModal>(employees.OrderByDescending(n => n.FirstName));
                break;
            case "date_asc":
                employees = new List<EmployeeViewModal>(employees.OrderBy(s => s.DateOfBirth));
                break;
            case "date_desc":
                employees = new List<EmployeeViewModal>(employees.OrderByDescending(s => s.DateOfBirth));
                break;
            case "isactive_desc":
                employees = new List<EmployeeViewModal>(employees.OrderByDescending(e => e.IsActive));
                break;
            case "isactive_asc":
                employees = new List<EmployeeViewModal>(employees.OrderBy(e => e.IsActive));
                break;
            default:
                employees = employees.OrderBy(n => n.FirstName).ToList();
                break;
        }
        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var departments = await _employeeRepository.GetAllDepartmentsAsync();
        
        ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(EmployeeViewModal model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _employeeRepository.AddAsync(model);
        return RedirectToAction("Index",  "Employee");
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var departments = await _employeeRepository.GetAllDepartmentsAsync();
        ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
        
        var employee = await _employeeRepository.GetByIdAsync(id);
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmployeeViewModal employee)
    {
        if (!ModelState.IsValid)
        {
            return View(employee);
        }

        await _employeeRepository.UpdateAsync(employee);
        return RedirectToAction("Index",  "Employee");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _employeeRepository.DeleteAsync(id);
        return RedirectToAction("Index",  "Employee");
    }
}