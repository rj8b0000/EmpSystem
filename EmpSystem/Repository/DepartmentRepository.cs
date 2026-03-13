using EmpSystem.Data;
using EmpSystem.Models;
using EmpSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EmpSystem.Repository;

public class DepartmentRepository: IDepartmentRepository
{
    private readonly AppDbContext _dbContext;
    public DepartmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // public async Task<DepartmentViewModal> GetAsync(int Id)
    // {
    //     var department = await _dbContext.Departments.FindAsync(Id);
    //     var departmentViewModal = new DepartmentViewModal()
    //     {
    //         DepartmentId = department.DepartmentId,
    //         DepartmentName = department.DepartmentName,
    //     };
    //     return departmentViewModal;
    // }

    public async Task<DepartmentViewModal> GetByIdAsync(int id)
    {
        var department = await _dbContext.Departments.FindAsync(id);
        var departmentViewModel = new DepartmentViewModal
        {
            DepartmentId = department.DepartmentId,
            DepartmentName = department.DepartmentName
        };
        return departmentViewModel;
    }

    public async Task<List<DepartmentViewModal>> GetAllAsync()
    {
        List<Department> departments = await _dbContext.Departments.ToListAsync();
        List<DepartmentViewModal> departmentViewModals = new List<DepartmentViewModal>();
        
        foreach (var department in departments)
        {
            var departmentModel = new DepartmentViewModal
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
            };
            departmentViewModals.Add(departmentModel);
        }

        return departmentViewModals;
    }

    public async Task AddDepartment(DepartmentViewModal department)
    {
        var newDepartment = new Department()
        {
            DepartmentName = department.DepartmentName
        };
        await _dbContext.Departments.AddAsync(newDepartment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(DepartmentViewModal departmentUpdated)
    {
        var department = await _dbContext.Departments.FindAsync(departmentUpdated.DepartmentId);
        department.DepartmentName = departmentUpdated.DepartmentName;

        _dbContext.Departments.Update(department);
        await _dbContext.SaveChangesAsync();
    }

 
    public async Task DeleteAsync(int Id)
    {
        var department =await _dbContext.Departments.FindAsync(Id);
        _dbContext.Departments.Remove(department);
        await _dbContext.SaveChangesAsync();
    }
}